using Contracts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Models
{
    public class TransactionReadModel
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public Guid AccountId { get; set; }

        public Guid CounterpartyId { get; set; }

        public TransactionType TransactionTypes { get; set; }

        public decimal Amount { get; set; }

        public string Description { get; set; }

        public DateTime DateCreated { get; set; }
    }
}
