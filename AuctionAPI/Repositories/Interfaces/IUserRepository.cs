using AuctionAPI.Models;

namespace AuctionAPI.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> GetUsers();
        Task<List<Seller>> GetSellers();
        Task<User> GetById(int id);
        Task<Seller> GetSellerById(int id);
        Task AddUser(User user);
        void UpdateUser(User user);
        void DeleteUser(User user);
        Task Save();
        bool UserExist(int id);
        bool SellerExist(int id);
        
    }
}
