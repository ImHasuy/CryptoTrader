using CryptoTrade.DTOs;
using CryptoTrade.Entities;
using CryptoTrade.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using NuGet.Protocol;
using System.Security.Claims;

namespace CryptoTrade.Controllers
{

    /// <summary>
    /// This controller is responsible for handling user-related operations such as registration and fetching user details.
    /// </summary>
    [ApiController]
    [Route("api/users")]
    [Authorize]
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
        /// <returns>It returns with the User</returns>
        [HttpGet]
        [Route("UserById/{userid}")]
        [Authorize(Policy = "AdminPolicy")]
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
        /// <returns>It returns a response which indicates the result of the action</returns>
        [HttpPost]
        [Route("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody]UserCreateDto userCreateDto)
        {
            ApiResponse apiResponse = new ApiResponse();
            try
            {
                await _unitOfWork.UserService.CreateUserAsync(userCreateDto);
                apiResponse.Message = "User Created Successfully";
                return Ok(apiResponse);
            }
            catch(Exception e)
            {
                apiResponse.StatusCode = 400;
                apiResponse.Message = e.Message;
            }
            return BadRequest(apiResponse);
        }


        /// <summary>
        /// Login endpoitn to an existing user and return a JWT token.
        /// </summary>
        /// <param name="userLoginDto"></param>
        /// <returns>It returns a response which indicates the result of the action</returns>
        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
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
            return BadRequest(apiResponse);
        }

        /// <summary>
        /// This endpoint is used to Disable a user account.
        /// </summary>
        /// <returns>It returns a response which indicates the result of the action</returns>
        [HttpDelete]
        [Route("Delete")]
        [Authorize(Policy = "AllUserPolicy")]
        public async Task<IActionResult> DeleteUser()
        {
            ApiResponse apiResponse = new ApiResponse();
            try
            {
                var id = User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
                if(await _unitOfWork.UserService.DeleteUserAsync(id))
                {
                    apiResponse.Message = "User succesfully deleted";
                    return Ok(apiResponse);
                } 
            }
            catch (Exception e)
            {
                apiResponse.StatusCode = 400;
                apiResponse.Message = e.Message;
            }
            return BadRequest(apiResponse.Message);
        }


        /// <summary>
        /// This endpoint updatein existing user infomations based on the provided ones.
        /// </summary>
        /// <param name="userUpdateDto">The provided informations to be updated</param>
        /// <param name="userid">The subject UserId to change</param>
        /// <returns>It returns a response which indicates the result of the action</returns>
        [HttpPut]
        [Route("{userid}")]
        [Authorize(Policy = "AllUserPolicy")]
        public async Task<IActionResult> UpdateUser([FromBody] UserUpdateDto userUpdateDto,string userid)
        {
            ApiResponse apiResponse = new ApiResponse();
            try
            {
                //var id = User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;             Later
                await _unitOfWork.UserService.UpdateUserAsync(userUpdateDto, userid);
                apiResponse.Message = "The update was succesfull";
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
