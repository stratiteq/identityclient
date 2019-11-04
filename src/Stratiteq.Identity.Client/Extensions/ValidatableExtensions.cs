// Copyright (c) Stratiteq Sweden AB. All rights reserved.
//
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Stratiteq.Identity.Client.Abstractions;
using System.Collections.Generic;
using System.Linq;

namespace Stratiteq.Identity.Client.Extensions
{
    /// <summary>
    /// Extensions on <see cref="IValidatable"/>.
    /// </summary>
    public static class ValidatableExtensions
    {
        /// <summary>
        /// Gets the validation result.
        /// </summary>
        /// <returns>True if validation is successful, false otherwise.</returns>
        public static bool IsValid(this IValidatable input)
        {
            return input.ValidationResult().Success;
        }

        /// <summary>
        /// Gets the validation message.
        /// </summary>
        public static string ValidationMessage(this IValidatable input)
        {
            return input.ValidationResult().Message;
        }

        /// <summary>
        /// Gets the <see cref="ValidationResult"/> object.
        /// </summary>
        public static ValidationResult ValidationResult(this IValidatable input)
        {
            // This avoids needing a null check in our code when we validate nullable objects
            return input == null ? new ValidationResult(false, "Cannot validate null.") : input.Validate();
        }

        /// <summary>
        /// Extension to get the ValidationResult.
        /// </summary>
        public static ValidationResult ToValidationResult<T>(this IEnumerable<T> input)
        {
            if (input == null)
            {
                return new ValidationResult(false, "Cannot validate null.");
            }

            var errors = input.ToList();
            bool success = errors.Count == 0;
            string message = success ? "Validation successful." : string.Join(" ", errors);

            return new ValidationResult(success, message);
        }
    }
}
