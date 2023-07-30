using AuctionAPI.Models;
using AuctionAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace AuctionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        [HttpPost("CreateCategory")]
        public async Task<IActionResult> CreateCategory(string name, string Description)
        {
            try
            {
                Category category = new Category()
                {
                    Name = name,
                    Description = Description
                };
                await _categoryRepository.AddCategory(category);
                await _categoryRepository.Save();
                return Ok($"Controller:{category.Name} Category has been added.");
            }
            catch (Exception ex)
            {
                Log.Error($"ERROR:{ex.Message}");
                return StatusCode(500, "An unexpected error occurred.Please try again later.");
            }
        }

        [HttpGet("GetCategories")]
        public async Task<IActionResult> GetCategories()
        {
            try
            {
                var categories = await _categoryRepository.GetCategories();
                return Ok(categories);
            }
            catch (Exception ex)
            {
                Log.Error($"ERROR:{ex.Message}");
                return StatusCode(500, "An unexpected error occurred.Please try again later.");
            }
        }
        [HttpPut("UpdateCategory")]
        public async Task<IActionResult> UpdateCategory(int categoryId,string? name,string? description)
        {
            try
            {
                if (_categoryRepository.CategoryExist(categoryId))
                {
                    Category category = await _categoryRepository.GetCategoryById(categoryId);
                    category.Name = name ?? category.Name;
                    category.Description = description ?? category.Description;
                    _categoryRepository.UpdateCategory(category);
                    await _categoryRepository.Save();
                    return Ok($"{category.Name} has been updated.");
                }
                else
                {
                    return BadRequest($"Category id:{categoryId} does not exist");
                }
            }
            catch (Exception ex)
            {
                Log.Error($"ERROR:{ex.Message}");
                return StatusCode(500, "An unexpected error occurred.Please try again later.");
            }
        }
        [HttpDelete("DeleteCategory")]
        public async Task<IActionResult> DeleteCategory(int categoryId)
        {
            try
            {
                if (_categoryRepository.CategoryExist(categoryId))
                {
                    Category category = await _categoryRepository.GetCategoryById(categoryId);
                    _categoryRepository.DeleteCategory(category);
                    await _categoryRepository.Save();
                    return Ok($"{category.Name} has been deleted.");
                }
                else { return BadRequest($"Category id:{categoryId} does not exist"); }
            }
            catch (Exception ex)
            {
                Log.Error($"ERROR:{ex.Message}");
                return StatusCode(500, "An unexpected error occurred.Please try again later.");
            }
        }
    }
}
