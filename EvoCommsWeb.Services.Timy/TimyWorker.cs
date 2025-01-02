using System.Net;
using EvoCommsWeb.Services.Timy.TimyServer;

namespace EvoCommsWeb.Services.Timy;

public class TimyWorker(ILogger<TimyWorker> logger, TimyServerFactory timyServerFactory)
    : BackgroundService
{
    private TimyServerInstance? _server;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
            if (logger.IsEnabled(LogLevel.Information))
                logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
        TimyConfig serverConfig = new("timy", IPAddress.Any, 7788);
        _server = await timyServerFactory.CreateServer(serverConfig);
    }
}