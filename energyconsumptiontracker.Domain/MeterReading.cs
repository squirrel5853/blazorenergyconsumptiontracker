
namespace energyconsumptiontracker.Domain
{
    public class MeterReading
    {
        public Guid Id { get; set; }
        public int AccountId { get; set; }
        public DateTime MeterReadingDate { get; set; }
        public decimal MeterReadingValue { get; set; }
    }
}
