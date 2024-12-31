using System.Text;

namespace EvoCommsWeb.Server.Terminals.ZK;

public class ServerSettings(ILogger<ServerSettings> logger)
{
    private readonly string Delay = "15";
    private readonly string Encrypt = "0";
    private readonly string ErrorDelay = "3";
    private readonly bool GetAllLogs = true;
    private readonly string RealTime = "1";
    private string TimeZone = "0";
    private readonly string TransFlag = "1111000000";
    private readonly string TransInterval = "1";
    private readonly string TransTimes = "00:00;14:05";

    public void SetTimeZone(string timeZone)
    {
        TimeZone = timeZone;
    }

    public string MakeSettingsResponse(string serialNumber)
    {
        var response = new StringBuilder();
        response.AppendLine($"GET OPTION FROM:{serialNumber}");
        response.AppendLine("AttStamp=1");
        response.AppendLine("OpStamp=1");
        response.AppendLine("PhotoStamp=1");
        response.AppendLine($"ErrorDelay={ErrorDelay}");
        response.AppendLine($"Delay={Delay}");
        response.AppendLine($"TransTimes={TransTimes}");
        response.AppendLine($"TransInterval={TransInterval}");
        response.AppendLine($"TransFlag={TransFlag}");
        response.AppendLine($"Realtime={RealTime}");
        response.AppendLine($"Encrypt={Encrypt}");
        response.AppendLine($"TimeZone={TimeZone}");
        logger.LogDebug($"Response Body: {response}");
        return response.ToString();
    }
}