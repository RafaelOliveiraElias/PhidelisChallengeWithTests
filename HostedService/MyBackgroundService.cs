using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Phidelis_Challenge.HostedService;

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
            while(!stoppingToken.IsCancellationRequested)
            {
                using(var scope = _serviceProvider.CreateAsyncScope())
                {
                    var scopedService = scope.ServiceProvider.GetRequiredService<IScopedService>();
                    await scopedService.Write();
                    
                    await Task.Delay(TimeSpan.FromSeconds(scopedService.TimeSeconds() | 10), stoppingToken);
                }
            }
        }
    }
}