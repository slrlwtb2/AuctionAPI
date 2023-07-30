﻿using AuctionAPI.Models;
using AuctionAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace AuctionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository= userRepository;
        }
        [HttpGet("GetUsers")]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                var users = await _userRepository.GetUsers();
                return Ok(users);
            }
            catch (Exception ex)
            {
                Log.Error($"ERROR:{ex.Message}");
                return StatusCode(500, "An unexpected error occurred.Please try again later.");
            }
        }
        [HttpGet("GetBalance")]
        public async Task<IActionResult> GetBalance(int userId)
        {
            try
            {
                User user = await _userRepository.GetById(userId);
                return Ok(user.GetBalance());
            }
            catch (Exception ex)
            {
                Log.Error($"ERROR:{ex.Message}");
                return StatusCode(500, "An unexpected error occurred.Please try again later.");
            }
        }
        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser(string username,string? email)
        {
            try
            {
                User user = new User()
                {
                    Username = username,
                    Email = email ?? string.Empty,
                    RegisterationDate = DateTime.Now,
                };
                await _userRepository.AddUser(user);
                await _userRepository.Save();

                return Ok(user);
            }
            catch (Exception ex)
            {
                Log.Error($"ERROR:{ex.Message}");
                return StatusCode(500, "An unexpected error occurred.Please try again later.");
            }
        }
        [HttpPut("IncreaseBalance")]
        public async Task<IActionResult> IncreaseBalnace(int userId, float amount)
        {
            try
            {
                if (_userRepository.UserExist(userId))
                {
                    User user = await _userRepository.GetById(userId);
                    user.IncreaseBalance(amount);
                    await _userRepository.Save();
                    return Ok($"User:{user.Username}'s balance has increased by {amount}");
                }
                return BadRequest($"User with the id of {userId} does not exist.");
            }
            catch (Exception ex)
            {
                Log.Error($"ERROR:{ex.Message}");
                return StatusCode(500, "An unexpected error occurred.Please try again later.");
            }
        }
        [HttpPut("DecreaseBalance")]
        public async Task<IActionResult> DecreaseBalance(int userId, float amount)
        {
            try
            {
                if (_userRepository.UserExist(userId))
                {
                    User user = await _userRepository.GetById(userId);
                    user.DecreaseBalance(amount);
                    await _userRepository.Save();
                    return Ok($"User:{user.Username}'s balance has decreased by {amount}.");
                }
                return BadRequest($"User with the id of {userId} does not exist.");
            }
            catch (Exception ex)
            {
                Log.Error($"ERROR:{ex.Message}");
                return StatusCode(500, "An unexpected error occurred.Please try again later.");
            }
        }
        [HttpPut("GiveRatingToSeller")]
        public async Task<IActionResult> GiveRatingToSeller(int sellerId, int amount)
        {
            try
            {
                if (_userRepository.SellerExist(sellerId))
                {
                    Seller seller = await _userRepository.GetSellerById(sellerId);
                    if (amount <= 5 && amount >= 0)
                    {
                        seller.Rating += amount;
                        seller.RatingCount += 1;
                    }
                    else
                    {
                        return BadRequest("Rating must be between 0 to 5.");
                    }
                    await _userRepository.Save();
                    return Ok($"{seller.Username}'s rating has increased by {amount}.");
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
