using System.Net;

namespace EvoCommsWeb.Services.Timy.TimyServer;

public class TimyConfig(string name, IPAddress listenAddress, int listenPort)
{
    protected string SuperSocketPrefix = "serverOptions:";
    public string Name { get; } = name;
    public IPAddress ListenAddress { get; } = listenAddress;
    public int ListenPort { get; } = listenPort;
}