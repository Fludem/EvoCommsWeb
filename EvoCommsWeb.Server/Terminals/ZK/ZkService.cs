using System.Text;

namespace EvoCommsWeb.Server.Terminals.ZK;

public class ZkService(ILogger<ZkService> logger, ServerSettings settings)
{
    public string HandleInitialConnectionAsync(string serialNumber)
    {
        logger.LogInformation($"Device {serialNumber} initiated connection");
        return settings.MakeSettingsResponse(serialNumber);
    }
    
    public async Task<bool> ProcessAttendanceDataAsync(string data)
    {
        try
        {
            var records = data.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            logger.LogInformation($"Processing {records.Length} attendance records");

            foreach (var record in records)
            {
                var fields = record.Split('\t');
                if (fields.Length < 5)
                {
                    logger.LogWarning($"Invalid record format: {record}");
                    continue;
                }

                await ProcessAttendanceRecord(fields);
            }

            return true;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error processing attendance data");
            throw;
        }
    }
    private async Task ProcessAttendanceRecord(string[] fields)
    {
        var userId = fields[0].Trim();
        var timestamp = DateTime.Parse(fields[1].Trim());
        
        logger.LogInformation(
            "Retrieved attendance record: User {userId} at {time}",
            userId,
            timestamp
        );
    }
}