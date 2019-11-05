// Copyright (c) Stratiteq Sweden AB. All rights reserved.
//
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.Extensions.Logging;
using Stratiteq.Identity.Client.AAD;

namespace AADIdentityClientSample
{
    public class Service1IdentityClient : IdentityClientBase
    {
        public Service1IdentityClient(AADAppConfiguration aadAppConfiguration, ILogger<Service1IdentityClient> logger)
            : base(aadAppConfiguration, logger)
        {
        }
    }
}
