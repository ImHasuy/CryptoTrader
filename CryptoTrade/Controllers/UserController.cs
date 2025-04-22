using CryptoTrade.DTOs;
using CryptoTrade.Entities;
using CryptoTrade.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using NuGet.Protocol;

namespace CryptoTrade.Controllers
{

    /// <summary>
    /// This controller is responsible for handling user-related operations such as registration and fetching user details.
    /// </summary>
    [ApiController]
    [Route("api/[Controller]")]
    //[Authorize]
    public class UserController : ControllerBase
    {

        private readonly IUnitOfWork _unitOfWork;
        /// <summary>
        /// Constructor for UserController
        /// </summary>
        /// <param name="unitOfWork"></param>
        public UserController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Gets back with the user which matches the id
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


        /// <summary>
        /// Login endpoitn to an existing user and return a JWT token.
        /// </summary>
        /// <param name="userLoginDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto userLoginDto)
        {
            ApiResponse apiResponse = new ApiResponse();
            try
            {
                var token = await _unitOfWork.UserService.AuthenticateAsync(userLoginDto);
                apiResponse.Message =token;
                return Ok(apiResponse);
            }
            catch (Exception e)
            {
                apiResponse.StatusCode = 400;
                apiResponse.Message = e.Message;
            }
            return BadRequest(apiResponse.Message);
        }
    }
}
