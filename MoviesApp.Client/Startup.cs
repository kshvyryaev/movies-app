using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MoviesApp.Client.Clients;
using MoviesApp.Client.Options;

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
            services.AddScoped<IMoviesClient, MoviesClient>();

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

                options.SaveTokens = authenticationOptions.OpenIdConnect.SaveTokens;
                options.GetClaimsFromUserInfoEndpoint = authenticationOptions.OpenIdConnect.GetClaimsFromUserInfoEndpoint;
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
