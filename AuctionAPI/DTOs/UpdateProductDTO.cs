namespace AuctionAPI.DTOs
{
    public class UpdateProductDTO
    {
        public string? Name { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public int? CategoryId { get; set; } = null;
        public float? StarttingPrice { get; set; } = null;

    }
}
