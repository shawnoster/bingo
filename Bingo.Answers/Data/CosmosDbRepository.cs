// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.using System

namespace Bingo.Answers.Data
{
    using System;
    using System.Net;
    using System.Threading.Tasks;
    using Microsoft.Azure.Cosmos;
    using Bingo.Answers.Interfaces;
    using Bingo.Answers.Models;
    using Bingo.Answers.Exceptions;

    public abstract class CosmosDbRepository<T> : IRepository<T>, IContainerContext<T> where T : Entity
    {
        /// <summary>
        /// Name of the CosmosDB container
        /// </summary>
        public abstract string ContainerName { get; }

        /// <summary>
        /// Generate id
        /// </summary>
        public abstract string GenerateId(T entity);

        /// <summary>
        /// Resolve the partition key
        /// </summary>
        public abstract PartitionKey ResolvePartitionKey(string entityId);

        private readonly ICosmosDbContainerFactory _cosmosDbContainerFactory;
        private readonly Container _container;

        protected CosmosDbRepository(ICosmosDbContainerFactory cosmosDbContainerFactory)
        {
            _cosmosDbContainerFactory = cosmosDbContainerFactory ?? throw new ArgumentNullException(nameof(cosmosDbContainerFactory));
            _container = _cosmosDbContainerFactory.GetContainer(ContainerName).Container;
        }

        public async Task AddItemAsync(T item)
        {
            item.Id = GenerateId(item);
            await _container.CreateItemAsync<T>(item, ResolvePartitionKey(item.Id));
        }

        public async Task DeleteItemAsync(string id)
        {
            await _container.DeleteItemAsync<T>(id, ResolvePartitionKey(id));
        }

        public async Task<T> GetItemAsync(string id)
        {
            try
            {
                ItemResponse<T> response = await _container.ReadItemAsync<T>(id, ResolvePartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                throw new EntityNotFoundException();
            }
        }

        public async Task UpdateItemAsync(string id, T item)
        {
            await _container.UpsertItemAsync<T>(item, ResolvePartitionKey(id));
        }
    }
}
