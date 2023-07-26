using AuctionAPI.Models;

namespace AuctionAPI.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetCategories();
        Task<Category> GetCategoryById(int id);
        Task AddCategory(Category category);
        void DeleteCategory(Category category);
        void UpdateCategory(Category category);
        void Save();
    }
}
