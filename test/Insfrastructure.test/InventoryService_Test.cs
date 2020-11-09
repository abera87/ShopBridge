using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShopBridge.Domain.Common;
using ShopBridge.Domain.Entities;
using ShopBridge.Infrastructure.Persistence;
using ShopBridge.Infrastructure.Test.Mock;
using ShopBridge.Infrastructure.Test.TestContext;
using Xunit;

namespace ShopBridge.Infrastructure.Test
{
    public class InventoryService_Test
    {
        private readonly DbContextOptions options;
        public InventoryService_Test()
        {
            options = ConfigureInMemoryDB.ConfigureDB("ShopBridgeDB");
        }


        [Fact]
        public void GetItems()
        {

            //Given

            //When
            using (var dbContext = new ApplicationDbContext(options, new MockCurrentUserService()))
            {
                InventoryService invs = new InventoryService(dbContext);
                var result = invs.GetItemsAsync().Result;

                Assert.Equal(4, result.Count<InventoryItem>());

            }
            //Then
        }

        [Fact]
        public void GetItemById()
        {
            //Given
            using var dbContext = new ApplicationDbContext(options, new MockCurrentUserService());
            InventoryService invs = new InventoryService(dbContext);
            //When
            var result = invs.GetItemByIdAsync(3).Result;
            //Then
            Console.WriteLine(result.Id);
            Assert.Equal(3, result.Id);
        }

        [Fact]
        public void UpdateItem()
        {
            //Given
            using var dbContext = new ApplicationDbContext(options, new MockCurrentUserService());
            InventoryService invs = new InventoryService(dbContext);
            var item = invs.GetItemByIdAsync(3).Result;
            //new InventoryItem { Name = "Angular", Description = "Frontend framework", Price = 500 };
            item.Price = 400;
            item.LastModifiedBy = "Test User";
            item.LastModifiedOn = DateTimeOffset.Now.DateTime;
            //When
            var result = invs.UpdateItemAsync(item.Id, item).Result;
            //Then
            Assert.Equal("NoContent", result);
        }
        [Fact]
        public void UpdateItem_BadRequest()
        {
            //Given
            using var dbContext = new ApplicationDbContext(options, new MockCurrentUserService());
            InventoryService invs = new InventoryService(dbContext);
            var item = invs.GetItemByIdAsync(4).Result;
            //new InventoryItem { Name = "Angular", Description = "Frontend framework", Price = 500 };
            item.Price = 400;
            //When
            var result = invs.UpdateItemAsync(item.Id + 1, item).Result;
            //Then
            Assert.Equal("BadRequest", result);
        }
        [Fact]
        public void UpdateItem_NotFound()
        {
            //Given
            using var dbContext = new ApplicationDbContext(options, new MockCurrentUserService());
            InventoryService invs = new InventoryService(dbContext);
            var item = invs.GetItemByIdAsync(4).Result;
            //new InventoryItem { Name = "Angular", Description = "Frontend framework", Price = 500 };
            item.Price = 400;
            item.Id = -1;
            //When
            var result = invs.UpdateItemAsync(-1, item).Result;
            //Then
            Assert.Equal("NotFound", result);
        }
        [Fact]
        public void DeleteItem()
        {
            //Given
            using var dbContext = new ApplicationDbContext(options, new MockCurrentUserService());
            InventoryService invs = new InventoryService(dbContext);
            //When
            var result = invs.DeleteItemAsync(4).Result;
            //Then
            Assert.Equal(4, result.Id);
        }
        [Fact]
        public void DeleteItem_ReturnNull()
        {
            //Given
            using var dbContext = new ApplicationDbContext(options, new MockCurrentUserService());
            InventoryService invs = new InventoryService(dbContext);
            //When
            var result = invs.DeleteItemAsync(400).Result;
            //Then
            Assert.Null(result);
        }
        [Fact]
        public void AddItem()
        {
            //Given
            using var dbContext = new ApplicationDbContext(options, new MockCurrentUserService());
            InventoryService invs = new InventoryService(dbContext);
            var newItem = new InventoryItem
            {
                Name = "Javascripe",
                Description = "scripting language",
                Price = 100,
                CreatedBy = "Test User",
                CreatedOn = DateTimeOffset.Now.DateTime
            };
            //When
            var result = invs.AddItemAsync(newItem).Result;
            //Then
            Assert.Equal(newItem.Name, result.Name);
        }

    }
}