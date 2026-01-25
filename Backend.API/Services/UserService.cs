using Backend.API.Models;
using Backend.API.Data; // your AppDbContext
using Microsoft.EntityFrameworkCore;
using Backend.API.Services;
using System;
using System.Threading.Tasks;

namespace Backend.API.Services
{
    public class UserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        // Add new user
        public async Task AddUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        // Get user by email or number
        public async Task<User> GetUserByEmailOrNumberAsync(string emailOrNumber)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Email == emailOrNumber || u.Number == emailOrNumber);
        }

        // Verify user
        public async Task VerifyUserAsync(string emailOrNumber)
        {
            var user = await GetUserByEmailOrNumberAsync(emailOrNumber);
            if (user != null)
            {
                user.IsVerified = true;
                user.OTP = string.Empty;
                await _context.SaveChangesAsync();
            }
        }

        // Update OTP
        public async Task UpdateOTPAsync(string emailOrNumber, string otp)
        {
            var user = await GetUserByEmailOrNumberAsync(emailOrNumber);
            if (user != null)
            {
                user.OTP = otp;
                await _context.SaveChangesAsync();
            }
        }
        // Update any user (OTP, expiry, role, etc.)
public async Task UpdateUserAsync(User user)
{
    _context.Users.Update(user);
    await _context.SaveChangesAsync();
}

    }
}
