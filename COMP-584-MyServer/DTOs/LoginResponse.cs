namespace COMP_584_MyServer.DTOs
{
    public class LoginResponse
    {
        public bool Success { get; set; }
        public required string Message { get; set; }
        public required string Token { get; set; }
    }
}
