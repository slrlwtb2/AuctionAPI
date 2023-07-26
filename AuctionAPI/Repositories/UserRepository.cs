﻿using AuctionAPI.Data;
using AuctionAPI.Models;
using AuctionAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AuctionAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
                _context= context;
        }
        public async Task AddUser(User user)
        {
            await _context.Users.AddAsync(user);
        }

        public void DeleteUser(User user)
        {
            _context.Users.Remove(user);
        }

        public async Task<User> GetById(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
            return user;
        }

        public async Task<Seller> GetSellerById(int id)
        {
            var seller = await _context.Users.OfType<Seller>().FirstOrDefaultAsync(s => s.Id == id);
            return seller;
        }

        public async Task<List<Seller>> GetSellers()
        {
            var sellers =  await _context.Users.OfType<Seller>().ToListAsync();
            return sellers;
        }

        public async Task<List<User>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public bool SellerExist(int id)
        {
            Seller? seller = _context.Users.OfType<Seller>().FirstOrDefault(x => x.Id == id);
            if (seller == null) { return false; }
            return true;
        }

        public void UpdateUser(User user)
        {
            _context.Users.Update(user);
        }

        public bool UserExist(int id)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == id);
            if(user == null) { return false; }
            return true;
        }
    }
}
