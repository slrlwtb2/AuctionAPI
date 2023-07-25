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
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        [Required]
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        [Required]
        [ForeignKey("Seller")]
        public int SellerId { get; set; }
        public float StartingPrice { get; set; }
        public float CurrentBid { get; set; }
        public DateTime BidEndTime { get; set; }


        [JsonIgnore]
        public Seller Seller { get; set; } //navigation property
        [JsonIgnore]
        public Category Category{ get; set; } //navigation property
    }
}
