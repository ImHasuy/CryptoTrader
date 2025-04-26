using CryptoTrade.DTOs;
using CryptoTrade.Entities;
using CryptoTrade.UOW;
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
                apiResponse.Message = await _unitOfWork.CryptoRepository.UpdateCryptoByIdAsync(cryptoUpdateDTO)!;

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
        /// Get the exchange rate history of the given crypto
        /// </summary>
        /// <param name="cryptoid"></param>
        /// <returns>Returns the logs of the crypto matches the cryptoid</returns>
        [HttpGet]
        [Route("price/histroy/{cryptoid}")]
        public async Task<IActionResult> GetExchangeRates(string cryptoid)
        {
            ApiResponse apiResponse = new ApiResponse();
            try
            {
                apiResponse.Data = await _unitOfWork.CryptoRepository.GetCryptoLogsByIdAsync(cryptoid)!;
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
