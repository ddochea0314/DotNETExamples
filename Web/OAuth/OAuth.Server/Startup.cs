using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace OAuth.Server
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
            services.AddAuthentication("OAuth")
                   .AddJwtBearer("OAuth", config =>
                   {
                       var secretBytes = Encoding.UTF8.GetBytes(Constants.Secret);
                       var key = new SymmetricSecurityKey(secretBytes);

                       
                       config.Events = new JwtBearerEvents()
                       {
                           OnMessageReceived = context =>
                           {
                               if (context.Request.Query.ContainsKey("access_token"))
                               {
                                   //queryString 방식을 지원하기위한 코드. WebAPI 통신에서 Authorization 헤더를 쓰지못할 경우 사용
                                   context.Token = context.Request.Query["access_token"];
                               }
                               else if (context.Request.Cookies.ContainsKey("access_token"))
                               {
                                   // 쿠키 방식을 지원하기위한 코드. OAuth 인증서비스와 실제 기능제공용 웹서비스가 같은 소스인 경우는 드물기 때문에 일반적으론 사용하지 않음.
                                   context.Token = context.Request.Cookies["access_token"];
                               }
                               return Task.CompletedTask;
                           }
                       };

                       // 토큰 검증. 헤더 "Authorization"에 Value "Bearer {JWT}"를 확인하여 토큰이 올바르면 [Authorize]된 웹서비스 접근 가능해짐.
                       config.TokenValidationParameters = new TokenValidationParameters()
                       {
                           ValidIssuer = Constants.Issuer, // 발행인 검증
                           ValidAudience = Constants.Audiance, // 사용서버 검증
                           IssuerSigningKey = key, // 키 검증
                           ClockSkew = TimeSpan.Zero, // 기본값은 5분. exipre 시간 초과여부 확인시 통신간 지연시간을 감안한 시간값
                       };
                   });
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
