using energyconsumptiontracker.Application.DataImport;
using energyconsumptiontracker.Application.Models;
using energyconsumptiontracker.Domain;
using Microsoft.AspNetCore.Components.Forms;

namespace meterreadingapi.Controllers;

public class MeterReadingController
{
    private readonly ICsvFileProcessor _csvFileProcessor;
    private readonly IMeterReadingService _meterReadingService;

    public MeterReadingController(ICsvFileProcessor csvFileProcessor, IMeterReadingService meterReadingService)
    {
        _csvFileProcessor = csvFileProcessor;
        _meterReadingService = meterReadingService;
    }

    public Task<MeterReadingDto[]> GetMeterReadingsAsync(DateTime startDate)
    {
        return Task.FromResult(Enumerable.Range(1, 5).Select(index => new MeterReadingDto
        {
            ReadingDate = startDate.AddDays(index),
            AccountId = index,
            ReadingValue = Random.Shared.Next(1, 100) + Random.Shared.Next(DateTime.UtcNow.Second)
        }).ToArray());
    }

    public async Task<StoreResultDto> PostMeterReadings(MeterReadingDto[] meterReadingDTOs)
    {
        List<MeterReading> meterReadings = new List<MeterReading>();
        foreach (var item in meterReadingDTOs)
        {
            meterReadings.Add(new MeterReading()
            {
                Id = Guid.NewGuid(),
                AccountId = item.AccountId,
                MeterReadingDate = item.ReadingDate,
                MeterReadingValue = item.ReadingValue
            });
        }
        var storeResult = await _meterReadingService.StoreMeterReadings(meterReadings.ToArray());
        return new StoreResultDto() { SuccessCount = storeResult.SuccessCount, FailureCount = storeResult.FailureCount };
    }


    public async Task<MeterReadingDto[]> CreateMeterReadingFromCsv(IBrowserFile browserFile)
    {
        return await CreateMeterReadingFromStream(browserFile.OpenReadStream(maxAllowedSize: 10_000_000));
    }

    internal async Task<MeterReadingDto[]> CreateMeterReadingFromStream(Stream stream)
    {
        try
        {
            var meterReadings = await _csvFileProcessor.ProcessCsvFile(stream);
            return meterReadings;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        return Array.Empty<MeterReadingDto>();
    }

    public async Task ResetMeterReadings()
    {
        await _meterReadingService.ClearMeterReadings();
    }
}
