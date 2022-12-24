using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ScrumBoard.Common.HttpClients;

namespace ScrumBoard.Common.Helpers
{
    public static class SetupHttpClients
    {
        public static IServiceCollection AddNamedHttpClients(this IServiceCollection serviceCollection, ConfigurationManager configManager)
        {
            var config = configManager.GetSection("NamedHttpClients").Get<NamedHttpClients>();

            if (config.Backend is null || config.Authorization is null)
            {
                throw new ArgumentException("Failed to get the configuration of the http clients");
            }

            serviceCollection.AddHttpClient(HttpClientNames.Backend, httpClient =>
            {
                httpClient.BaseAddress = new Uri(config.Backend.BaseUrl);
            });

            serviceCollection.AddHttpClient(HttpClientNames.Authorization, httpClient =>
            {
                httpClient.BaseAddress = new Uri(config.Authorization.BaseUrl);
            });

            return serviceCollection;
        }
    }
}
