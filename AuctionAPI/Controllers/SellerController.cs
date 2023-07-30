using AuctionAPI.Models;
using AuctionAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace AuctionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SellerController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        public SellerController(IUserRepository userRepository)
        {
            _userRepository= userRepository;
        }
        [HttpPost("CreateSeller")]
        public async Task<IActionResult> CreateSeller(string username,string? email)
        {
            try
            {
                User seller = new Seller()
                {
                    Username = username,
                    Email = email ?? string.Empty,
                    RegisterationDate = DateTime.Now,
                    Bidable = false
                };
                await _userRepository.AddUser(seller);
                await _userRepository.Save();
                return Ok(seller);
            }
            catch (Exception ex)
            {
                Log.Error($"ERROR:{ex.Message}");
                return StatusCode(500, "An unexpected error occurred.Please try again later.");
            }
        }
        [HttpGet("GetSellers")]
        public async Task<IActionResult> GetSellers()
        {
            try
            {
                var sellers = await _userRepository.GetSellers();
                return Ok(sellers);
            }
            catch (Exception ex)
            {
                Log.Error($"ERROR:{ex.Message}");
                return StatusCode(500, "An unexpected error occurred.Please try again later.");
            }
        }
        [HttpGet("GetRating")]
        public async Task<IActionResult> GetRating(int sellerId)
        {
            try
            {
                if (_userRepository.SellerExist(sellerId))
                {
                    Seller seller = await _userRepository.GetSellerById(sellerId);
                    return Ok(seller.GetRating());
                }
                return BadRequest($"Seller with the id of {sellerId} does not exist.");
            }
            catch (Exception ex)
            {
                Log.Error($"ERROR:{ex.Message}");
                return StatusCode(500, "An unexpected error occurred.Please try again later.");
            }
        }
    }
}
