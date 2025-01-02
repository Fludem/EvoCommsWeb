using EvoCommsWeb.Services.Timy;
using EvoCommsWeb.Services.Timy.TimyServer;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
builder.Logging.AddConsole();
builder.Services.AddSingleton<TimyServerFactory>();

builder.Services.AddHostedService<TimyWorker>();
IHost host = builder.Build();
host.Run();