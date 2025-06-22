using AutoMapper;
using CryptoTrade.DTOs;
using CryptoTrade.Entities;
using CryptoTrade.UOW;
using Microsoft.AspNetCore.Mvc;

namespace CryptoTrade.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class SavingsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public SavingsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }



        [HttpPost]
        [Route("lock")]
        public async Task<IActionResult> LockCrypto(SavingsInputDTO savingsInputDTO)
        {
            ApiResponse apiResponse = new ApiResponse();
            try
            {
                apiResponse.Data = await _unitOfWork.SavingsService.CreateSaving(savingsInputDTO);
                return Ok(apiResponse);
            }
            catch (Exception e)
            {
                apiResponse.StatusCode = 400;
                apiResponse.Message = e.Message;
            }
            return BadRequest(apiResponse);
        }

        //[HttpPut]
        //public async Task<IActionResult> ModifyLimits(List<CashBackDto> inputlist)
        //{
        //    ApiResponse apiResponse = new ApiResponse();
        //    try
        //    {
        //        apiResponse.Message = await _unitOfWork.CashbackService.ModifyCashback(inputlist);
        //        return Ok(apiResponse);
        //    }
        //    catch (Exception e)
        //    {
        //        apiResponse.StatusCode = 400;
        //        apiResponse.Message = e.Message;
        //    }
        //    return BadRequest(apiResponse);
        //}
    }
}
