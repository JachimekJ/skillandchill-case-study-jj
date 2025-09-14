namespace BackendApp.Models
{
    public class ActivityLogs
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public Users User { get; set; }

        public string Action { get; set; } = string.Empty; // np. LoginSuccess, LoginFailed, PasswordChanged
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public string? IPAddress { get; set; }
        public string? Details { get; set; } // JSON / text
    }
}