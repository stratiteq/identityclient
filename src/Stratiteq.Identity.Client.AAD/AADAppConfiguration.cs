// Copyright (c) Stratiteq Sweden AB. All rights reserved.
//
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.Extensions.Configuration;

namespace Stratiteq.Identity.Client.AAD
{
    /// <summary>
    /// Contains the information needed to make authenticated requests to a web API protected with Azure Active Directory (and role based authentication).
    /// </summary>
    public sealed class AADAppConfiguration
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AADAppConfiguration"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public AADAppConfiguration(IConfiguration configuration)
        {
            this.AADAppIdentifier = configuration["AADAppIdentifier"];
            this.TenantId = configuration["TenantId"];
            this.ClientId = configuration["ClientId"];
            this.ClientSecret = configuration["ClientSecret"];
            this.CertificateSubjectName = configuration["CertificateSubjectName"];
            this.Scopes = new string[] { $"{this.AADAppIdentifier}/.default" };
        }

        /// <summary>
        /// Gets the Azure Active Directory (AAD) application identifier of the web API that the calling application needs authenticated access to.
        /// These identifiers can be found in the AAD application settings, and is separate from the client id / application id. It has the form of a URI.
        /// </summary>
        public string AADAppIdentifier { get; }

        /// <summary>
        /// Gets the scopes the application requests.
        /// </summary>
        public string[] Scopes { get; }

        /// <summary>
        /// Gets the tenant id of the Azure Active Directory (AAD) that hosts the application that is requesting access to another application.
        /// The tenant id is the id of the AAD-instance. This is always a GUID.
        /// </summary>
        public string TenantId { get; }

        /// <summary>
        /// Gets the client id (aka application id) of the Azure Active Directory-application that is requesting access to another application. This is always a GUID.
        /// </summary>
        public string ClientId { get; }

        /// <summary>
        /// Gets the secret string that the application uses to prove its identity when requesting a token. Also can be referred to as application password.
        /// </summary>
        public string ClientSecret { get; }

        /// <summary>
        /// Gets the subject name of the certificate that will be loaded and passed along the request to Azure Active Directory (AAD) to get an authentication token.
        /// The certificate (without the private key, .cer format) must be uploaded to the AAD application itself so that it can verify the certificate.
        /// The certificate (with the private key, pfx-format) must be uploaded to the web application host (App service or Azure Function).
        /// </summary>
        public string CertificateSubjectName { get; }
    }
}
