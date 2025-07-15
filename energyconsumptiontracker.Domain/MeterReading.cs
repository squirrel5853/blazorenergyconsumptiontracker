
namespace energyconsumptiontracker.Domain
{
    public class MeterReading
    {
        public Guid Id { get; set; }
        public required CustomerAccount CustomerAccount { get; set; }
        public DateTime MeterReadingDate { get; set; }
        public decimal MeterReadingValue { get; set; }
    }
}
