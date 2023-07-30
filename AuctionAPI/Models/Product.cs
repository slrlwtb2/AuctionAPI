using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AuctionAPI.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
        [StringLength(200)]
        public string Description { get; set; } = string.Empty;
        [Required]
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        [Required]
        [ForeignKey("Seller")]
        public int SellerId { get; set; }
        [Required]
        [Range(0.0f,float.MaxValue)]
        public float StartingPrice { get; set; }
        public float CurrentBid { get; set; } = 0.0f;
        [ForeignKey("User")]
        public int? BidWinnerId { get; set; } = null;
        [Required]
        public DateTime BidEndTime { get; set; }

        [JsonIgnore]
        public User User { get; set; } //navigation property
        [JsonIgnore]
        public Seller Seller { get; set; } //navigation property
        [JsonIgnore]
        public Category Category{ get; set; } //navigation property
    }
}
