using CryptoTrade.DTOs;
using CryptoTrade.Entities;
using CryptoTrade.Repositories.Interfaces;
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

        [HttpPut]
        [Route("price/{userid}")]
        public async Task<IActionResult> OverAllProfit(string userid)
        {
            ApiResponse apiResponse = new ApiResponse();
            try
            {
                var temp = await _unitOfWork.ProfitService.GetAllProfitAsync(userid)!;
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

        [HttpPut]
        [Route("price/details/{userid}")]
        public async Task<IActionResult> GetOverDetailedProfitAsync(string userid)
        {
            ApiResponse apiResponse = new ApiResponse();
            try
            {
                apiResponse.Data = await _unitOfWork.ProfitService.GetDetailedProfitAsync(userid)!;
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
