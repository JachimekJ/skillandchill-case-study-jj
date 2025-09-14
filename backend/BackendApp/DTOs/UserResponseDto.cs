namespace BackendApp.DTOs
{
    public class UserResponseDto
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty; // np. "Distributor", "Admin"
    }
}
