﻿using Contracts.Enums;
using Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public interface ITransactionsRepository
    {
        Task<IEnumerable<TransactionReadModel>> GetAllAsync(Guid userId);

        Task<IEnumerable<TransactionReadModel>> GetByAccountIdAsync(Guid accountId);

        Task<TransactionReadModel> GetAsync(Guid id);

        Task<IEnumerable<TransactionReadModel>> GetAsync(TransactionType transaction, Guid userId);

        Task<int> SaveOrUpdateAsync(TransactionWriteModel model);

        Task<int> DeleteAsync(Guid id);
        Task<int> DeleteByAccountIdAsync(Guid accountId);
    }
}
