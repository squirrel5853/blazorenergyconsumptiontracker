using meterreadingapi.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace meterreadingapi.Api;

[ApiController]
[Route("api/meterreading")]
public class MeterReadingApi : ControllerBase
{
    private MeterReadingController _meterReadingController;

    public MeterReadingApi(MeterReadingController meterReadingController)
    {
        _meterReadingController = meterReadingController;
    }

    [HttpPost("meter-reading-uploads")]
    public async Task<IActionResult> UploadAsync([FromForm] IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return new BadRequestResult();
        }

        using var stream = file.OpenReadStream();

        var meterReadings = await _meterReadingController.CreateMeterReadingFromStream(stream);
        var result = await _meterReadingController.PostMeterReadings(meterReadings);

        return new OkObjectResult(result);
    }

    [HttpDelete("meter-readings-clear")]
    public async Task<IActionResult> Reset()
    { 
        await _meterReadingController.ResetMeterReadings();

        return new OkResult();
    }
}
