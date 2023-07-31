using AuctionAPI.DTOs;
using AuctionAPI.Models;
using AuctionAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

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
            try
            {
                var products = await _productRepository.GetProducts();
                Log.Information("Get Products => {@products}", products);
                return Ok(products);
            }
            catch (Exception ex)
            {
                Log.Error($"An unexpected error occurred while getting the products.ERROR: {ex}");
                return StatusCode(500, "An unexpected error occurred while getting the products.Please try again later.");
            }
        }
        [HttpPut("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct(int productId,UpdateProductDTO updateProductDTO)
        {
            try
            {
                var product = await _productRepository.GetProductById(productId);
                if (product == null) { return BadRequest($"product id {productId} does not exist."); }
                product.Name = updateProductDTO.Name ?? product.Name;
                product.Description = updateProductDTO.Description ?? product.Description;
                product.CategoryId = updateProductDTO.CategoryId ?? product.CategoryId;
                product.StartingPrice = updateProductDTO.StarttingPrice ?? product.StartingPrice;
                _productRepository.UpdateProduct(product);
                await _productRepository.Save();
                Log.Information($"Update Product => {product}");
                return Ok(product);
            }
            catch (Exception ex)
            {
                Log.Error($"An unexpected error occurred while updating the product.ERROR: {ex}");
                return StatusCode(500, "An unexpected error occurred while updating the product.Please try again later.");
            }
        }
        [HttpPost("CreateProduct")]
        public async Task<IActionResult> CreateProduct(int sellerId,ProductDTO productDTO)
        {
            try
            {
                if (_userRepository.SellerExist(sellerId) && _categoryRepository.CategoryExist(productDTO.CategoryId))
                {
                    Product product = new Product()
                    {
                        Name = productDTO.Name,
                        Description = productDTO.Description ?? string.Empty,
                        CategoryId = productDTO.CategoryId,
                        SellerId = sellerId,
                        StartingPrice = productDTO.StarttingPrice,
                        CurrentBid = 0,
                        BidWinnerId = null,
                        BidEndTime = DateTime.Now.AddMinutes(productDTO.BidEndTime)
                    };
                    await _productRepository.AddProduct(product);
                    await _productRepository.Save();
                    Log.Information($"Create Product => {product}");
                    return Ok($"Product {product.Name} created");
                }
                return BadRequest("SellerId or CategoryId does not exist");
            }
            catch (Exception ex)
            {
                Log.Error($"An unexpected error occurred while creating the product.ERROR: {ex}");
                return StatusCode(500, "An unexpected error occurred while creating the product.Please try again later.");
            }
        }
        [HttpDelete("DeleteProduct")]
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            try
            {
                if (_productRepository.ProductExist(productId))
                {
                    var product = await _productRepository.GetProductById(productId);
                    _productRepository.DeleteProduct(product);
                    await _productRepository.Save();
                    Log.Information($"Delete Product => {product}");
                    return Ok($"Product {product.Id} has been deleted");
                }
                return BadRequest($"Product id {productId} does not exist");
            }
            catch (Exception ex)
            {
                Log.Error($"An unexpected error occurred while deleting the product.ERROR: {ex}");
                return StatusCode(500, "An unexpected error occurred while deleting the product.Please try again later.");
            }
        }
        [HttpPut("BidProduct")]
        public async Task<IActionResult> BidProduct(int userId,int productId,float amount)
        {
            try
            {
                if (_userRepository.UserExist(userId) && _productRepository.ProductExist(productId))
                {
                    var user = await _userRepository.GetById(userId);
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

                        //If latest bid on the product less than one minutes it will add one more minute. 
                        var timeLeft = product.BidEndTime - DateTime.Now;
                        if (timeLeft.TotalMinutes <= 1) { product.BidEndTime.AddMinutes(1); }
                        await _productRepository.Save();


                        Log.Information($"Bid on Product {product}");
                        return Ok($"User with the Id of {userId} has bid the item with the id of {productId} by {amount}");
                    }
                    return BadRequest("this user cannot bid the product");
                }
                return BadRequest($"UserId of {userId} or ProductId of {productId}, does not exist ");
            }
            catch (Exception ex)
            {
                Log.Error($"An unexpected error occurred while bidding the product.ERROR: {ex}");
                return StatusCode(500, "An unexpected error occurred while bidding the product.Please try again later.");
            }
        }
        [HttpGet("GetBidableProducts")]
        public async Task<IActionResult> GetBidableProducts()
        {
            try
            {
                var product = await _productRepository.GetBidableProducts(DateTime.Now);
                return Ok(product);
            }
            catch (Exception ex)
            {
                Log.Error($"An unexpected error occurred while get the product bidable status.ERROR: {ex}");
                return StatusCode(500, "An unexpected error occurred while get the product bidable status.Please try again later.");
            }
        }
    }
}
