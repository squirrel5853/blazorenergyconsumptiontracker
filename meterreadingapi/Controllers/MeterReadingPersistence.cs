using energyconsumptiontracker.Application.Models;

namespace meterreadingapi.Data;

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