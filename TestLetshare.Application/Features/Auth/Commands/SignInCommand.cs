namespace TestLetshare.Application.Features.Auth.Commands
{
    public class SignInCommand
    {
        public string grantType { get; set; } = string.Empty;
        public string clientId { get; set; } = string.Empty;
        public string clientSecret { get; set; } = string.Empty;
        public string username { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;
    }
}
