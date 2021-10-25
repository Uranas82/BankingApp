using Contracts.Models.Request;
using Contracts.Models.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Persistence.Models;
using Persistence.Repositories;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace RestAPI.Controllers
{
    [ApiController]
    [Route("account")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IUsersRepository _usersRepository;
        private readonly ITransactionsRepository _transactionsRepository;

        public AccountController(IAccountRepository accountRepository, IUsersRepository usersRepository, ITransactionsRepository transactionsRepository)
        {
            _accountRepository = accountRepository;
            _usersRepository = usersRepository;
            _transactionsRepository = transactionsRepository;
        }

        [HttpGet]
        [Route("balance/{id}")]
        [Authorize]
        public async Task<ActionResult<AccountBalanceResponse>> GetAccountBalance(Guid id)
        {
            //var firebaseId = HttpContext.User.Claims.SingleOrDefault(claim => claim.Type == "user_id").Value;

            //var user = await _usersRepository.GetAsync(firebaseId);

            var account = await _accountRepository.GetAsync(id);

            var transactions = await _transactionsRepository.GetByAccountIdAsync(account.Id);

            var transactionsResponse = transactions.Select(transaction => new TransactionResponse
            {
                Id = transaction.Id,
                UserId = transaction.UserId,
                TransactionTypes = transaction.TransactionTypes,
                Amount = transaction.Amount,
                Description = transaction.Description,
                DateCreated = transaction.DateCreated
            });


            return Ok(new AccountBalanceResponse
            {
                Id = account.Id,
                UserId = account.Id,
                Balance = account.Balance,
                Bankname = account.Bankname,
                Currency = account.Currency,
                DateCreated = account.DateCreated,
                Transactions = transactionsResponse
            });
        }


        [HttpPost]
        [Route("createAccount")]
        [Authorize]
        public async Task<ActionResult<CreateAccountResponse>> CreateAccount(CreateAccountRequest request)
        {
            var firebaseId = HttpContext.User.Claims.SingleOrDefault(claim => claim.Type == "user_id").Value;

            var user = await _usersRepository.GetAsync(firebaseId);

            var account = new AccountWriteModel
            {
                Id = Guid.NewGuid(),
                UserId = user.UserId,
                Balance = 0,
                Bankname = request.Bankname,
                Currency = request.Currency,
                DateCreated = DateTime.Now
            };

            await _accountRepository.SaveOrUpdateAsync(account);

            return Ok(new CreateAccountResponse
            {
                Id = account.Id,
                UserId = account.UserId,
                Balance = 0,
                Bankname = account.Bankname,
                Currency = account.Currency,
                DateCreated = account.DateCreated
            });
        }


    }
}
