namespace BackendApp.Models
{
    public class LoginAttempts
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public Users User { get; set; }

        public DateTime AttemptTime { get; set; } = DateTime.UtcNow;
        public bool IsSuccessful { get; set; }
    }
}
