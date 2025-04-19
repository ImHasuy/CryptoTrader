using CryptoTrade.Entities;
using CryptoTrade.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace CryptoTrade.Controllers
{
    public class UserController : ControllerBase
    {

        private readonly IUnitOfWork _unitOfWork;
        public UserController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

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


    }
}
