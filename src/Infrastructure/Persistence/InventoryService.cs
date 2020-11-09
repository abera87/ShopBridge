using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ShopBridge.Application.Common.Interfaces;
using ShopBridge.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ShopBridge.Infrastructure.Persistence
{
    public class InventoryService : IInventoryService
    {
        private readonly IApplicationDBContext applicationDBContext;

        public InventoryService(IApplicationDBContext applicationDBContext)
        {
            this.applicationDBContext = applicationDBContext;
        }

        public async Task<InventoryItem> AddItemAsync(InventoryItem item)
        {
            var result = applicationDBContext.InventoryItems.Add(item);
            var records = await applicationDBContext.SaveChangesAsync(new CancellationToken());
            return result.Entity;

        }

        public async Task<InventoryItem> DeleteItemAsync(int id)
        {
            var item = await applicationDBContext.InventoryItems.FindAsync(id);
            if (item == null)
            {
                return null;
            }
            applicationDBContext.InventoryItems.Remove(item);
            await applicationDBContext.SaveChangesAsync(new CancellationToken());

            return item;
        }

        public async Task<InventoryItem> GetItemByIdAsync(int id)
        {
            return await applicationDBContext.InventoryItems.FindAsync(id);
        }

        public async Task<IEnumerable<InventoryItem>> GetItemsAsync()
        {
            return await applicationDBContext.InventoryItems.ToListAsync();
        }

        public async Task<string> UpdateItemAsync(int id, InventoryItem inventoryItem)
        {
            if (id != inventoryItem.Id)
            {
                return "BadRequest";
            }

            if (await IsInventoryItemExistsAsync(id))
            {
                var item = await applicationDBContext.InventoryItems.FindAsync(id);

                item.Name = inventoryItem.Name;
                item.Description = inventoryItem.Description;
                item.Price = inventoryItem.Price;
                item.ImagePath = inventoryItem.ImagePath;
            }
            else
            {
                return "NotFound";
            }
            try
            {
                await applicationDBContext.SaveChangesAsync(new CancellationToken());
            }
            catch (DbUpdateConcurrencyException)
            {
                var isExists = await IsInventoryItemExistsAsync(id);
                if (!isExists)
                {
                    return "NotFound";
                }
                else
                {
                    throw;
                }
            }
            return "NoContent";
        }

        private async Task<bool> IsInventoryItemExistsAsync(int id)
        {
            return await applicationDBContext.InventoryItems.AnyAsync<InventoryItem>(i => i.Id == id, new CancellationToken());
        }
    }
}