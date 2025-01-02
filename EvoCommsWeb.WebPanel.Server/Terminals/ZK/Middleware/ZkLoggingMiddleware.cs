namespace EvoCommsWeb.WebPanel.Server.Terminals.ZK.Middleware;

public class ZkLoggingMiddleware(
    RequestDelegate next,
    ILogger<ZkLoggingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Path.StartsWithSegments("/iclock"))
        {
            context.Request.EnableBuffering();
            string? body = await new StreamReader(context.Request.Body).ReadToEndAsync();
            logger.LogDebug($"Device Request: {body}");
            context.Request.Body.Position = 0;
        }

        await next(context);
    }
}