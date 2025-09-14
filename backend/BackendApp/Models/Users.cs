namespace BackendApp.Models
{
    public class Users
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;

        public int RoleId { get; set; }
        public Roles Role { get; set; }  // FK

        public bool IsLocked { get; set; } = false;

        public bool MustChangePassword { get; set; } = true; // zmiana hasła przy pierwszym logowaniu

        public int FailedLoginAttempts { get; set; } = 0; // licznik nieudanych prób
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
