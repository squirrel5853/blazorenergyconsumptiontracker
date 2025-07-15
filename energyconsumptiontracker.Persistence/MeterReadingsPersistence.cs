using energyconsumptiontracker.Domain;
using energyconsumptiontracker.Persistence;
using Microsoft.EntityFrameworkCore;

public class MeterReadingPersistence : IMeterReadingPersistence
{
    private readonly MeterReadingDbContext _context;

    public MeterReadingPersistence(MeterReadingDbContext context)
    {
        _context = context;
    }

    public async Task Clear()
    {
        var readings = await _context.MeterReadings.ToArrayAsync();
        _context.MeterReadings.RemoveRange(readings);
        await _context.SaveChangesAsync();
    }

    public async Task StoreMeterReadings(MeterReading[] meterReadings)
    {
        // Add validation logic if needed
        await _context.MeterReadings.AddRangeAsync(meterReadings);
        await _context.SaveChangesAsync();
    }
}