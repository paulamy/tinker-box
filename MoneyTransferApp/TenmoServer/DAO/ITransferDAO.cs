using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenmoServer.Models;

namespace TenmoServer.DAO
{
    public interface ITransferDAO
    {
        Transfer GetTransfer(int userId, int id);
        
        List<Transfer> GetTransfers(int userId);

        
        Transfer CreateTransfer(int userId, Transfer transfer);


        Transfer UpdateTransferStatus(int userId, Transfer transfer);

        List<Transfer> GetPendingTransfers(int userId);
    }
}
