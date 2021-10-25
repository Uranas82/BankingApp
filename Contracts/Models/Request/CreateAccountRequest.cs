using Contracts.Enums;

namespace Contracts.Models.Request
{
    public class CreateAccountRequest
    {
        public string Bankname { get; set; }

        public Currency Currency { get; set; }
    }
}
