using Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ISqlClient _sqlClient;
        private const string TableName = "account";

        public AccountRepository(ISqlClient sqlClient)
        {
            _sqlClient = sqlClient;
        }

        public Task<int> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AccountReadModel>> GetAllAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<AccountReadModel> GetAsync(Guid id)
        {
            var sql = $"SELECT * FROM {TableName} WHERE Id = @Id";

            return _sqlClient.QuerySingleOrDefaultAsync<AccountReadModel>(sql, new 
            {
                Id = id 
            });
        }

        public Task<int> SaveOrUpdateAsync(AccountWriteModel model)
        {
            var sql = @$"INSERT INTO {TableName} (Id, UserId, Balance, Bankname, Currency, DateCreated) 
                        VALUES (@Id, @UserId, @Balance, @Bankname, @Currency, @DateCreated)
                        ON DUPLICATE KEY UPDATE Balance = @Balance, Bankname = @Bankname, Currency = @Currency";

            return _sqlClient.ExecuteAsync(sql, new
            {
                model.Id,
                model.UserId,
                model.Balance,
                model.Bankname,
                Currency = model.Currency.ToString(),
                model.DateCreated
            });
        }
    }
}
