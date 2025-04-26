using CryptoTrade.DTOs;
using CryptoTrade.Entities;
using CryptoTrade.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CryptoTrade.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class CryptoController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CryptoController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        /// <summary>
        /// Update the crypto with the given id
        /// </summary>
        /// <param name="cryptoUpdateDTO"></param>
        /// <returns>It returns a response which indicates the result of the action</returns>
        [HttpPut]
        [Route("price")]
        public async Task<IActionResult> UpdateCrypto(CryptoUpdateDTO cryptoUpdateDTO)
        {
            ApiResponse apiResponse = new ApiResponse();
            try
            {
                apiResponse.Message = await _unitOfWork.CryptoService.UpdateCryptoByIdAsync(cryptoUpdateDTO)!;

                return Ok(apiResponse);
            }
            catch (Exception e)
            {
                apiResponse.StatusCode = 400;
                apiResponse.Message = e.Message;
            }
            return BadRequest(apiResponse);
        }

        [HttpGet]
        [Route("price")]
        public async Task<IActionResult> GetExchangeRates()
        {
            ApiResponse apiResponse = new ApiResponse();
            try
            {
                apiResponse.Data = await _unitOfWork.CryptoService.GetAllExchangeRateAsync()!;
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
