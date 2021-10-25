using Contracts.Enums;
using System;

namespace Contracts.Models.Response
{
    public class CreateAccountResponse
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public decimal Balance { get; set; }

        public string Bankname { get; set; }

        public Currency Currency { get; set; }

        public DateTime DateCreated { get; set; }
    }
}
