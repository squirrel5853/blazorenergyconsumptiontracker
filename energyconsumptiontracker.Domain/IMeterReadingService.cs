namespace energyconsumptiontracker.Domain;

public interface IMeterReadingService
{
    public Task ProcessMeterReadings(IEnumerable<MeterReading> meterReadings);
}
