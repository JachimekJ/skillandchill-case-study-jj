using BackendApp.DTOs;
using BackendApp.Models;

namespace BackendApp.Services
{
    public interface IAuthService
    {
        Task<UserResponseDto?> Login(UserLoginDto loginDto);
        Task<bool> ChangePassword(int userId, string oldPassword, string newPassword);
        Task<bool> LockAccount(int userId);
        Task<bool> UnlockAccount(int userId, int performedByUserId);
    }
}