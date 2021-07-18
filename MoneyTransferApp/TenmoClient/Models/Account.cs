using System;
using System.Collections.Generic;
using System.Text;

namespace TenmoClient.Models
{
    public class PrivateAccount
    {
        public int AccountId { get; set; }
        public int UserId { get; set; }
        public decimal Balance { get; set; }
    }

    public class PublicAccount
    {
        public int AccountId { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
    }
}
