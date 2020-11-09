using System.Linq;
using Microsoft.EntityFrameworkCore;
using ShopBridge.Domain.Entities;
using ShopBridge.Infrastructure.Persistence;
using ShopBridge.Infrastructure.Test.Mock;

namespace ShopBridge.Infrastructure.Test.TestContext
{
    public class ConfigureInMemoryDB
    {
        public static DbContextOptions ConfigureDB(string databaseName)
        {
            DbContextOptions options;

            options = new DbContextOptionsBuilder<ApplicationDbContext>()
                                           .UseInMemoryDatabase(databaseName: databaseName)
                                           .Options;

            // insert seed data to DB
            using (var dbContext = new ApplicationDbContext(options, new MockCurrentUserService()))
            {
                if (dbContext.InventoryItems.Count<InventoryItem>() == 0)
                {
                    dbContext.InventoryItems.Add(new InventoryItem { Name = "C#", Description = "Programming Language", Price = 350 });
                    dbContext.InventoryItems.Add(new InventoryItem { Name = ".Net", Description = "Framework ", Price = 250 });
                    dbContext.InventoryItems.Add(new InventoryItem { Name = "SQL Server", Description = "Database", Price = 450 });
                    dbContext.InventoryItems.Add(new InventoryItem { Name = "Angular", Description = "Frontend framework", Price = 500 });

                    dbContext.SaveChanges();
                }
            }

            return options;
        }
    }
}