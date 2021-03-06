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
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IServiceScopeFactory _scopeFactory;

        public Worker(ILogger<Worker> logger, IServiceScopeFactory scopeFactory)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var scope = _scopeFactory.CreateScope();
            using var mailDB = scope.ServiceProvider.GetService<MailDbContext>(); // 여기에서 사용하면 데이터 불일치 현상이 나타남.

            #region DB 초기데이터 입력
            mailDB.Database.EnsureCreated();
            try
            {
                for (int i = 0; i < 10; i++)
                {
                    mailDB.Add(new ReceiveUser()
                    {
                        Addr = "test@test.com",
                        Name = "Test",
                        Step = 1,
                        TemplateID = 1,
                        UserID = $"uesr00{i}"
                    });
                }
                await mailDB.SaveChangesAsync();
            }
            catch
            {

            }
            #endregion
            while (!stoppingToken.IsCancellationRequested)
            {
                foreach (var receive in mailDB.ReceiveUser.Where(r => r.Step == 3))
                {
                    // Step 값이 3인 데이터가 조회되어 진입했는데 막상보면 초기값인 1이다.
                    // 이 현상을 해결하려면 ReceiveUser.AsNoTracking().Where(r => r.Step == 3) 로 수정하거나, mailDB를 while문 안으로 옮겨야한다.
                    _logger.LogInformation($"{DateTime.Now:G} receive {receive.UserID} is done. Step:{receive.Step}"); 
                }
                await Task.Delay(10000, stoppingToken);
            }
        }
    }
}
