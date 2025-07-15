namespace energyconsumptiontracker.Domain;

public interface IMeterReadingService
{
    Task ClearMeterReadings();
    Task<StoreResult> StoreMeterReadings(MeterReading[] meterReadings);
}
