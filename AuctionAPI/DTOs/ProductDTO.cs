using System.ComponentModel.DataAnnotations;

namespace AuctionAPI.DTOs
{
    public class ProductDTO
    {
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "CategoryId is required.")]
        public int CategoryId { get; set; }
        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string Description { get; set; } = string.Empty;
        [Range(0.0, double.MaxValue, ErrorMessage = "StartingPrice must be a non-negative value.")]
        public float StarttingPrice { get; set; }
        [Required(ErrorMessage = "BidEndTime is required.")]
        public double BidEndTime { get; set; }
    }
}
