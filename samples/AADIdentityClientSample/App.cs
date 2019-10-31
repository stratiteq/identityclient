// Copyright (c) Stratiteq Sweden AB. All rights reserved.
//
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Threading.Tasks;

namespace AADIdentityClientSample
{
    public class App
    {
        private readonly IService1WebApi service1WebApi;

        public App(IService1WebApi service1WebApi)
        {
            this.service1WebApi = service1WebApi ?? throw new ArgumentNullException(nameof(service1WebApi));
        }

        public async Task RunAsync()
        {
            await this.service1WebApi.ExecuteAsync();
        }
    }
}
