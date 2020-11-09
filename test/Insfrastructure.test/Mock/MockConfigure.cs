using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;

namespace ShopBridge.Infrastructure.Test.Mock
{
    public class MockConfigure : IConfiguration
    {
        public string this[string key] { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        public IEnumerable<IConfigurationSection> GetChildren()
        {
            throw new System.NotImplementedException();
        }

        public IChangeToken GetReloadToken()
        {
            throw new System.NotImplementedException();
        }

        public IConfigurationSection GetSection(string key)
        {
            return null;
        }
        public string GetConnectionString(string name) => "Server=fst-vm;Initial Catalog=ShopBridgeDB;Integrated Security=true;";

    }
}