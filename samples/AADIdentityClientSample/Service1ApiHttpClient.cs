// Copyright (c) Stratiteq Sweden AB. All rights reserved.
//
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace AADIdentityClientSample
{
    public class Service1ApiHttpClient : IService1ApiHttpClient
    {
        private readonly HttpClient httpClient;

        public Service1ApiHttpClient(HttpClient httpClient)
        {
            this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<string> GetAsync()
        {
            HttpResponseMessage response = await this.httpClient.GetAsync("api/v1/protected");
            response.EnsureSuccessStatusCode();
            return JsonSerializer.Deserialize<string>(await response.Content.ReadAsStringAsync());
        }
    }
}
