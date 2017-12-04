using Xunit;
namespace WaesApi.Integration.Tests.Fixtures
{
    [CollectionDefinition("SystemCollection")]
    public class Collection : ICollectionFixture<TestContext>
    {
    }
}
