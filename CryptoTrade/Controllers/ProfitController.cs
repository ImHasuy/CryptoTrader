using CryptoTrade.DTOs;
using CryptoTrade.Entities;
using CryptoTrade.UOW;
using Microsoft.AspNetCore.Mvc;

namespace CryptoTrade.Controllers
{
    [ApiController]
    [Route("api/profit")]
    public class ProfitController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProfitController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// This endpoint allows a user to get the overall profit/loss of the user with the given Userid.
        /// </summary>
        /// <param name="userid"></param>
        /// <returns>Returns the overall profit/loss</returns>
        [HttpPut]
        [Route("price/{userid}")]
        public async Task<IActionResult> OverAllProfit(string userid)
        {
            ApiResponse apiResponse = new ApiResponse();
            try
            {
                var temp = await _unitOfWork.ProfitRepository.GetAllProfitAsync(userid)!;
                apiResponse.Message= $"The Overall Profit/Loss of the user with id {userid} is {temp}";
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
        /// This endpoint allows a user to get the detailed profit/loss of the user with the given Userid.
        /// </summary>
        /// <param name="userid"></param>
        /// <returns>Returns a Json with the detailes of profit/loss</returns>
        [HttpPut]
        [Route("price/details/{userid}")]
        public async Task<IActionResult> GetOverDetailedProfitAsync(string userid)
        {
            ApiResponse apiResponse = new ApiResponse();
            try
            {
                apiResponse.Data = await _unitOfWork.ProfitRepository.GetDetailedProfitAsync(userid)!;
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
