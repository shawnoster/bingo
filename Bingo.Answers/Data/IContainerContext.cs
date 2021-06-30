// Copyright (c) Shawn Oster. All rights reserved.
// Licensed under the MIT license.

namespace Bingo.Answers.Data
{
    using Microsoft.Azure.Cosmos;
    using Bingo.Answers.Models;

    /// <summary>
    ///  Defines the container level context
    /// </summary>
    public interface IContainerContext<T> where T : Entity
    {
        /// <summary>
        /// Name of the CosmosDB container
        /// </summary>
        string ContainerName { get; }

        /// <summary>
        /// Generate ID
        /// </summary>
        string GenerateId(T entity);

        /// <summary>
        /// Resolve the partition key
        /// </summary>
        PartitionKey ResolvePartitionKey(string entityId);
    }
}
