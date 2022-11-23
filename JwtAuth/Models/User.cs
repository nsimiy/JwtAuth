using System.ComponentModel.DataAnnotations;

namespace JwtAuth.Models
{
    public class User
    {
        [Key]
        public string userId { get; set; }
        public string username { get; set; }
        public string password { get; set; }

    }
}
