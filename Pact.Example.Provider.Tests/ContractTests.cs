using Microsoft.AspNetCore.Hosting;
using PactNet;
using Xunit;

namespace Pact.Example.Provider.Tests
{
    public class ContractTests
    {
        [Fact]
        public void EnsureSomethingApiHonoursPactWithConsumer()
        {
            const string serviceUri = "http://localhost:9222";
            var config = new PactVerifierConfig
            {
                Verbose = true
            };

            var webHost = new WebHostBuilder()
                .UseStartup<Startup>()
                .UseUrls(serviceUri);

            webHost.Start();

            IPactVerifier pactVerifier = new PactVerifier(config);
            pactVerifier
                .ProviderState($"{serviceUri}/provider-states")
                .ServiceProvider("Something API", serviceUri)
                .HonoursPactWith("Consumer")
                .PactUri("http://pact-broker/pacts/provider/Something%20Api/consumer/Consumer/latest")
                .Verify();
        }
    }
}