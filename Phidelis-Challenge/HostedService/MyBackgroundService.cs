using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Phidelis_Challenge.HostedService;
//criando o hosted service na api:
namespace Phidelis_Challenge
{
    public class MyBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<MyBackgroundService> _logger;
        public MyBackgroundService(IServiceProvider serviceProvider, ILogger<MyBackgroundService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while(!stoppingToken.IsCancellationRequested )
            {
                using(var scope = _serviceProvider.CreateAsyncScope())
                {
                    var scopedService = scope.ServiceProvider.GetRequiredService<IScopedService>();
                    if(scopedService.TimeSeconds() < 3600)
                    {
                        if(scopedService.GetListSize() < 200)
                        {
                            await scopedService.Write();
                        }
                        var scopedServiceReturn = scope.ServiceProvider.GetRequiredService<IScopedService>();
                        int timeSecondsReturn =  scopedServiceReturn.TimeSeconds();
                        await Task.Delay(TimeSpan.FromSeconds(timeSecondsReturn));
                    }
                }
                await Task.Delay(TimeSpan.FromSeconds(1), stoppingToken);
            }
        }
    }
}