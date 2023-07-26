using AuctionAPI.Models;
using AuctionAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuctionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICategoryRepository _categoryRepository;
        public ProductController(IProductRepository productRepository,
            IUserRepository userRepository,
            ICategoryRepository categoryRepository)
        {
            _productRepository= productRepository;
            _userRepository= userRepository;
            _categoryRepository= categoryRepository;
        }
        [HttpGet("GetProducts")]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _productRepository.GetProducts();
            return Ok(products);
        }
        [HttpPut("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct(
            int productId,
            string? productName,
            string? productDescription,
            int? categoryId,
            float? startingPrice)
        {
            var product = await _productRepository.GetProductById(productId);
            if (product == null) { return BadRequest($"product id {productId} does not exist."); }
            product.Name = productName ?? product.Name;
            product.Description = productDescription ?? product.Description;
            product.CategoryId = categoryId ?? product.CategoryId;
            product.StartingPrice = startingPrice ?? product.StartingPrice;
            _productRepository.UpdateProduct(product);
            await _productRepository.Save();
            return Ok(product);
        }
        [HttpPost("CreateProduct")]
        public async Task<IActionResult> CreateProduct(string name,string? description,int categoryId,int sellerId,float startingPrice,double endTime)
        {
            if (_userRepository.SellerExist(sellerId) && _categoryRepository.CategoryExist(categoryId))
            {
                Product product = new Product()
                {
                    Name = name,
                    Description = description ?? string.Empty,
                    CategoryId = categoryId,
                    SellerId = sellerId,
                    StartingPrice = startingPrice,
                    CurrentBid = 0,
                    BidWinnerId = null,
                    BidEndTime = DateTime.Now.AddMinutes(endTime)
                }; 
                await _productRepository.AddProduct(product);
                await _productRepository.Save();
                return Ok($"Product {product.Name} created");
            }
            return BadRequest("SellerId or CategoryId does not exist");
        }
        [HttpDelete("DeleteProduct")]
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            if (_productRepository.ProductExist(productId))
            {
                var product = await _productRepository.GetProductById(productId);
                _productRepository.DeleteProduct(product);
                await _productRepository.Save();
                return Ok($"Product {product.Id} has been deleted"); 
            }
            return BadRequest($"Product id {productId} does not exist");
        }
        [HttpPut("BidProduct")]
        public async Task<IActionResult> BidProduct(int userId,int productId,float amount)
        {
            if (_userRepository.UserExist(userId) && _productRepository.ProductExist(productId))
            {
                var user =  await _userRepository.GetById(userId);
                if (user.Bidable)
                {
                    Product product = await _productRepository.GetProductById(productId);
                    if (DateTime.Now >= product.BidEndTime) { return BadRequest("The time has passed, and no more bids can be accepted."); }

                    if (amount < product.StartingPrice) { return BadRequest($"bids must be more or equal to starting price (must be at least {product.StartingPrice})"); }


                    var tenpercent = product.CurrentBid * 0.1;
                    var preferPrice = product.CurrentBid + tenpercent;
                    if (amount <= preferPrice) { return BadRequest($"bids must be more than 10 percent of the current bid (must be at greater than {preferPrice})"); }


                    product.CurrentBid = amount;
                    product.BidWinnerId = userId;
                    var timeLeft = product.BidEndTime - DateTime.Now;
                    if (timeLeft.TotalMinutes <= 1) { product.BidEndTime.AddMinutes(1); }
                    await _productRepository.Save();
                    return Ok($"User with the Id of {userId} has bid the item with the id of {productId} by {amount}");
                }
                return BadRequest("this user cannot bid the product");
            }
            return BadRequest($"UserId of {userId} or ProductId of {productId}, does not exist ");
        }
        [HttpGet("GetBidableProducts")]
        public async Task<IActionResult> GetBidableProducts()
        {
            var product = await _productRepository.GetBidableProducts(DateTime.Now);
            return Ok(product);
        }
    }
}
