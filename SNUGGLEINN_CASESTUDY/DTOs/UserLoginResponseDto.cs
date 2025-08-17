namespace SNUGGLEINN_CASESTUDY.DTOs
{
    public class UserLoginResponseDto
    {
        public string Token { get; set; }
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string PhoneNumber { get; set; }
    }
}
