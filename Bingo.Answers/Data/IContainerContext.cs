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
        string ContainerName { get; }
        string GenerateId(T entity);
        PartitionKey ResolvePartitionKey(string entityId);
    }
}
