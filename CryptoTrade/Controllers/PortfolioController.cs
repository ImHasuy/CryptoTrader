using CryptoTrade.Entities;
using CryptoTrade.UOW;
using Microsoft.AspNetCore.Mvc;

namespace CryptoTrade.Controllers
{
    [ApiController]
    [Route("api/portfolio")]
    public class PortfolioController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public PortfolioController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Gets back with the portfolio of the user which matches the id
        /// </summary>
        /// <param name="userid"></param>
        /// <returns>Return the portfolio </returns>
        [HttpGet]
        [Route("{userid}")]
        public async Task<IActionResult> GetPortfolio(string userid)
        {
            ApiResponse apiResponse = new ApiResponse();
            try
            {
                apiResponse.Data = await _unitOfWork.PortfolioRepository.GetPortfolioAsync(userid);
                return Ok(apiResponse);
            }
            catch (Exception ex)
            {
                apiResponse.StatusCode = 400;
                apiResponse.Message = ex.Message;
                return BadRequest(apiResponse);
            }
        }
    }
}
