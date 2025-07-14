using energyconsumptiontracker.Application.Models;

namespace energyconsumptiontracker.Application.DataImport
{
    public interface ICsvFileProcessor
    {
        Task<MeterReading[]> ProcessCsvFile(Stream stream);
    }
}