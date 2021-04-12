using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MoviesApp.Api.Data;
using MoviesApp.Api.Options;

namespace MoviesApp.Api
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
            var authenticationOptions = new AuthenticationOptions();
            _configuration.GetSection(AuthenticationOptions.SectionKey).Bind(authenticationOptions);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.Authority = authenticationOptions.JwtBearer.Authority;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = authenticationOptions.JwtBearer.TokenValidationParameters.ValidateAudience
                    };
                });
            
            var authorizationOptions = new AuthorizationOptions();
            _configuration.GetSection(AuthorizationOptions.SectionKey).Bind(authorizationOptions);

            services.AddAuthorization(options =>
            {
                options.AddPolicy(AuthorizationOptions.ApiClientIdPolicyName, policy
                    => policy.RequireClaim(
                        authorizationOptions.ApiClientIdPolicy.ClientIdType,
                        authorizationOptions.ApiClientIdPolicy.ClientIdValue));
            });
            
            services.AddDbContext<MoviesApiContext>(options
                => options.UseInMemoryDatabase("Movies"));
            
            services.AddControllers();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo {Title = "MoviesApp.Api", Version = "v1"});
            });
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                
                app.UseSwagger();
                
                app.UseSwaggerUI(options
                    => options.SwaggerEndpoint("/swagger/v1/swagger.json", "MoviesApp.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}