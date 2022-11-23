using JwtAuth.Models;

namespace JwtAuth.App_Repositories;


public interface IJwtRepo
{
    User createUser(User user);
    User updateUser(User user);
    public User Delete(string userId);
    Tokens Authenticate(User user);
    bool login(User loginObj);

}
