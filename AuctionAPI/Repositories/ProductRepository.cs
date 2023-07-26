using AuctionAPI.Data;
using AuctionAPI.Models;
using AuctionAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AuctionAPI.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context)
        {
            _context= context;
        }
        public async Task AddProduct(Product product)
        {
            await _context.Products.AddAsync(product);
        }

        public void DeleteProduct(Product product)
        {
            _context.Products.Remove(product);
        }

        public async Task<Product> GetProductById(int id)
        {
            return await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Product>> GetProducts()
        {
            var products = await _context.Products.ToListAsync();
            return products;
        }

        public bool ProductExist(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            if (product == null) { return false; }
            return true;
        }

        public void UpdateProduct(Product product)
        {
            _context.Products.Update(product);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<List<Product>> GetBidableProducts(DateTime dateTime)
        {
          var products =  await _context.Products.Where(p => p.BidEndTime >= dateTime).ToListAsync();
            return products;
        }
    }
}
