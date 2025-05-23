﻿
using CryptoTrade.Context;
using CryptoTrade.Entities;
using System.Net.Http;
using System.Text.Json;
using CryptoTrade.Context;
using static System.Runtime.InteropServices.JavaScript.JSType;
using CryptoTrade.Repositories.Interfaces;
using CryptoTrade.DTOs;
using Microsoft.EntityFrameworkCore;


namespace CryptoTrade.Services
{


    public class CryptoExchRateUpdateBGService : BackgroundService
    {
        public class CurrencyData
        {
            public double usd { get; set; }
        }


        private readonly HttpClient _httpClient;
        private readonly ILogger<CryptoExchRateUpdateBGService> _logger;
        private readonly string _apiKey;
        private readonly IServiceProvider _serviceProvider;

        public CryptoExchRateUpdateBGService(IHttpClientFactory httpClientFactory, ILogger<CryptoExchRateUpdateBGService> logger, IConfiguration configuration, IServiceProvider serviceProvider)
        {

            _httpClient = httpClientFactory.CreateClient();
            _logger = logger;
            _apiKey = configuration["ApiKey"]!;
            if (string.IsNullOrWhiteSpace(_apiKey))
            {
                _logger.LogError("API key is missing or invalid.");
                throw new Exception("API key is required.");
            }
            _serviceProvider = serviceProvider;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("x-cg-demo-api-key", $"{_apiKey}");
            _httpClient.DefaultRequestHeaders.Add("accept", "application/json");
            var url = "https://api.coingecko.com/api/v3/simple/price?ids=bitcoin,ethereum,cardano,solana,polkadot,chainlink,uniswap,litecoin,dogecoin,ripple,stellar,tron,algorand,near,vechain&vs_currencies=usd";
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var _context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                        var response = await _httpClient.GetAsync(url, stoppingToken);
                        response.EnsureSuccessStatusCode();

                        var content = await response.Content.ReadAsStringAsync(); //Contains the response in JSON format
                        var data = JsonSerializer.Deserialize<Dictionary<string, CurrencyData>>(content) ?? throw new Exception("Error occuerd while deserializeing.");

                        if (_context.Cryptos.Any())
                        {
                            if (data != null)
                            {
                                foreach (var kvp in data)
                                {
                                    var l_Crypto = _context.Cryptos.FirstOrDefault(x => x.Name == kvp.Key);
                                    if (l_Crypto == null) continue; // Skip to the next kvp if null, in case of a crypto is deleted previously

                                    l_Crypto.Value = kvp.Value.usd;
                                    _context.Cryptos.Update(l_Crypto);

                                    // Log the updated values
                                    var CryptoLogValue = new ExchangeRateLog
                                    {
                                        CryptoId = l_Crypto.Id.ToString(),
                                        Value = kvp.Value.usd,
                                        Date = DateTime.UtcNow
                                    };
                                    await _context.ExchangeRateLogs.AddAsync(CryptoLogValue);
                                }
                                await _context.SaveChangesAsync();
                            }
                        }
                        else
                        {
                            await FirstRun(data);
                            await DummyDataInsert();
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Hiba az árfolyam lekérés során");
                }

                Thread.Sleep(60000); // Sleep for 1 minute
            }
        }

        public async Task<bool> FirstRun(Dictionary<string, CurrencyData> data)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                if (data != null)
                {
                    foreach (var kvp in data)
                    {
                        //New Crypto object creation
                        var temp_crypto = new Crypto
                        {
                            Name = kvp.Key,
                            Value = kvp.Value.usd
                        };
                        await _context.Cryptos.AddAsync(temp_crypto);

                        //Log the values 
                        var CryptoLogValue = new ExchangeRateLog
                        {
                            CryptoId = temp_crypto.Id.ToString(),
                            Value = kvp.Value.usd,
                            Date = DateTime.UtcNow
                        };
                        await _context.ExchangeRateLogs.AddAsync(CryptoLogValue);
                    }
                    await _context.SaveChangesAsync();
                    return true;
                }else 
                {
                    throw new Exception("Error occuerd while deserializeing.");
                }
                
            }
        }


        public async Task<bool> DummyDataInsert()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                var userService = scope.ServiceProvider.GetService<IUserServices>();
                var cryptoTradeService = scope.ServiceProvider.GetService<ICryptoTradeService>();
                var cryptoList = await _context.Cryptos.ToListAsync();
                var random = new Random();

                var jsondata = JsonSerializer.Deserialize<List<UserCreateDto>>(File.ReadAllText("./DummyDatas/DummyUsers.json"));
                if (jsondata == null)
                {
                    throw new Exception("Error occuerd while inserting Dummy datas");
                }
                for (int i = 0; i < jsondata.Count; i++)
                {
                    User user;
                    if (i == 1)
                    {
                        user = await userService.CreateAdminAsync(jsondata[i]);
                    }
                    else {
                        user = await userService.CreateUserAsync(jsondata[i]);
                    }

                    var cryptoAmountForSold = random.NextDouble() * 32 + 1;

                    await cryptoTradeService.BuyCryptoAsync(new CryptoTradeDTOtoFunc
                    {
                        UserGuid = user.Id.ToString(),
                        CryptoId = cryptoList[i].Id.ToString(),
                        Amount = ((double)(i) + cryptoAmountForSold) /10000
                    });
                    await cryptoTradeService.BuyCryptoAsync(new CryptoTradeDTOtoFunc
                    {
                        UserGuid = user.Id.ToString(),
                        CryptoId = cryptoList[i + 1].Id.ToString(),
                        Amount = ((double)(i) + (random.NextDouble() * 32 + 1)) / 10000
                    });
                    await cryptoTradeService.SellCryptoAsync(new CryptoTradeDTOtoFunc
                    {
                        UserGuid = user.Id.ToString(),
                        CryptoId = cryptoList[i].Id.ToString(),
                        Amount = ((double)(i) + cryptoAmountForSold) / 11521
                    });
                }
            }
            return true;
        }


    }
}
