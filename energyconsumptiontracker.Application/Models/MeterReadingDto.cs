namespace energyconsumptiontracker.Application.Models;

public class MeterReadingDto
{
    public int AccountId { get; set; }
    public DateTime ReadingDate { get; set; }
    public decimal ReadingValue { get; set; }
}
