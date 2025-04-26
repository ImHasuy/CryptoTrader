using CryptoTrade.Entities;
using CryptoTrade.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CryptoTrade.Controllers
{
    [ApiController]
    [Route("api/transactions")]
    public class TransactionController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public TransactionController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        ///Gets back with the transaction logs of the user which matches the id
        /// </summary>
        /// <param name="userid"></param>
        /// <returns>Returns the logs in a list</returns>
        [HttpGet]
        [Route("{userid}")]
        public async Task<IActionResult> LogTransactions(string userid)
        {
            ApiResponse apiResponse = new ApiResponse();
            try
            {
                apiResponse.Data = await _unitOfWork.TransactionLogService.ListTransactionsAsync(userid);
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
        /// Gets back with the transaction log which matches the id
        /// </summary>
        /// <param name="transactionid"></param>
        /// <returns>Returns a transaction</returns>
        [HttpGet]
        [Route("details/{transactionid}")]
        public async Task<IActionResult> GetTransactionById(string transactionid)
        {
            ApiResponse apiResponse = new ApiResponse();
            try
            {
                apiResponse.Data = await _unitOfWork.TransactionLogService.GetTransactionDetailsAsync(transactionid);
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
