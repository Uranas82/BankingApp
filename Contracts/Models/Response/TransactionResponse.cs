using System;
using Contracts.Enums;

namespace Contracts.Models.Response
{
    public class TransactionResponse
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public TransactionType TransactionTypes { get; set; }

        public decimal Amount { get; set; }

        public string Description { get; set; }

        public DateTime DateCreated { get; set; }
    }
}
