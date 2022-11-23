using Microsoft.EntityFrameworkCore;

namespace JwtAuth.Models
{
    public class Tokens
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
