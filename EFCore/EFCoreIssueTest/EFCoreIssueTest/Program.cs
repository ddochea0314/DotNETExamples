using EFCoreIssueTest.Models.Mail;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EFCoreIssueTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Worker>();
                    services.AddHostedService<Worker2>();
                    services.AddDbContextPool<MailDbContext>(option =>
                    {
                        option.UseInMemoryDatabase("Mail");
                        //option.UseSqlServer("Server=sql-dev;Database=Mail;User Id=sa;Password=yourStrong(!)Password;");
                    });
                });
    }
}
