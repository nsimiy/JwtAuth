using JwtAuth.App_Repositories;
using JwtAuth.Data;
using JwtAuth.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JwtAuth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IJwtRepo _jwtRepo;
        private readonly JwtDbContext _jwtDbContext;
        private readonly IConfiguration _configuration;

        public UserController(IJwtRepo jwtRepo, JwtDbContext jwtDbContext, IConfiguration configuration)
        {
            _jwtRepo = jwtRepo;
            _jwtDbContext = jwtDbContext;
            _configuration = configuration;
        }

        [HttpPost("createUser")]
        public IActionResult createUser([FromBody] User user)
        {
            Response response = new Response();

            var addUser = _jwtRepo.createUser(user);
            if(addUser !=null)
            {
                response.Status = true;
                response.Message = "successful";
                return new OkObjectResult(response);
            }
            else
            {
                response.Status = false;
                response.Message = "failed";
                return BadRequest(response);
            }
        }

        [HttpDelete("deleteUser")]
        public IActionResult delete(string userId)
        {
            Response response = new Response();
            try 
            {
                _jwtRepo.Delete(userId);
                response.Status = true;
                response.Message = "success";
                return new OkObjectResult(response);
            }
            catch
            {
                response.Status = false;
                response.Message = "Error deleting user";
                return BadRequest(response);
            } 
        }

        [HttpPut("updateUser")]
        public IActionResult updateUser([FromBody] User user)
        {
            Response response = new Response();
            _jwtRepo.updateUser(user);
            response.Status = true;
            response.Message = "Update Successful";
            return new OkObjectResult(response);
        }

        [HttpPost("login")]
        public IActionResult login(User loginObj)
        {
            var user = _jwtDbContext.fire.Where(x => x.userId.Equals(loginObj.userId) && x.password.Equals(loginObj.password)).FirstOrDefault();
            if (user == null)
            {
                return Unauthorized();
            }

            var claims = new[]
                   {
                      new Claim ("userId", user.userId!=null?user.userId:""),
                      new Claim ("username", user.username!=null?user.username:""),
                      new Claim ("password", user.password!=null?user.password:""),

                    };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:key"]));
            var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(20),
              signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                );
            return Ok(new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}
