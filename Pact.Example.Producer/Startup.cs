using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Pact.Example.Producer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                
            }

            app.UseMiddleware<ProviderStateMiddleware>();
            app.UseMvc();
        }
    }

    public class ProviderStateMiddleware
    {
        private const string ConsumerName = "Web";
        private readonly RequestDelegate _next;
        private readonly IDictionary<string, Action> _providerStates;

        public ProviderStateMiddleware(RequestDelegate next)
        {
            _next = next;
            _providerStates = new Dictionary<string, Action>
            {
                {
                    "There is a something with id 'tester'",
                    AddTesterIfItDoesntExist
                }
            };
        }

        private void AddTesterIfItDoesntExist()
        {
            //Add code to go an inject or insert the tester data
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if (httpContext.Request.Path.Value == "/provider-states")
            {
                httpContext.Response.StatusCode = (int)HttpStatusCode.OK;

                if (httpContext.Request.Method == HttpMethod.Post.ToString() &&
                    httpContext.Request.Body != null)
                {
                    string jsonRequestBody;
                    using (var reader = new StreamReader(httpContext.Request.Body, Encoding.UTF8))
                    {
                        jsonRequestBody = reader.ReadToEnd();
                    }

                    var providerState = JsonConvert.DeserializeObject<ProviderState>(jsonRequestBody);

                    //A null or empty provider state key must be handled
                    if (providerState != null &&
                        !string.IsNullOrEmpty(providerState.State) &&
                        providerState.Consumer == ConsumerName &&
                        _providerStates.ContainsKey(providerState.State))
                    {
                        _providerStates[providerState.State].Invoke();
                    }

                    await httpContext.Response.WriteAsync(string.Empty);
                }
            }
            else
            {
                await _next(httpContext);
            }
        }
    }

    public class ProviderState
    {
        public string Consumer { get; set; }
        public string State { get; set; }
    }
}
