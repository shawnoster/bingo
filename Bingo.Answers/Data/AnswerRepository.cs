// Copyright (c) Shawn Oster. All rights reserved.
// Licensed under the MIT license.

namespace Bingo.Answers.Data
{
    using System;

    using Bingo.Answers.Interfaces;
    using Bingo.Answers.Models;
    using Microsoft.Azure.Cosmos;

    public class AnswerRepository : CosmosDbRepository<Answer>, IAnswerRepository
    {
        /// <summary>
        /// CosmosDB container name
        /// </summary>
        public override string ContainerName { get; } = "Answers";

        /// <summary>
        /// Generate ID
        /// </summary>
        public override string GenerateId(Answer entity) => $"{entity.Category}:{Guid.NewGuid()}";

        /// <summary>
        /// Returns the value of the partition key
        /// </summary>
        public override PartitionKey ResolvePartitionKey(string entityId) => new(entityId.Split(':')[0]);

        public AnswerRepository(ICosmosDbContainerFactory factory) : base(factory) { }
    }
}
