using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.PlatformAbstractions;

namespace Pact.Example.Producer.Tests
{
    public class TestServerFixture: IDisposable
    {
        public IWebHost WebHost { get; }

        public TestServerFixture()
        {
            WebHost = new WebHostBuilder()
                .UseContentRoot(GetContentRootPath())
                .UseEnvironment("Development")
                .UseKestrel(options =>
                {
                    options.Listen(IPAddress.Loopback, 5000);
                })
                .UseStartup<Startup>()
                .Build();
        }

        private string GetContentRootPath()
        {
            var testProjectPath = PlatformServices.Default.Application.ApplicationBasePath;
            var relativePathToHostProject = @"../../../../../Pact.Example.Producer";
            return Path.Combine(testProjectPath, relativePathToHostProject);
        }

        public async Task StartAsync()
        {
            await WebHost.StartAsync();
        }
        
        void IDisposable.Dispose()
        {
            WebHost.StopAsync().Wait();
        }
    }
}
