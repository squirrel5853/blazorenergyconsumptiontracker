using energyconsumptiontracker.Application.Models;

namespace energyconsumptiontracker.Application.DataImport
{
    public interface ICsvFileProcessor
    {
        Task<MeterReadingDto[]> ProcessCsvFile(Stream stream);

        Task<AccountDto[]> ProcessAccountCsvFile(Stream stream);
    }
}