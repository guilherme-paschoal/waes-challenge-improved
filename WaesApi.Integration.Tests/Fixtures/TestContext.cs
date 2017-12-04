using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;

namespace WaesApi.Integration.Tests.Fixtures
{
    
    public class TestContext : IDisposable
    {
        TestServer server;
        public HttpClient Client { get; private set; }

        public TestContext()
        {
            SetUpClient();
        }

        private void SetUpClient()
        {
            server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>());

            Client = server.CreateClient();

            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        }

        public void Dispose() {
            server?.Dispose();
            Client?.Dispose();
        }
    }
}
