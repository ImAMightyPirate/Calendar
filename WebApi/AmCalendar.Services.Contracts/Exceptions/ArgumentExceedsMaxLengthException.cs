// Copyright (c) Adam Mytton. All Rights Reserved.

namespace AmCalendar.Services.Contracts.Exceptions
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Exception thrown when the argument exceeds the maximum length.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class ArgumentExceedsMaxLengthException : Exception
    {
        private string paramName;

        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentExceedsMaxLengthException" /> class.
        /// </summary>
        /// <param name="paramName">The parameter name.</param>
        public ArgumentExceedsMaxLengthException(string paramName)
        {
            this.paramName = paramName;
        }
    }
}