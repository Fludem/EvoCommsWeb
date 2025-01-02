using System.Data;
using SuperSocket.Server.Abstractions;
using SuperSocket.Server.Host;
using SuperSocket.WebSocket.Server;

namespace EvoCommsWeb.Services.Timy.TimyServer;

public class TimyServerFactory(ILogger<TimyServerFactory> logger, ILogger<TimyServerInstance> timyLogger) : IAsyncDisposable
{
    private readonly Dictionary<int, TimyServerInstance?> _timyServers = new();

    public async ValueTask DisposeAsync()
    {
        if (_timyServers.Count > 0)
            foreach (TimyServerInstance? server in _timyServers.Values)
                await server.DisposeAsync();
    }

    public async Task<TimyServerInstance?> CreateServer(TimyConfig config)
    {
        logger.LogInformation("Initializing WebSocket Server.");
        if (!IsPortUnique(config.ListenPort))
            throw new ConstraintException(
                $"A Timy Server Listener with port {config.ListenPort} has already been initialized.");
        TimyServerInstance? newServerInstance = new(timyLogger, MakeSuperSocketServer(config));
        await newServerInstance.StartAsync();
        _timyServers.Add(config.ListenPort, newServerInstance);
        return newServerInstance;
    }

    private IServer MakeSuperSocketServer(TimyConfig config)
    {
        WebSocketHostBuilder? builder = WebSocketHostBuilder.Create();
        builder.UseWebSocketMessageHandler((session, message) =>
            {
                logger.LogDebug($"Received message: {message.Message}");
                return ValueTask.CompletedTask;
            }
        );
        builder.ConfigureAppConfiguration((hostCtx, configApp) =>
        {
            configApp.AddInMemoryCollection(new Dictionary<string, string>
            {
                { "serverOptions:name", config.Name },
                { "serverOptions:listeners:0:ip", config.ListenAddress.ToString() },
                { "serverOptions:listeners:0:port", config.ListenPort.ToString() }
            }!);
        });
        return builder.BuildAsServer();
    }

    private bool IsPortUnique(int port)
    {
        return !_timyServers.ContainsKey(port);
    }
}