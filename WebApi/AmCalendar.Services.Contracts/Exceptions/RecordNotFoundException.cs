// Copyright (c) Adam Mytton. All Rights Reserved.

namespace AmCalendar.Services.Contracts.Exceptions
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Exception thrown when a record is not found.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class RecordNotFoundException : Exception
    {
    }
}