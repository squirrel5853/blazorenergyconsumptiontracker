using energyconsumptiontracker.Application.Models;

namespace energyconsumptiontracker.Application.DataImport;

public class CsvFileProcessor : ICsvFileProcessor
{
    public async Task<MeterReading[]> ProcessCsvFile(Stream stream)
    {
        var result = new List<MeterReading>();

        using (var reader = new StreamReader(stream))
        {
            using (var csv = new CsvHelper.CsvReader(reader, System.Globalization.CultureInfo.InvariantCulture))
            {
                csv.Context.RegisterClassMap<MeterReadingMap>();
                await foreach (var record in csv.GetRecordsAsync<MeterReading>())
                {
                    result.Add(record);
                }
            }
        }

        return result.ToArray();
    }
}
