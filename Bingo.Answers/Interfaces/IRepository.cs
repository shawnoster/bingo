// Copyright (c) Shawn Oster. All rights reserved.
// Licensed under the MIT license.

namespace Bingo.Answers.Interfaces
{
    using System.Threading.Tasks;
    using Bingo.Answers.Models;

    public interface IRepository<T> where T : Entity
    {
        Task<T> GetItemAsync(string id);
        Task AddItemAsync(T item);
        Task UpdateItemAsync(string id, T item);
        Task DeleteItemAsync(string id);
    }
}
