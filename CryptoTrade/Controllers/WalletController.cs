using CryptoTrade.DTOs;
using CryptoTrade.Entities;
using CryptoTrade.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CryptoTrade.Controllers
{
    [ApiController]
    [Route("api/wallet")]
    [Authorize]
    public class WalletController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public WalletController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Get the wallet of the user with the given id
        /// </summary>
        /// <param name="userid">The id of the user</param>
        /// <returns>It returns the wallet of the specified user</returns>
        [HttpGet]
        [Route("{userid}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> GetUserWallet(string userid)
        {
            ApiResponse apiResponse = new ApiResponse();
            try
            {
                var temp = await _unitOfWork.WalletService.GetWalletByUserIdAsync(userid);
                apiResponse.Data = temp;
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
        /// Top up the balance of the user with the given id
        /// </summary>
        /// <param name="userid">The Id of the User</param>
        /// <param name="walletTopUpDto">Contains the necessary datas for the function</param>
        /// <returns>It returns a response which indicates the result of the action</returns>
        [HttpPut]
        [Route("{userid}")]
        [Authorize(Policy = "AllUserPolicy")]
        public async Task<IActionResult> TopUpBalance(string userid, WalletTopUpDto walletTopUpDto)
        {
            ApiResponse apiResponse = new ApiResponse();
            try
            {
                var temp = await _unitOfWork.WalletService.TopUpWalletBalanceAsync(userid,walletTopUpDto);
                apiResponse.Message = temp;
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
        /// Delete the wallet of the user with the given id
        /// </summary>
        /// <param name="userid">The id of the user whos wallet will be deleted</param>
        /// <returns>It returns a response which indicates the result of the action</returns>
        [HttpDelete]
        [Route("{userid}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> DeleteWallet(string userid)
        {
            ApiResponse apiResponse = new ApiResponse();
            try
            {
                var temp = await _unitOfWork.WalletService.DeleteWalletAsync(userid);
                apiResponse.Message = temp;
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
