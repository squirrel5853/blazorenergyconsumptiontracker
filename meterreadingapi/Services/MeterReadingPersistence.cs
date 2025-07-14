using energyconsumptiontracker.Application.Models;

namespace meterreadingapi.Services;

internal class MeterReadingPersistence : IMeterReadingPersistence
{
    public MeterReadingPersistence()
    {
    }

    public async Task StoreMeterReadings(MeterReading[] meterReadings)
    {
        throw new NotImplementedException();
    }
}