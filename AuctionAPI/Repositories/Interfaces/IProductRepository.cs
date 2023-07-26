using AuctionAPI.Models;

namespace AuctionAPI.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Product>> GetProducts();
        Task<Product> GetProductById(int id);
        Task AddProduct(Product product);
        void UpdateProduct(Product product);    
        void DeleteProduct(Product product);
        bool ProductExist(int id);
        Task Save();
        Task<List<Product>> GetBidableProducts(DateTime dateTime);
    }
}
