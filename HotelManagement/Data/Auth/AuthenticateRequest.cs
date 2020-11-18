namespace HotelManagement.Data.Auth
{
    /// <summary>
    /// Administrator's email and password
    /// </summary>
    public class AuthenticateRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
