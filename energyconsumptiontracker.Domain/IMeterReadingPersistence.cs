namespace energyconsumptiontracker.Domain;

public interface IMeterReadingPersistence
{
    Task Clear();
    Task StoreMeterReadings(MeterReading[] meterReadings);
}