using BackendApp.Data;
using BackendApp.DTOs;
using BackendApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendApp.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;

        public AuthService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<UserResponseDto?> Login(UserLoginDto loginDto)
        {
            var user = await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Username == loginDto.Username);

            if (user == null) return null;

            // TODO: verify password hash instead of plain password
            if (user.PasswordHash != loginDto.Password)
            {
                // TODO: log failed attempt, lock account after 3 fails
                return null;
            }

            return new UserResponseDto
            {
                Id = user.Id,
                Username = user.Username,
                Role = user.Role.Name
            };
        }

        public async Task<bool> ChangePassword(int userId, string oldPassword, string newPassword)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null) return false;

            if (user.PasswordHash != oldPassword) return false;

            // TODO: hash password before saving
            user.PasswordHash = newPassword;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> LockAccount(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null) return false;

            user.IsLocked = true;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UnlockAccount(int userId, int performedByUserId)
        {
            // TODO: check if performedByUserId has higher role
            var user = await _context.Users.FindAsync(userId);
            if (user == null) return false;

            user.IsLocked = false;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}