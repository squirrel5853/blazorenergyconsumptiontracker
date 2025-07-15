using energyconsumptiontracker.Domain;
using energyconsumptiontracker.Persistence;

public class MeterReadingPersistence : IMeterReadingPersistence
{
    private readonly MeterReadingDbContext _context;

    public MeterReadingPersistence(MeterReadingDbContext context)
    {
        _context = context;
    }

    public async Task StoreMeterReadings(MeterReading[] meterReadings)
    {
        // Add validation logic if needed
        await _context.MeterReadings.AddRangeAsync(meterReadings);
        await _context.SaveChangesAsync();
    }
}