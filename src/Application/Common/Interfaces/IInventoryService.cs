using System.Collections.Generic;
using System.Threading.Tasks;
using ShopBridge.Domain.Entities;

namespace ShopBridge.Application.Common.Interfaces
{
    public interface IInventoryService
    {
        Task<IEnumerable<InventoryItem>> GetItemsAsync();
        Task<InventoryItem> GetItemByIdAsync(int id);
        Task<InventoryItem> AddItemAsync(InventoryItem item);
        Task<string> UpdateItemAsync(int id, InventoryItem item);
        Task<InventoryItem> DeleteItemAsync(int id);
    }
}