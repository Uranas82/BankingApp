using Contracts.Enums;
using Persistence.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class TransactionsRepository : ITransactionsRepository
    {
        private readonly ISqlClient _sqlClient;

        private const string TableName = "transaction";

        public TransactionsRepository(ISqlClient sqlClient)
        {
            _sqlClient = sqlClient;
        }

        public Task<int> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteByAccountIdAsync(Guid accountId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TransactionReadModel>> GetAllAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<TransactionReadModel> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TransactionReadModel>> GetAsync(TransactionType transaction, Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TransactionReadModel>> GetByAccountIdAsync(Guid accountId)
        {
            var sql = $"SELECT * FROM {TableName} WHERE AccountId = @AccountId";

            return _sqlClient.QueryAsync<TransactionReadModel>(sql, new
            {
                AccountId = accountId
            });
        }

        public Task<int> SaveOrUpdateAsync(TransactionWriteModel model)
        {
            var sql = @$"INSERT INTO {TableName} (Id, UserId, AccountId, CounterpartyId, TransactionTypes, Amount, Description, DateCreated) 
                        VALUES (@Id, @UserId, @AccountId, @CounterpartyId, @TransactionTypes, @Amount, @Description, @DateCreated)
                        ON DUPLICATE KEY UPDATE Amount = @Amount, Comment = @Comment";

            return _sqlClient.ExecuteAsync(sql, new
            {
                model.Id,
                model.UserId,
                model.AccountId,
                model.CounterpartyId,
                TransactionTypes = model.TransactionTypes.ToString(),
                model.Amount,
                model.Description,
                model.DateCreated
            });
        }
    }
}
