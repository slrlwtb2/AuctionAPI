using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AuctionAPI.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Username { get; set; } = string.Empty;
        [EmailAddress]
        public string? Email { get; set; }
        private float Balance { get; set; } = 0;
        public virtual bool Bidable { get; set; } = true;
        public DateTime RegisterationDate { get; set; }

        public float GetBalance()
        {
            return Balance;
        }
        public void IncreaseBalance(float value)
        {
            if(value>=0) Balance += value;
            else
            {
                throw new Exception("Value cannot be less than zero");
            }
        }
        public void DecreaseBalance(float value) 
        {
            if (value>=0)
            {
                if (value > Balance) throw new Exception("Value cannot be more than current balnace");
                Balance -= value;
            }
            else
            {
                throw new Exception("Value cannot be less than zero");
            }
        }
    }
}
