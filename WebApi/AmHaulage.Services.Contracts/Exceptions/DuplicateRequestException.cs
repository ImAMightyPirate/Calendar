// Copyright (c) Adam Mytton. All Rights Reserved.

namespace AmHaulage.Services.Contracts.Exceptions
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Exception thrown when a duplicate request occurs.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class DuplicateRequestException : Exception
    {
    }
}