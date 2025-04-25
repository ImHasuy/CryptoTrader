using AutoMapper;
using CryptoTrade.DTOs;
using CryptoTrade.Entities;
using CryptoTrade.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CryptoTrade.Controllers
{
    [ApiController]
    [Route("api/trade")]
    [Authorize]
    public class CryptoTradeController : ControllerBase
    {
        
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CryptoTradeController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// This endpoint allows a user to buy cryptocurrency.
        /// </summary>
        /// <param name="createTradeDTO"></param>
        /// <returns>It returns a response which indicates the result of the action</returns>
        [HttpPost("buy")]
        [Authorize(Policy = "AllUserPolicy")]
        public async Task<IActionResult> BuyCrypto([FromBody] CryptoTradeDTO createTradeDTO)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var l_userId = User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value.ToString();
                var temp = _mapper.Map<CryptoTradeDTOtoFunc>(createTradeDTO);
                temp.UserGuid = l_userId;
   
                var result = await _unitOfWork.CryptoTradeService.BuyCryptoAsync(temp);
                response.Message = "Crypto bought successfully";
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.StatusCode = 400;
                response.Message = ex.Message;
                return BadRequest(response);
            }
        }

        /// <summary>
        /// This endpoint allows a user to sell cryptocurrency.
        /// </summary>
        /// <param name="createTradeDTO"></param>
        /// <returns>It returns a response which indicates the result of the action</returns>
        [HttpPost("sell")]
        [Authorize(Policy = "AllUserPolicy")]
        public async Task<IActionResult> SellCrypto([FromBody] CryptoTradeDTO createTradeDTO)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var l_userId = User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value.ToString();
                var temp = _mapper.Map<CryptoTradeDTOtoFunc>(createTradeDTO);
                temp.UserGuid = l_userId;

                var result = await _unitOfWork.CryptoTradeService.SellCryptoAsync(temp);
                response.Message = "Crypto sold successfully";
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.StatusCode = 400;
                response.Message = ex.Message;
                return BadRequest(response);
            }
        }



    }
}
