using System.ComponentModel.DataAnnotations;

namespace AuctionAPI.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
        [StringLength(100)]
        public string Description { get; set; } = string.Empty;
    }
}