using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PactNet;
using PactNet.Infrastructure.Outputters;

namespace Pact.Example.Producer.Tests
{
    [TestClass]
    public class EmployeesControllerTests
    {

        private readonly TestServerFixture _fixture;

        public EmployeesControllerTests()
        {
            _fixture = new TestServerFixture();
            _fixture.StartAsync().Wait();
        }

        [TestMethod]
        public void EnsureSomethingApiHonoursPactWithConsumer()
        {
            var uri = "http://localhost:5000/employees";

            var config = new PactVerifierConfig
            {
                Outputters = new List<IOutput> //NOTE: We default to using a ConsoleOutput, however xUnit 2 does not capture the console output, so a custom outputter is required.
                {
                    new ConsoleOutput()
                },
                Verbose = false //Output verbose verification logs to the test output
            };

            IPactVerifier pactVerifier = new PactVerifier(config);
            pactVerifier
                .ProviderState(uri)
                .ServiceProvider("EmployeeService", uri)
                .HonoursPactWith("Web")
                .PactUri("https://example-pact-broker.azurewebsites.net/pacts/provider/EmployeeService/consumer/Web/latest") //You can specify a http or https uri
                .Verify();
        }
    }
}
