using ShopBridge.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace ShopBridge.Application.Common.Interfaces
{
    public interface IApplicationDBContext
    {
        DbSet<InventoryItem> InventoryItems { get; set; }
        Task<int> SaveChangesAsync(CancellationToken CancellationToken);
    }
}