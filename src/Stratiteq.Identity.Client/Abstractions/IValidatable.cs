// Copyright (c) Stratiteq Sweden AB. All rights reserved.
//
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Stratiteq.Identity.Client.Abstractions
{
    /// <summary>
    /// Provides a way for an object to be validate itself.
    /// </summary>
    public interface IValidatable
    {
        /// <summary>
        /// Determines whether the object is valid.
        /// </summary>
        /// <returns>The result of the validation.</returns>
        ValidationResult Validate();
    }
}
