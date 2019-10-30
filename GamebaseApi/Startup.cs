using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using GamebaseApi.Auth;
using IdentityServer4.Services;
using IdentityServer4.Validation;
using Ipstset.Gamebase.Core.Games;
using Ipstset.Gamebase.Core.Platforms;
using Ipstset.Gamebase.Core.Users;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace GamebaseApi
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

            var inTestMode = Convert.ToBoolean(Configuration["InTestMode"]);
            //--IDENTITY SERVER-->
            //todo: real cert
            //var cert = new X509Certificate2("cert_file", "your_cert_password");
            services.AddIdentityServer(options =>
            {
                options.Authentication.CookieLifetime = new TimeSpan(0, 60, 0);
                options.Authentication.CookieSlidingExpiration = true;
            })
                .AddDeveloperSigningCredential()
                .AddInMemoryIdentityResources(AuthConfig.IdentityResources)
                .AddInMemoryClients(AuthConfig.Clients)
                .AddInMemoryApiResources(AuthConfig.Apis)
                //.AddTestUsers(AuthConfig.Users);
                .AddProfileService<ProfileService>();

            //IdentityServer dependency injection
            services.AddTransient<IResourceOwnerPasswordValidator, ResourceOwnerPasswordValidator>()
                .AddTransient<IProfileService, ProfileService>()
                .AddTransient<IUserStore, UserStore>();

            //configure authentication for API
            //prevents mapping of standard claim types to Microsoft proprietary ones
            var authConfigKey = inTestMode ? "AuthTest" : "Auth";
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            services.AddAuthentication()
                .AddJwtBearer(jwt =>
                {
                    jwt.Authority = Configuration[$"{authConfigKey}:Authority"];
                    jwt.RequireHttpsMetadata = Convert.ToBoolean(Configuration[$"{authConfigKey}:RequireHttpsMetadata"]);
                    jwt.Audience = Configuration[$"{authConfigKey}:ApiName"];
                });
            //--IDENTITY SERVER END-->
            var connString = Configuration["ConnectionStrings:GamebaseConnection"];
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient(u => ClaimsService.CreateAppUser(new HttpContextAccessor().HttpContext?.User?.Claims));

            //services.AddTransient<IGameRepository>(r => new Ipstset.Gamebase.Data.Test.Repositories.GameRepository());
            services.AddTransient<IGameRepository>(r => new Ipstset.Gamebase.Data.Repositories.GameRepository(connString));
            services.AddTransient<IGameService, GameService>();
            //services.AddTransient<IPlatformRepository>(r => new Ipstset.Gamebase.Data.Test.Repositories.PlatformRepository());
            services.AddTransient<IPlatformRepository>(r => new Ipstset.Gamebase.Data.Repositories.PlatformRepository(connString));
            services.AddTransient<IPlatformService, PlatformService>();
            //services.AddTransient<IUserRepository>(r => new Ipstset.Gamebase.Data.Test.Repositories.UserRepository());
            services.AddTransient<IUserRepository>(r => new Ipstset.Gamebase.Data.Repositories.UserRepository(connString));
            services.AddTransient<IUserService, UserService>();

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials()
                        .SetPreflightMaxAge(TimeSpan.FromSeconds(2520))
                        .Build());
            });

            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //needed for Heroku
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedProto
            });

            app.UseAuthentication();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.Map("/auth", builder =>
            {
                builder.UseIdentityServer();
                builder.UseMvcWithDefaultRoute();
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });


        }
    }
}
