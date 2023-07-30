using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AuctionAPI.Models
{
    public class Seller : User
    {
        [Range(1,5)]
        public float Rating { get; set; }
        public int RatingCount { get; set; } = 0;
        public override bool Bidable { get => base.Bidable; set => base.Bidable = false; }

        [JsonIgnore]
        public List<Product> Products { get; set; } // navigation property

        public float GetRating()
        {
            float rating = Rating / RatingCount;
            float ratingfloor = (float)Math.Floor(rating * 10) / 10;
            if (RatingCount > 0) return ratingfloor; else return 0;
        }
    }
}