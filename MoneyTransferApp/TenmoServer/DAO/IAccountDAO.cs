using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenmoServer.Models;

namespace TenmoServer.DAO
{
    public interface IAccountDAO
    {
        PrivateAccount GetAccount(int id);
        List<PublicAccount> GetAccounts();
    }
}
