using energyconsumptiontracker.Application.Models;

namespace meterreadingapi.Services;

public interface IMeterReadingPersistence
{
    Task StoreMeterReadings(MeterReading[] meterReadings);
}