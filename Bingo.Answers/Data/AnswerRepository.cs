// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.using System

namespace Bingo.Answers.Data
{
    using System;
    using Microsoft.Azure.Cosmos;
    using Bingo.Answers.Interfaces;
    using Bingo.Answers.Models;

    public class AnswerRepository : CosmosDbRepository<Answer>, IAnswerRepository
    {
        /// <summary>
        /// CosmosDB container name
        /// </summary>
        public override string ContainerName { get; } = "Answers";

        /// <summary>
        ///     Generate Id.
        ///     e.g. "shoppinglist:783dfe25-7ece-4f0b-885e-c0ea72135942"
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override string GenerateId(Answer entity) => $"{entity.Category}:{Guid.NewGuid()}";

        /// <summary>
        /// Returns the value of the partition key
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        public override PartitionKey ResolvePartitionKey(string entityId) => new(entityId.Split(':')[0]);

        public AnswerRepository(ICosmosDbContainerFactory factory) : base(factory) { }
    }
}
