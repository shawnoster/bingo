// Copyright (c) Shawn Oster. All rights reserved.
// Licensed under the MIT license.

namespace Bingo.Answers.Data
{
    using System.Threading.Tasks;

    public interface ICosmosDbContainerFactory
    {
        /// <summary>
        /// Returns a CosmosDbContainer wrapper
        /// </summary>
        ICosmosDbContainer GetContainer(string containerName);

        /// <summary>
        /// Ensure the database is created
        /// </summary>
        Task EnsureDbSetupAsync();
    }
}
