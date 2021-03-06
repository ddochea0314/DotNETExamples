using EFCoreIssueTest.Models.Mail;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EFCoreIssueTest
{
    public class Worker2 : BackgroundService
    {
        private readonly ILogger<Worker2> _logger;
        private readonly IServiceScopeFactory _scopeFactory;

        public Worker2(ILogger<Worker2> logger, IServiceScopeFactory scopeFactory)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var scope = _scopeFactory.CreateScope();
            using var mailDB = scope.ServiceProvider.GetService<MailDbContext>();
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(5000, stoppingToken);
                foreach(var receive in mailDB.ReceiveUser.AsNoTracking().ToList())
                {
                    try
                    {
                        receive.Step = 3;
                        mailDB.Update(receive);
                        await mailDB.SaveChangesAsync();
                    }
                    catch(Exception ex)
                    {
                        
                    }
                }
            }
        }
    }
}
