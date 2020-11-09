using Microsoft.Extensions.DependencyInjection;
using ShopBridge.Application.Common.Interfaces;
using ShopBridge.Infrastructure.Test.Mock;
using Xunit;

namespace ShopBridge.Infrastructure.Test
{
    public class DependencyInjection_Test
    {
        [Fact]
        public void AddInfrastructure_Test()
        {
            //Given
            var service = new ServiceCollection();
            var configure = new MockConfigure();
            service.AddInfrastructure(configure);
            service.AddScoped<ICurrentUserService,MockCurrentUserService>();
            //When
            var provider = service.BuildServiceProvider();
            var inventoryService = provider.GetRequiredService<IInventoryService>();
            //Then
            Assert.IsType<IInventoryService>(inventoryService); 
        }
    }
}