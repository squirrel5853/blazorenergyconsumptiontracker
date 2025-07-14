using CsvHelper.Configuration;
using energyconsumptiontracker.Application.Models;

namespace energyconsumptiontracker.Application.DataImport;

internal class MeterReadingMap : ClassMap<MeterReading>
{
    private const string MeterReadingDateTime = "MeterReadingDateTime";
    private const string MeterReadValue = "MeterReadValue";
    private const string AccountId = "AccountId";

    public MeterReadingMap()
    {
        Map(m => m.AccountId).Name(AccountId);
        Map(m => m.ReadingDate).Name(MeterReadingDateTime).TypeConverterOption.Format("dd/MM/yyyy HH:mm"); ;
        Map(m => m.ReadingValue).Name(MeterReadValue);
    }
}