namespace energyconsumptiontracker.Domain;

public interface IMeterReadingPersistence
{
    Task StoreMeterReadings(MeterReading[] meterReadings);
}