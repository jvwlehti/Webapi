using System;
using System.Linq;
using System.Collections.Generic;
using katalogi.Entities;
using System.Threading.Tasks;

namespace katalogi.Repositories
{


    public class InMemItemsRepository : IItemsRepository
    {
        //Creates list for a testing
        private readonly List<Item> items = new()
        {
            new Item { Id = Guid.NewGuid(), Name = "Potion", Price = 9, CreatedDate = DateTimeOffset.UtcNow },
            new Item { Id = Guid.NewGuid(), Name = "Iron Sword", Price = 20, CreatedDate = DateTimeOffset.UtcNow },
            new Item { Id = Guid.NewGuid(), Name = "Bronze Shield", Price = 12, CreatedDate = DateTimeOffset.UtcNow }
        };

        //returns list of items
        public async Task<IEnumerable<Item>> GetItemsAsync()
        {
            return await Task.FromResult(items);
        }

        //returns item based on it's id
        public async Task<Item> GetItemAsync(Guid id)
        {
            var item = items.Where(item => item.Id == id).SingleOrDefault();
            return await Task.FromResult(item);
        }

        public async Task CreateItemAsync(Item item)
        {
            items.Add(item);
            await Task.CompletedTask;
        }

        public async Task UpdateItemAsync(Item item)
        {
            var index = items.FindIndex(existingItem => existingItem.Id == item.Id);
            items[index] = item;
            await Task.CompletedTask;
        }

        public async Task DeleteItemAsync(Guid id)
        {
            var index = items.FindIndex(existingItem => existingItem.Id == id);
            items.RemoveAt(index);
            await Task.CompletedTask;
        }

    }
}