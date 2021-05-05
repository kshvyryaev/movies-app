using System;
using IdentityModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using MoviesApp.Client.Clients;
using MoviesApp.Client.Options;
using AuthenticationOptions = MoviesApp.Client.Options.AuthenticationOptions;

namespace MoviesApp.Client
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
            services.AddTransient<AuthenticationHandler>();

            var apiUrisOptions = new ApiUrisOptions();
            _configuration.GetSection(ApiUrisOptions.SectionKey).Bind(apiUrisOptions);
            
            services.AddHttpClient<IMoviesClient, MoviesClient>(httpClient =>
                {
                    httpClient.BaseAddress = new Uri(apiUrisOptions.MoviesApiUri);
                })
                .AddHttpMessageHandler<AuthenticationHandler>()
                .AddPolicyHandler(HttpPolicies.RetryPolicy)
                .AddPolicyHandler(HttpPolicies.CircuitBreakerPatternPolicy);

            services.AddHttpContextAccessor();

            var authenticationOptions = new AuthenticationOptions();
            _configuration.GetSection(AuthenticationOptions.SectionKey).Bind(authenticationOptions);

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            })
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
            {
                options.Authority = authenticationOptions.OpenIdConnect.Authority;
                options.ClientId = authenticationOptions.OpenIdConnect.ClientId;
                options.ClientSecret = authenticationOptions.OpenIdConnect.ClientSecret;
                options.ResponseType = authenticationOptions.OpenIdConnect.ResponseType;

                foreach (var scope in authenticationOptions.OpenIdConnect.Scope)
                {
                    options.Scope.Add(scope);
                }
                
                options.ClaimActions.MapUniqueJsonKey(JwtClaimTypes.Role,JwtClaimTypes.Role);

                options.SaveTokens = authenticationOptions.OpenIdConnect.SaveTokens;
                options.GetClaimsFromUserInfoEndpoint = authenticationOptions.OpenIdConnect.GetClaimsFromUserInfoEndpoint;

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    NameClaimType = JwtClaimTypes.GivenName,
                    RoleClaimType = JwtClaimTypes.Role
                };
            });

            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
