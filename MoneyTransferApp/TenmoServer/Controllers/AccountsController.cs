using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenmoServer.DAO;
using TenmoServer.Models;

namespace TenmoServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]

    public class AccountsController : ControllerBase
    {
        private readonly IAccountDAO accountDAO;
        private readonly ITransferDAO transferDAO;

        public AccountsController(IAccountDAO _accountDAO, ITransferDAO _transferDAO)
        {
            accountDAO = _accountDAO;
            transferDAO = _transferDAO;
        }

        [HttpGet]
        public ActionResult<List<PublicAccount>> GetAllAccounts()
        {
            List<PublicAccount> accounts = accountDAO.GetAccounts();
            return accounts;
        }

        [HttpGet("{userId}")]
        public ActionResult<PrivateAccount> GetAccountById(int userId)
        {
            if (UserHasCorrectID(userId))
            {
                PrivateAccount account = accountDAO.GetAccount(userId);
                if (account != null)
                {
                    return Ok(account);
                }
                else
                {
                    return NotFound();
                }
            }
            else
                return Forbid();
        }


        [HttpGet("{userId}/transfers")]
        public ActionResult<List<Transfer>> GetAllTransfers(int userId)
        {
            if (UserHasCorrectID(userId))
            {
                List<Transfer> transfers = transferDAO.GetTransfers(userId);
                if (transfers != null)
                {
                    return Ok(transfers);
                }
                else
                {
                    return NotFound();
                }
            }
            else
                return Forbid();
        }

        [HttpGet("{userId}/transfers/pending")]
        public ActionResult<List<Transfer>> GetPendingTransfers(int userId)
        {
            if (UserHasCorrectID(userId))
            {
                List<Transfer> transfers = transferDAO.GetPendingTransfers(userId);
                if (transfers != null)
                {
                    return Ok(transfers);
                }
                else
                {
                    return NotFound();
                }
            }
            else
                return Forbid();
        }

        [HttpGet("{userId}/transfers/{id}")]
        public ActionResult<Transfer> GetTransferById(int userId, int id)
        {
            if (UserHasCorrectID(userId))
            {
                Transfer transfer = transferDAO.GetTransfer(userId, id);
                if (transfer != null)
                {
                    return Ok(transfer);
                }
                else
                {
                    return NotFound();
                }
            }
            else
                return Forbid();
        }

        [HttpPost("{userId}/transfers")]
        public ActionResult<Transfer> CreateTransfer(int userId, Transfer transfer)
        {
            int initializerID = transfer.TransferTypeId == 2 ? transfer.AccountFrom : transfer.AccountTo;
            if (UserHasCorrectID(initializerID))
            {
                try
                {
                    Transfer created = transferDAO.CreateTransfer(userId, transfer);
                    return Created($"[controller]/{userId}/transfers/{created.TransferId}", created);
                }
                catch (System.Exception e)
                {
                    return BadRequest(e);
                }
            }
            else
                return Forbid();
        }

        [HttpPut("{userId}/transfers/{transferId}")]
        public ActionResult<Transfer> UpdateTransferStatus(int userId, Transfer transfer)
        {
            if (UserHasCorrectID(userId))
            {
                Transfer pending = transferDAO.GetTransfer(userId, transfer.TransferId);
                if (pending == null)
                {
                    return NotFound();
                }
                Transfer updated = transferDAO.UpdateTransferStatus(userId, transfer);
                return Ok(updated);
            }
            else
                return Forbid();
        }
        /// <summary>
        /// Helper function to ensure that users can only get sensitive info about their account.
        /// Prevents looking up sensitive info using Postman etc..
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool UserHasCorrectID(int userId)
            => userId == int.Parse(User.Claims.Where(x => x.Type == "sub").First().Value);

    }
}
