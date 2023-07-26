using AuctionAPI.Models;
using AuctionAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
            var users = await _userRepository.GetUsers();
            return Ok(users);
        }
        [HttpGet("GetBalance")]
        public async Task<IActionResult> GetBalance(int userId)
        {
            User user = await _userRepository.GetById(userId);
            return Ok(user.GetBalance());
        }
        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser(string username,string? email)
        {
            User user = new User()
            {
                Username = username,
                Email = email ?? string.Empty,
                RegisterationDate= DateTime.Now,
            };
            await _userRepository.AddUser(user);
            await _userRepository.Save();
            
            return Ok(user);
        }
        [HttpPut("IncreaseBalance")]
        public async Task<IActionResult> IncreaseBalnace(int userId, float amount)
        {
            User user = await _userRepository.GetById(userId);
            user.IncreaseBalance(amount);
            await _userRepository.Save();
            return Ok($"User:{user.Username}'s balance has increased by {amount}");
        }
        [HttpPut("DecreaseBalance")]
        public async Task<IActionResult> DecreaseBalance(int userId, float amount)
        {
            User user = await _userRepository.GetById(userId);
            user.DecreaseBalance(amount);
            await _userRepository.Save();
            return Ok($"User:{user.Username}'s balance has decreased by {amount}");
        }

    }
}
