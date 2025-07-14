namespace energyconsumptiontracker.Application.Models;

public class MeterReading
{
    public int AccountId { get; set; }
    public DateTime ReadingDate { get; set; }
    public decimal ReadingValue { get; set; }
}
