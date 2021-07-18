using System.Collections.Generic;
using TenmoServer.Models;

namespace TenmoServer.DAO
{
    public interface IUserDAO
    {
        User GetUser(string username);
        DisplayUser GetUserById(int id);
        User AddUser(string username, string password);
        List<DisplayUser> GetUsers();
    }
}
