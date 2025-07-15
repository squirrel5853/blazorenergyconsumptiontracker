using CsvHelper.Configuration;
using energyconsumptiontracker.Application.Models;

namespace energyconsumptiontracker.Application.DataImport;

internal class MeterReadingDtoMap : ClassMap<MeterReadingDto>
{
    private const string MeterReadingDateTime = "MeterReadingDateTime";
    private const string MeterReadValue = "MeterReadValue";
    private const string AccountId = "AccountId";

    public MeterReadingDtoMap()
    {
        Map(m => m.AccountId).Name(AccountId);
        Map(m => m.ReadingDate).Name(MeterReadingDateTime).TypeConverterOption.Format("dd/MM/yyyy HH:mm"); ;
        Map(m => m.ReadingValue).Name(MeterReadValue);
    }
}