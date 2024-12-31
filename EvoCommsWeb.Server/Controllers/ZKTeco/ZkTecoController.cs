using EvoCommsWeb.Server.Terminals.ZK;
using EvoCommsWeb.Server.Terminals.ZK.Requests;
using Microsoft.AspNetCore.Mvc;

namespace EvoCommsWeb.Server.Controllers.ZKTeco;

[ApiController]
[Route("iclock")]
[Produces("text/plain")]
[Tags("ZKTeco Terminal")]
public class ZkTecoController(
    ILogger<ZkTecoController> logger,
    ZkService zkService)
    : ControllerBase
{
    /// <summary>
    /// Handles initial device connection and configuration
    /// </summary>
    /// <param name="request">Connection parameters</param>
    /// <remarks>
    /// Sample request:
    /// GET /iclock/cdata?SN=OID7054297041500253&amp;options=all&amp;pushver=2.2.14&amp;language=73
    /// 
    /// Sample response:
    /// ```
    /// GET OPTION FROM: 354313
    /// Stamp=82983982
    /// OpStamp=9238883
    /// ```
    /// </remarks>
    [HttpGet("cdata")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> HandleInitialConnection([FromQuery] ZkConnectionRequest request)
    {
        try
        {
            await LogRequestDetails();

            var serialNumber = Request.Query["SN"].ToString();
            if (string.IsNullOrEmpty(serialNumber))
            {
                logger.LogWarning("Device connection attempt without serial number");
                return BadRequest("Serial number is required");
            }

            var response = zkService.HandleInitialConnectionAsync(serialNumber);
            return Content(response);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error handling device connection");
            return StatusCode(500, "Internal server error");
        }
    }

    /// <summary>
    /// Handles device command requests and can send commands to the device
    /// </summary>
    /// <param name="sn">Device Serial Number</param>
    /// <remarks>
    /// Sample request:
    /// GET /iclock/getrequest?SN=OID7054297041500253
    /// 
    /// Sample response (changing user PIN):
    /// ```
    /// C:122:INFO
    /// C:123:DATA USER PIN=852 Name=Wawan
    /// ```
    /// </remarks>
    [HttpGet("getrequest")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ContentResult HandleGetRequest([FromQuery] string sn)
    {
        return Content("OK");
    }
    
    /// <summary>
    /// Receives device command execution results
    /// </summary>
    /// <param name="sn">Device Serial Number</param>
    /// <remarks>
    /// Sample request:
    /// POST /iclock/devicecmd?SN=OID7054297041500253
    /// 
    /// Sample response:
    /// ```
    /// OK
    /// ```
    /// </remarks>
    [HttpPost("devicecmd")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    public ContentResult HandleDeviceCommand([FromQuery] string sn)
    {
        return Content("OK");
    }

    /// <summary>
    /// Receives attendance data from the device
    /// </summary>
    /// <param name="request">Attendance data parameters</param>
    /// <remarks>
    /// Sample request:
    /// POST /iclock/cdata?SN=0316144680030&amp;table=ATTLOG&amp;Stamp=9999
    /// Content: 1452 2015-07-30 15:16:28 0 1 0 0 0
    /// 
    /// Sample response:
    /// ```
    /// OK: 1
    /// ```
    /// </remarks>
    [HttpPost("cdata")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    public async Task<IActionResult> HandleAttendanceData([FromQuery] ZkAttendanceDataRequest request)
    {
        try
        {
            using var reader = new StreamReader(Request.Body);
            var data = await reader.ReadToEndAsync();

            if (Request.Query.ContainsKey("table") && 
                Request.Query["table"].ToString() == "ATTLOG")
            {
                await zkService.ProcessAttendanceDataAsync(data);
                return Content("OK");
            }

            if (data.Contains("OPERLOG"))
            {
                // Handle operation log
                return Content("OK");
            }

            return Content("OK");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error processing attendance data");
            return StatusCode(500, "Internal server error");
        }
    }

    private async Task LogRequestDetails()
    {
        foreach (var header in Request.Headers)
        {
            logger.LogInformation($"Header: {header.Key} = {header.Value}");
        }

        Request.EnableBuffering();
        Request.Body.Position = 0;
        
        var rawRequestBody = await new StreamReader(Request.Body).ReadToEndAsync();
        logger.LogInformation($"Request body: {rawRequestBody}");
        
        Request.Body.Position = 0;
    }
}