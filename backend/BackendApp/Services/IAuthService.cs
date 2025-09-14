using BackendApp.DTOs;

namespace BackendApp.Services
{
    public interface IAuthService
    {
        Task<AuthResponseDto?> Login(UserLoginDto loginDto);
        Task<bool> ChangePassword(int userId, string oldPassword, string newPassword);
        Task<bool> LockAccount(int userId);
        Task<bool> UnlockAccount(int userId, int performedByUserId);
    }
}
