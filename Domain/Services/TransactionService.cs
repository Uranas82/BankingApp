using Contracts.Models.Request;
using Contracts.Models.Response;
using Domain.Exceptions;
using Persistence.Models;
using Persistence.Repositories;
using System;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IUsersRepository _usersRepository;
        private readonly ITransactionsRepository _transactionsRepository;

        public TransactionService(IAccountRepository accountRepository, IUsersRepository usersRepository, ITransactionsRepository transactionsRepository)
        {
            _accountRepository = accountRepository;
            _usersRepository = usersRepository;
            _transactionsRepository = transactionsRepository;
        }

        public async Task<TransactionResponse> TopUp(TransactionRequest request, string firebaseId)
        {
            var user = await _usersRepository.GetAsync(firebaseId);
            var account = await _accountRepository.GetAsync(request.AccountId);
            if (account.Balance + request.Amount < 0)
            {
                throw new UserException($"There are not enough funds in the account!", 404);
            }
            var transactionWriteModel = new TransactionWriteModel
            {
                Id = Guid.NewGuid(),
                UserId = user.UserId,
                AccountId = request.AccountId,
                TransactionTypes = request.TransactionTypes,
                Amount = request.Amount,
                Description = request.Description,
                DateCreated = DateTime.Now
            };

            var accountWriteModel = new AccountWriteModel
            {
                Id = account.Id,
                UserId = account.UserId,
                Balance = account.Balance + request.Amount,
                Bankname = account.Bankname,
                Currency = account.Currency,
                DateCreated = account.DateCreated
            };

            await _transactionsRepository.SaveOrUpdateAsync(transactionWriteModel);
            await _accountRepository.SaveOrUpdateAsync(accountWriteModel);

            return new TransactionResponse
            {
                Id = transactionWriteModel.Id,
                UserId = transactionWriteModel.UserId,
                TransactionTypes = transactionWriteModel.TransactionTypes,
                Amount = transactionWriteModel.Amount,
                Description = transactionWriteModel.Description,
                DateCreated = transactionWriteModel.DateCreated
            };
        }
    }
}
