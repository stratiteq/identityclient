// Copyright (c) Stratiteq Sweden AB. All rights reserved.
//
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;
using Stratiteq.Identity.Client.Abstractions;
using Stratiteq.Identity.Client.Extensions;
using Stratiteq.Microservices.X509Certificate;
using System;
using System.Threading.Tasks;

namespace Stratiteq.Identity.Client.AAD
{
    /// <summary>
    /// An IIdentityClient implementation using Microsoft.Identity.Client to authenticate against.
    /// </summary>
    public class IdentityClientBase : IIdentityClient
    {
        private readonly AADAppConfiguration aadAppConfiguration;
        private readonly ILogger<IdentityClientBase> logger;
        private IConfidentialClientApplication? confidentialClientApplication;

        /// <summary>
        /// Initializes a new instance of the <see cref="IdentityClientBase"/> class.
        /// </summary>
        public IdentityClientBase(AADAppConfiguration aadAppConfiguration, ILogger<IdentityClientBase> logger)
        {
            this.aadAppConfiguration = aadAppConfiguration ?? throw new ArgumentNullException(nameof(aadAppConfiguration));
            this.logger = logger;
        }

        /// <inheritdoc/>
        public async Task<string> RequestTokenAsync()
        {
            this.logger?.LogInformation("Requesting token from identity provider.");

            if (this.confidentialClientApplication == null)
            {
                ValidationResult validationResult = this.aadAppConfiguration.ValidationResult();
                if (!validationResult.Success)
                {
                    throw new ArgumentException(validationResult.Message);
                }

                if (!string.IsNullOrEmpty(this.aadAppConfiguration.CertificateSubjectName))
                {
                    this.confidentialClientApplication = ConfidentialClientApplicationBuilder.Create(this.aadAppConfiguration.ClientId)
                        .WithCertificate(CertificateFinder.FindBySubjectName(this.aadAppConfiguration.CertificateSubjectName, DateTime.UtcNow))
                        .WithAuthority(AzureCloudInstance.AzurePublic, this.aadAppConfiguration.TenantId)
                        .Build();
                }
                else
                {
                    this.confidentialClientApplication = ConfidentialClientApplicationBuilder.Create(this.aadAppConfiguration.ClientId)
                        .WithClientSecret(this.aadAppConfiguration.ClientSecret)
                        .WithAuthority(AzureCloudInstance.AzurePublic, this.aadAppConfiguration.TenantId)
                        .Build();
                }
            }

            AuthenticationResult result;
            try
            {
                result = await this.confidentialClientApplication.AcquireTokenForClient(this.aadAppConfiguration.Scopes)
                    .ExecuteAsync();

                this.logger?.LogInformation("Token requested successfully.");
                this.logger?.LogDebug($"Access token: {result.AccessToken}");
                this.logger?.LogDebug($"Expires on  : {result.ExpiresOn}");
                this.logger?.LogDebug($"Scopes      : {string.Join(";", result.Scopes)}");
            }
            catch (MsalException e)
            {
                this.logger?.LogError(e, "Requesting token failed");
                throw;
            }

            return result.AccessToken;
        }
    }
}
