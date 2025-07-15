using energyconsumptiontracker.Application.Models;

namespace energyconsumptiontracker.Application.DataImport;

public class CsvFileProcessor : ICsvFileProcessor
{
    public CsvFileProcessor()
    {
    }

    public async Task<AccountDto[]> ProcessAccountCsvFile(Stream stream)
    {
        var result = new List<AccountDto>();

        using (var reader = new StreamReader(stream))
        {
            using (var csv = new CsvHelper.CsvReader(reader, System.Globalization.CultureInfo.InvariantCulture))
            {
                csv.Context.RegisterClassMap<AccountDtoMap>();
                await foreach (var record in csv.GetRecordsAsync<AccountDto>())
                {
                    result.Add(record);
                }
            }
        }

        return result.ToArray();
    }

    public async Task<MeterReadingDto[]> ProcessCsvFile(Stream stream)
    {
        var result = new List<MeterReadingDto>();

        using (var reader = new StreamReader(stream))
        {
            using (var csv = new CsvHelper.CsvReader(reader, System.Globalization.CultureInfo.InvariantCulture))
            {
                csv.Context.RegisterClassMap<MeterReadingDtoMap>();
                await foreach (var record in csv.GetRecordsAsync<MeterReadingDto>())
                {
                    result.Add(record);
                }
            }
        }

        return result.ToArray();
    }
}
