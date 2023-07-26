using AuctionAPI.Models;

namespace AuctionAPI.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> GetUsers();
        Task<User> GetById(int id);
        Task AddUser(User user);
        void UpdateUser(User user);
        void DeleteUser(User user);
        Task Save();
        bool UserExist(int id);
    }
}
