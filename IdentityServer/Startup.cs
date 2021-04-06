using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace IdentityServer
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            var identityOptions = new IdentityOptions();
            _configuration.GetSection(IdentityOptions.SectionKey).Bind(identityOptions);
            
            services.AddIdentityServer()
                .AddInMemoryClients(IdentityConfiguration.GetClients(identityOptions))
                .AddInMemoryApiScopes(IdentityConfiguration.GetApiScopes(identityOptions))
                .AddDeveloperSigningCredential();
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseIdentityServer();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Identity server");
                });
            });
        }
    }
}