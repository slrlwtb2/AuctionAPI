using System.Text.Json.Serialization;

namespace AuctionAPI.Models
{
    public class Seller : User
    {
        public float Rating { get; set; }
        public int RatingCount { get; set; } = 0;
        public override bool Bidable { get => base.Bidable; set => base.Bidable = false; }

        [JsonIgnore]
        public List<Product> Products { get; set; } // navigation property

        public float GetRating()
        {
            if(RatingCount>0) return Rating/RatingCount; else return 0;
        }
    }
}