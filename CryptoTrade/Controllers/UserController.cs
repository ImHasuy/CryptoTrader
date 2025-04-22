using CryptoTrade.DTOs;
using CryptoTrade.Entities;
using CryptoTrade.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace CryptoTrade.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    //[Authorize]
    public class UserController : ControllerBase
    {

        private readonly IUnitOfWork _unitOfWork;
        public UserController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Gets back with the uesr which matches the id
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("UserById/{userid}")]
        public async Task<IActionResult> GetUserById([FromRoute] string userid)
        {
            ApiResponse apiResponse = new ApiResponse();
            try
            {
                apiResponse.Data = await _unitOfWork.UserService.GetUserByIdAsync(userid);
                return Ok(apiResponse);
                //Gets back with the User in the .Data
            }
            catch (Exception e)
            {
                apiResponse.StatusCode = 400;
                apiResponse.Message = e.Message;
            }
            return BadRequest(apiResponse);
            //Gets back with statuscode and message of the error

        }


        /// <summary>
        /// Register a new user.
        /// </summary>
        /// <param name="userCreateDto">The details for the user to be created.</param>
        /// <returns>It returns which indicates the result of the action</returns>
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody]UserCreateDto userCreateDto)
        {
            ApiResponse apiResponse = new ApiResponse();
            try
            {
                apiResponse.Data = await _unitOfWork.UserService.CreateUserAsync(userCreateDto);
                apiResponse.Message = "User Created Successfully";
                return Ok(apiResponse);

            }
            catch(Exception e)
            {
                apiResponse.StatusCode = 400;
                apiResponse.Message = e.Message;
            }
            return BadRequest(apiResponse.Message);
        }
    }
}
