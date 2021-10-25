using Domain.Clients.Firebase;
using Domain.Clients.Firebase.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddDomain(this IServiceCollection services)
        {
            return services
                .AddClients();
        }

        private static IServiceCollection AddClients(this IServiceCollection services)
        {
            services.AddHttpClient<IFirebaseClient, FirebaseClient>();

            return services;
        }

    }

}
