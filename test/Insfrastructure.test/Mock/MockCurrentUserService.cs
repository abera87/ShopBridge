using ShopBridge.Application.Common.Interfaces;

namespace ShopBridge.Infrastructure.Test.Mock{
    public class MockCurrentUserService : ICurrentUserService
    {
        public string UserId => "xUnit Test User";
    }
}