// Copyright (c) Stratiteq Sweden AB. All rights reserved.
//
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Stratiteq.Identity.Client.Abstractions
{
    /// <summary>
    /// Contains the result of a validation.
    /// </summary>
    public class ValidationResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationResult"/> class.
        /// </summary>
        /// <param name="success">Indicates wether the validation successed or not.</param>
        /// <param name="message">Validation messages.</param>
        public ValidationResult(bool success, string message)
        {
            this.Success = success;
            this.Message = message;
        }

        /// <summary>
        /// Gets a value indicating whether gets whether the specified object is valid or not.
        /// </summary>
        public bool Success { get; }

        /// <summary>
        /// Gets a string with messages from the validation.
        /// </summary>
        public string Message { get; }
    }
}
