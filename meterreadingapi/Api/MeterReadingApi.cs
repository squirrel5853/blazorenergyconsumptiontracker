using meterreadingapi.Services;
using Microsoft.AspNetCore.Mvc;

namespace meterreadingapi.Api;

[ApiController]
[Route("api/meterreading")]
public class MeterReadingApi : ControllerBase
{
    private MeterReadingService _meterReadingController;

    public MeterReadingApi(MeterReadingService meterReadingController)
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
        await _meterReadingController.PostMeterReadings(meterReadings);

        return new OkResult();
    }
}
