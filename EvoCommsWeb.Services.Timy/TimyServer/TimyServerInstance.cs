using SuperSocket.Server.Abstractions;

namespace EvoCommsWeb.Services.Timy.TimyServer;

public class TimyServerInstance(ILogger<TimyServerInstance> logger, IServer webSocketServer) : IAsyncDisposable
{
    public ValueTask DisposeAsync()
    {
        throw new NotImplementedException();
    }

    public async Task StopAsync()
    {
        if (webSocketServer.State is not (ServerState.Stopping or ServerState.Stopped))
        {
            logger.LogInformation("Stopping websocket server.");
            await webSocketServer.StopAsync();
            return;
        }

        logger.LogWarning("WebSocket server is already stopped.");
    }

    public async Task<IServer> StartAsync()
    {
        if (webSocketServer.State is not (ServerState.Started or ServerState.Starting))
        {
            await webSocketServer.StartAsync();
            logger.LogInformation("WebSocket server started successfully.");
            return webSocketServer;
        }

        logger.LogWarning("WebSocket server is already started.");
        return webSocketServer;
    }
}