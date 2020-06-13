// Copyright (c) Adam Mytton. All Rights Reserved.

namespace AmCalendar.Persistence
{
    using System.Diagnostics.CodeAnalysis;
    using AmCalendar.Persistence.Contracts;

    /// <summary>
    /// Factory for creating new instances of the repository.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class RepositoryFactory : IRepositoryFactory
    {
        /// <summary>
        /// Creates a new instance of the repository.
        /// </summary>
        /// <returns>A repository.</returns>
        public IRepository Create()
        {
            return new Repository();
        }
    }
}