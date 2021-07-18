﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TenmoClient.Models
{
    class Transfer
    {
        public int TransferID { get; set; }
        public int TransferTypeID { get; set; }
        public int TransferStatusID { get; set; }
        public int AccountFrom { get; set; }
        public int AccountTo { get; set; }
        public decimal Amount { get; set; }
        public string TransferTypeDescription { get; set; }

        public string TransferStatusDescription { get; set; }

    }

    
}
