using Contracts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Models.Request
{
    public class TransactionRequest
    {        
        public Guid AccountId { get; set; }

        public TransactionType TransactionTypes { get; set; }

        public decimal Amount { get; set; }

        public string Description { get; set; }
    }
}
