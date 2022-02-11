using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using katalogi.Dtos;
using katalogi.Entities;
using katalogi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace katalogi.Controllers
{
    //Gets items or item from the repositories
    [ApiController]
    [Route("items")]
    public class ItemsController : ControllerBase
    {
        private readonly IItemsRepository repository;

        public ItemsController(IItemsRepository repository)
        {
            this.repository = repository;
        }
        //GET /items
        //Returns list of items from repository
        [HttpGet]
        public async  Task<IEnumerable<ItemDto>> GetItemsAsync()
        {
            var items = (await repository.GetItemsAsync())
                    .Select( item => item.AsDto());
            return items;
        }

        // GET /items/{id}
        //Returns certain item from repository based on id
        //if item cannot be found returns "404 not found"
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDto>> GetItemAsync(Guid id)
        {
            var item = await repository.GetItemAsync(id);

            if (item is null)
            {
                return NotFound();
            }

            return item.AsDto();
        }

        // POST /items
        [HttpPost]
        public async Task<ActionResult<ItemDto>> CreateItemAsync(CreateItemDto itemDto)
        {
            Item item = new(){
                Id = Guid.NewGuid(),
                Name = itemDto.Name,
                Price = itemDto.Price,
                CreatedDate = DateTimeOffset.UtcNow
            };

            await repository.CreateItemAsync(item);

            return CreatedAtAction(nameof(GetItemAsync), new { id = item.Id }, item.AsDto());
        }

        // PUT /items/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateItemAsync(Guid id, UpdateItemDto itemDto ) {
            var exsistingItem = await repository.GetItemAsync(id);

            if (exsistingItem is null)
            {
                return NotFound();
            }

            Item updatedItem = exsistingItem with {
                Name = itemDto.Name,
                Price = itemDto.Price
            };

            await repository.UpdateItemAsync(updatedItem);

            return NoContent();
        }

        // DELETE /items/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteItemAsync(Guid id)
        {
            var exsistingItem = await repository.GetItemAsync(id);

            if (exsistingItem is null)
            {
                return NotFound();
            }

            await repository.DeleteItemAsync(id);
            
            return NoContent();
        }

    }
}