namespace SNUGGLEINN_CASESTUDY.DTOs
{
    public class UserLoginDto
    {
        public string Email { get; set; } // Email address of the user
        public string Password { get; set; } // Password for the user account
        public string Role { get; set; } // Role of the user (e.g., "Admin", "User", "Guest")
    }
}
