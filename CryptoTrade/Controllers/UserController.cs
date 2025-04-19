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

        [HttpGet]
        [Route("UserById")]
        public async Task<IActionResult> GetUserById()
        {
            ApiResponse apiResponse = new ApiResponse();
            try
            {
                int id = 0;
                apiResponse.Data = await _unitOfWork.UserService.GetUserByIdAsync(id);
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

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(UserCreateDto userCreateDto)
        {
            ApiResponse apiResponse = new ApiResponse();
            try
            {
                apiResponse.Data = await _unitOfWork.UserService.CreateUserAsync(userCreateDto);
                return Ok(apiResponse);

            }
            catch(Exception e)
            {
                apiResponse.StatusCode = 400;
                apiResponse.Message = e.Message;
            }
            return BadRequest(apiResponse);
        }
    }
}
