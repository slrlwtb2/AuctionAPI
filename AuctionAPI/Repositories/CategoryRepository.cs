﻿using AuctionAPI.Data;
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

        public bool CategoryExist(int id)
        {
            var category = _context.Categories.FirstOrDefault(c => c.Id == id);
            if (category == null) { return false; }
            return true;
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

        public async Task Save()
        {
          await _context.SaveChangesAsync();
        }

        public void UpdateCategory(Category category)
        {
            _context.Categories.Update(category);
        }
    }
}
