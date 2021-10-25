using Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public interface IUsersRepository
    {
        Task<int> SaveAsync(UserWriteModel model);

        Task<UserReadModel> GetAsync(string firebaseId);

        // Task<int> DeleteAsync();
    }
}
