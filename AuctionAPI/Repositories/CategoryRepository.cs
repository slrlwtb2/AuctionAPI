using AuctionAPI.Data;
using AuctionAPI.Models;
using AuctionAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AuctionAPI.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;
        public CategoryRepository(ApplicationDbContext context)
        {
            _context= context;
        }
        public async Task AddCategory(Category category)
        {
            await _context.Categories.AddAsync(category);
        }

        public void DeleteCategory(Category category)
        {
            _context.Categories.Remove(category);
        }

        public async Task<List<Category>> GetCategories()
        {
           return await _context.Categories.ToListAsync();
        }

        public async Task<Category> GetCategoryById(int id)
        {
            return await _context.Categories.FirstAsync(c => c.Id == id);
        }

        public void Save()
        {
          _context.SaveChangesAsync();
        }

        public void UpdateCategory(Category category)
        {
            _context.Categories.Update(category);
        }
    }
}
