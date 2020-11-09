using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShopBridge.Application.Common.Interfaces;
using ShopBridge.Domain.Entities;

namespace ShopBridge.WebApi.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class InventoryItemController : ShopBridgeAPIController
    {
        private readonly IInventoryService inventoryService;

        public InventoryItemController(IInventoryService inventoryService)
        {
            this.inventoryService = inventoryService;
        }
        [HttpGet]
        public async Task<IEnumerable<InventoryItem>> GetAsync()
        {
            //return await applicationDBContext.InventoryItems.ToListAsync();
            var result = await inventoryService.GetItemsAsync();
            return result;

        }
        [HttpGet("{id}")]
        public async Task<InventoryItem> GetByIdAsync(int id)
        {
            //return await applicationDBContext.InventoryItems.ToListAsync();
            var result = await inventoryService.GetItemByIdAsync(id);
            return result;

        }
        [HttpPost]
        public async Task<ActionResult<InventoryItem>> PostAsync([FromBody] InventoryItem item)
        {
            // var result = applicationDBContext.InventoryItems.Add(item);
            // var records = await applicationDBContext.SaveChangesAsync(new CancellationToken());

            var result = await inventoryService.AddItemAsync(item);

            return CreatedAtAction("GetById", new { id = result.Id }, result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<InventoryItem>> DeleteAsync(int id)
        {
            // var item = await applicationDBContext.InventoryItems.FindAsync(id);
            // if (item == null)
            // {
            //     return NotFound();
            // }

            // applicationDBContext.InventoryItems.Remove(item);
            // await applicationDBContext.SaveChangesAsync(new CancellationToken());

            var item = await inventoryService.DeleteItemAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutAsync(int id, InventoryItem inventoryItem)
        {
            var result = await inventoryService.UpdateItemAsync(id, inventoryItem);
            ActionResult returResult = NoContent();
            switch (result)
            {
                case "BadRequest":
                    returResult = BadRequest();
                    break;
                case "NotFound":
                    returResult = NotFound();
                    break;
                case "NoContent":
                    returResult = NoContent();
                    break;
            }

            return returResult;

            // if (id != inventoryItem.Id)
            // {
            //     return BadRequest();
            // }

            // var item = await applicationDBContext.InventoryItems.FindAsync(id);
            // item.Name = inventoryItem.Name;
            // item.Description = inventoryItem.Description;
            // item.Price = inventoryItem.Price;
            // item.ImagePath = inventoryItem.ImagePath;

            // try
            // {
            //     await applicationDBContext.SaveChangesAsync(new CancellationToken());
            // }
            // catch (DbUpdateConcurrencyException)
            // {
            //     var isExists = await IsInventoryItemExistsAsync(id);
            //     if (!isExists)
            //     {
            //         return NotFound();
            //     }
            //     else
            //     {
            //         throw;
            //     }
            // }
            // return NoContent();
        }


    }
}