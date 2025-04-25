using CryptoTrade.DTOs;
using CryptoTrade.Entities;
using CryptoTrade.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CryptoTrade.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    //[]
    public class CryptosController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CryptosController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Gets back with the list of all Cryptos
        /// </summary>
        /// <returns>Returns all the cryptos</returns>
        [HttpGet]
        [Route("Cryptos")]
        public async Task<IActionResult> GetCryptos()
        {
            ApiResponse apiResponse = new ApiResponse();
            try
            {
                var Cryptos = await _unitOfWork.CryptoService.GetCryptosAsync();
                apiResponse.Data = Cryptos;
                return Ok(apiResponse);
            }
            catch (Exception e)
            {
                apiResponse.StatusCode = 400;
                apiResponse.Message = e.Message;
            }
            return BadRequest(apiResponse);
        }

        /// <summary>
        /// Gets back with the crypto which matches the id
        /// </summary>
        /// <param name="cryptoid"></param>
        /// <returns>Return the crypto's value and name matches the id</returns>
        [HttpGet]
        [Route("Cryptos/{cryptoid}")]
        public async Task<IActionResult> GetCryptoById(string cryptoid)
        {
            ApiResponse apiResponse = new ApiResponse();
            try
            {
                apiResponse.Data = await _unitOfWork.CryptoService.GetCryptoByIdAsync(cryptoid);
                return Ok(apiResponse);
            }
            catch (Exception e)
            {
                apiResponse.StatusCode = 400;
                apiResponse.Message = e.Message;
            }
            return BadRequest(apiResponse);
        }

        /// <summary>
        /// Creates a new crypto
        /// </summary>
        /// <param name="cryptoCreateDTO"></param>
        /// <returns>It returns a response which indicates the result of the action</returns>
        [HttpPost]
        [Route("Cryptos")]
        public async Task<IActionResult> AddNewCrypto(CryptoDTO cryptoCreateDTO)
        {
            ApiResponse apiResponse = new ApiResponse();
            try
            {
                apiResponse.Message = await _unitOfWork.CryptoService.AddNewCryptoAsync(cryptoCreateDTO)!;

                return Ok(apiResponse);
            }
            catch (Exception e)
            {
                apiResponse.StatusCode = 400;
                apiResponse.Message = e.Message;
            }
            return BadRequest(apiResponse);
        }

        /// <summary>
        /// Deletes a crypto by its ID
        /// </summary>
        /// <param name="cryptoid"></param>
        /// <returns>It returns a response which indicates the result of the action</returns>
        [HttpDelete]
        [Route("Cryptos/{cryptoid}")]
        public async Task<IActionResult> DeletCryptoById(string cryptoid)
        {
            ApiResponse apiResponse = new ApiResponse();
            try
            {
                apiResponse.Message = await _unitOfWork.CryptoService.DeletCryptoByIdAsync(cryptoid)!;

                return Ok(apiResponse);
            }
            catch (Exception e)
            {
                apiResponse.StatusCode = 400;
                apiResponse.Message = e.Message;
            }
            return BadRequest(apiResponse);
        }

    }
}
