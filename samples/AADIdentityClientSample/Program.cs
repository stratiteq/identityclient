// Copyright (c) Stratiteq Sweden AB. All rights reserved.
//
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace AADIdentityClientSample
{
    public static class Program
    {
        private static IServiceProvider? serviceProvider;

        public static async Task Main(string[] args)
        {
            RegisterServices(args);
            App app = serviceProvider.GetService<App>();

            await RunAsync(app.RunAsync);

            DisposeServices();
        }

        private static void RegisterServices(string[] args)
        {
            var serviceCollection = new ServiceCollection();

            var startup = new Startup();
            startup.ConfigureServices(serviceCollection);

            startup.Configure();

            serviceProvider = serviceCollection.BuildServiceProvider();
        }

        private static async Task RunAsync(Func<Task> func)
        {
            try
            {
                await func.Invoke();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static void DisposeServices()
        {
            if (serviceProvider == null)
            {
                return;
            }

            if (serviceProvider is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }
    }
}
