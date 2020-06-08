// Copyright (c) Adam Mytton. All Rights Reserved.

namespace AmHaulage.Services.Contracts.Exceptions
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Exception thrown when the end date occurs before the start date.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class DateOutOfRangeException : Exception
    {
    }
}