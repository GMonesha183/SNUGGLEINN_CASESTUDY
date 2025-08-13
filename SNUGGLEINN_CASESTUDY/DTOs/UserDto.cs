namespace SNUGGLEINN_CASESTUDY.DTOs
{
    public class UserDto
    {
        public int UserId { get; set; } // Unique identifier for the user
        public string FullName { get; set; } // Full name of the user
        public string Email { get; set; } // Email address of the user
        public string Password { get; set; } // Password for the user account
        public string Role { get; set; } // Role of the user (e.g., "Admin", "User", "Guest")
        public string PhoneNumber { get; set; } // Phone number of the user
    }
}
