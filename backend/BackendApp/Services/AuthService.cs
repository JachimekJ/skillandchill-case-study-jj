using BackendApp.Data;
using BackendApp.DTOs;
using BackendApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BackendApp.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;

        public AuthService(AppDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public async Task<AuthResponseDto?> Login(UserLoginDto loginDto)
        {
            var user = await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Username == loginDto.Username);

            if (user == null) return null;

            if (user.IsLocked)
            {
                await LogActivity(user.Id, "Login attempt on locked account");
                return null;
            }

            // ✅ sprawdzamy, czy użytkownik musi zmienić hasło przy pierwszym logowaniu
            if (user.MustChangePassword)
            {
                await LogActivity(user.Id, "Login requires password change");
                return new AuthResponseDto
                {
                    Id = user.Id,
                    Username = user.Username,
                    Role = user.Role.Name,
                    Token = GenerateJwtToken(user)
                };
            }

            if (!BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
            {
                // zapisujemy nieudaną próbę
                _context.LoginAttempts.Add(new LoginAttempts
                {
                    UserId = user.Id,
                    AttemptTime = DateTime.UtcNow,
                    IsSuccessful = false
                });

                await LogActivity(user.Id, "Failed login attempt");

                var fails = await _context.LoginAttempts
                    .Where(a => a.UserId == user.Id && !a.IsSuccessful)
                    .OrderByDescending(a => a.AttemptTime)
                    .Take(3)
                    .CountAsync();

                if (fails >= 3)
                {
                    user.IsLocked = true;
                    await LogActivity(user.Id, "Account locked due to 3 failed attempts");
                }

                await _context.SaveChangesAsync();
                return null;
            }

            // udane logowanie
            _context.LoginAttempts.Add(new LoginAttempts
            {
                UserId = user.Id,
                AttemptTime = DateTime.UtcNow,
                IsSuccessful = true
            });

            await LogActivity(user.Id, "User logged in");

            await _context.SaveChangesAsync();

            var token = GenerateJwtToken(user);

            return new AuthResponseDto
            {
                Id = user.Id,
                Username = user.Username,
                Role = user.Role.Name,
                Token = token
            };
        }

        public async Task<bool> ChangePassword(int userId, string oldPassword, string newPassword)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null) return false;

            if (!BCrypt.Net.BCrypt.Verify(oldPassword, user.PasswordHash))
                return false;

            // polityka haseł
            if (!ValidatePasswordPolicy(newPassword))
                return false;

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);
            user.MustChangePassword = false; // wyłączamy wymóg zmiany
            await _context.SaveChangesAsync();

            await LogActivity(user.Id, "Password changed");
            return true;
        }

        public async Task<bool> LockAccount(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null) return false;

            user.IsLocked = true;
            await _context.SaveChangesAsync();

            await LogActivity(user.Id, "Account locked manually");
            return true;
        }

        public async Task<bool> UnlockAccount(int userId, int performedByUserId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null) return false;

            user.IsLocked = false;
            await _context.SaveChangesAsync();

            await LogActivity(user.Id, $"Account unlocked by {performedByUserId}");
            return true;
        }

        // --- helper: walidacja polityki haseł ---
        private bool ValidatePasswordPolicy(string password)
        {
            if (password.Length < 8) return false;
            if (!password.Any(char.IsDigit)) return false;
            if (!password.Any(ch => !char.IsLetterOrDigit(ch))) return false;
            return true;
        }

        // --- helper: generowanie JWT ---
        private string GenerateJwtToken(Users user)
        {
            var jwtSettings = _config.GetSection("Jwt");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim("id", user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role.Name)
            };

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(int.Parse(jwtSettings["ExpireMinutes"] ?? "60")),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        // --- helper: rejestracja aktywności ---
        private async Task LogActivity(int userId, string action)
        {
            _context.ActivityLogs.Add(new ActivityLogs
            {
                UserId = userId,
                Action = action,
                Timestamp = DateTime.UtcNow
            });
            await _context.SaveChangesAsync();
        }
    }
}