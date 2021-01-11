using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace OAuth.Client
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
            services.AddAuthentication(config =>
            {
                // 쿠키를 확인하여 인증되었는지 확인.
                config.DefaultAuthenticateScheme = "ClientCookie";
                // 로그인하면 쿠키를 처리.
                config.DefaultSignInScheme = "ClientCookie";
                // use this to check if we are allowed to do something.
                config.DefaultChallengeScheme = "OurServer";
            })
                .AddCookie("ClientCookie")
                .AddOAuth("OurServer", conf =>
                   {
                       conf.ClientId = "client_id";
                       conf.ClientSecret = "client_secret";
                       conf.CallbackPath = "/oauth/callback";
                       conf.AuthorizationEndpoint = "https://localhost:44300/oauth/authorize"; // OAuth.Server 주소
                       conf.TokenEndpoint = "https://localhost:44300/oauth/token"; // OAuth.Server 주소

                       conf.SaveTokens = true;

                       conf.Events = new OAuthEvents()
                       {
                           OnCreatingTicket = context =>
                           {
                               var accessToken = context.AccessToken;
                               var base64payload = accessToken.Split('.')[1];
                               var bytes = Convert.FromBase64String(base64payload);
                               var jsonPayload = Encoding.UTF8.GetString(bytes);
                               var claims = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonPayload);

                               foreach (var claim in claims)
                               {
                                   context.Identity.AddClaim(new Claim(claim.Key, claim.Value));
                               }

                               return Task.CompletedTask;
                           }
                       };
                   });
            services.AddHttpClient();
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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
