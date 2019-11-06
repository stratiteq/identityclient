// Copyright (c) Stratiteq Sweden AB. All rights reserved.
//
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Threading.Tasks;

namespace AADIdentityClientSample
{
    public class Service1WebApi : IService1WebApi
    {
        private readonly IService1ApiHttpClient service1ApiHttpClient;

        public Service1WebApi(IService1ApiHttpClient service1ApiHttpClient)
        {
            this.service1ApiHttpClient = service1ApiHttpClient;
        }

        public async Task ExecuteAsync()
        {
            try
            {
                await this.service1ApiHttpClient.GetAsync();
            }
            catch (Exception)
            {
            }
        }
    }
}
