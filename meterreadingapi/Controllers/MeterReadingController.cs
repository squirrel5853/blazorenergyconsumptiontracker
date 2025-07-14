using energyconsumptiontracker.Application.DataImport;
using energyconsumptiontracker.Application.Models;
using Microsoft.AspNetCore.Components.Forms;

namespace meterreadingapi.Data;

public class MeterReadingController
{
    private readonly ICsvFileProcessor _csvFileProcessor;
    private readonly IMeterReadingPersistence _meterReadingPersistence;

    public MeterReadingController(ICsvFileProcessor csvFileProcessor, IMeterReadingPersistence meterReadingPersistence)
    {
        _csvFileProcessor = csvFileProcessor;
        _meterReadingPersistence = meterReadingPersistence;
    }

    public Task<MeterReading[]> GetMeterReadingsAsync(DateTime startDate)
    {
        return Task.FromResult(Enumerable.Range(1, 5).Select(index => new MeterReading
        {
            ReadingDate = startDate.AddDays(index),
            AccountId = index,
            ReadingValue = Random.Shared.Next(1, 100) + Random.Shared.Next(DateTime.UtcNow.Second)
        }).ToArray());
    }

    public async Task PostMeterReadings(MeterReading[] meterReadings)
    {
        await _meterReadingPersistence.StoreMeterReadings(meterReadings);
    }

    public async Task<MeterReading[]> CreateMeterReadingFromCsv(IBrowserFile browserFile)
    {
        try
        {
            var meterReadings = await _csvFileProcessor.ProcessCsvFile(browserFile.OpenReadStream(maxAllowedSize: 10_000_000));
            return meterReadings;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        return Array.Empty<MeterReading>();
    }
}
