// Copyright (c) Adam Mytton. All Rights Reserved.

namespace AmHaulage.Persistence.Contracts
{
    /// <summary>
    /// Contract for the repository factory.
    /// </summary>
    public interface IRepositoryFactory
    {
        /// <summary>
        /// Creates a new instance of the repository.
        /// </summary>
        /// <returns>A repository.</returns>
        IRepository Create();
    }
}