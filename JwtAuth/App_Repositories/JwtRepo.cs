using JwtAuth.Data;
using JwtAuth.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JwtAuth.App_Repositories
{
    public class JwtRepo : IJwtRepo
    {
        private readonly JwtDbContext _jwtDbContext;
        public JwtRepo(JwtDbContext jwtDbContext)
        {
            _jwtDbContext = jwtDbContext;
        }

        public Tokens Authenticate(User user)
        {
            throw new NotImplementedException();
        }

        public User createUser(User user)
        {
            _jwtDbContext.fire.Add(user);
            _jwtDbContext.SaveChanges();
            return user;
        }

        public User Delete(string userId)
        {
            var delete = _jwtDbContext.fire.Find(userId);
            _jwtDbContext.fire.Remove(delete);
            _jwtDbContext.SaveChanges();
            return delete;
        }


        public bool login(User loginObj)
        {
            var login = _jwtDbContext.fire.Find(loginObj);
            _jwtDbContext.SaveChanges();
            return true;
        }

        public User updateUser(User register)
        {
            var user = _jwtDbContext.fire.FirstOrDefault(x => x.userId == register.userId);
            if (user != null)
            {
                user.username = register.username;
                user.password = register.password;

                _jwtDbContext.SaveChanges();
                return user;
            }
            return null;
        }

    }
}
