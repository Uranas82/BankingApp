using Domain.Clients.Firebase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Clients.Firebase
{
    public interface IFirebaseClient
    {
        Task<SignUpResponse> SignUpAsync(string email, string password);

        Task<SignInResponse> SignInAsync(string email, string password);
    }
}
