// Copyright (c) Stratiteq Sweden AB. All rights reserved.
//
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Stratiteq.Identity.Client;
using Stratiteq.Identity.Client.AAD;
using System.IO;

namespace AADIdentityClientSample
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(configure =>
                configure
                    .AddDebug()
                    .AddConsole());

            // Build config
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            // Add access to generic IConfigurationRoot
            services.AddSingleton<IConfiguration>(configuration);

            // Add services
            services.AddTransient<App>();

            services.AddSingleton<Service1IdentityClient>(serviceProvider =>
                new Service1IdentityClient(
                    new AADAppConfiguration(configuration.GetSection("Service1")),
                    serviceProvider.GetService<ILogger<Service1IdentityClient>>()));

            // These need to be transient so that a new instance is provided each time a new HttpClient is created.
            services.AddTransient<AuthenticatingDelegatingHandler<Service1IdentityClient>>();

            services.AddHttpClient<IService1ApiHttpClient, Service1ApiHttpClient>(client =>
            {
                client.BaseAddress = new System.Uri("https://localhost:44303/");
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            }).AddHttpMessageHandler<AuthenticatingDelegatingHandler<Service1IdentityClient>>();

            services.AddSingleton<IService1WebApi, Service1WebApi>();
        }

        public void Configure()
        {
        }
    }
}
