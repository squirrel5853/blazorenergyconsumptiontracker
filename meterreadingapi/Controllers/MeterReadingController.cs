using energyconsumptiontracker.Application.DataImport;
using energyconsumptiontracker.Application.Models;
using energyconsumptiontracker.Domain;
using Microsoft.AspNetCore.Components.Forms;

namespace meterreadingapi.Controllers;

public class MeterReadingController
{
    private readonly ICsvFileProcessor _csvFileProcessor;
    private readonly IMeterReadingPersistence _meterReadingPersistence;

    public MeterReadingController(ICsvFileProcessor csvFileProcessor, IMeterReadingPersistence meterReadingPersistence)
    {
        _csvFileProcessor = csvFileProcessor;
        _meterReadingPersistence = meterReadingPersistence;
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

    public async Task PostMeterReadings(MeterReadingDto[] meterReadingDTOs)
    {
        List<MeterReading> meterReadings = new List<MeterReading>();
        foreach (var item in meterReadingDTOs)
        {
            meterReadings.Add(new MeterReading()
            {
                Id = Guid.NewGuid(),
                CustomerAccount = new CustomerAccount(item.AccountId),
                MeterReadingDate = item.ReadingDate,
                MeterReadingValue = item.ReadingValue
            });
        }
        await _meterReadingPersistence.StoreMeterReadings(meterReadings.ToArray());
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
}
