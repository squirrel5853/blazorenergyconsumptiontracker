using energyconsumptiontracker.Application.Models;

namespace meterreadingapi.Data;

public interface IMeterReadingPersistence
{
    Task StoreMeterReadings(MeterReading[] meterReadings);
}