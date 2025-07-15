
using energyconsumptiontracker.Application.DataImport;
using energyconsumptiontracker.Domain;

namespace meterreadingapi;

internal class DatabaseSeeder
{
    private ICsvFileProcessor _csvFileProcessor;
    private ICustomerAccountPersistence _customerAccountPersistence;

    public DatabaseSeeder(ICsvFileProcessor processor, ICustomerAccountPersistence customerAccountPersistence)
    {
        _csvFileProcessor = processor;
        _customerAccountPersistence = customerAccountPersistence;
    }

    internal async Task SeedAsync(string csvPath)
    {
        if (!File.Exists(csvPath))
        {
            Console.WriteLine($"CSV file not found at: {csvPath}");
            return;
        }

        await using var stream = File.OpenRead(csvPath);
        var accountDtos = await _csvFileProcessor.ProcessAccountCsvFile(stream);

        if (accountDtos.Length > 0)
        {
            Console.WriteLine($"Seeding {accountDtos.Length} accounts from CSV...");

            List<CustomerAccount> accounts = new List<CustomerAccount>();
            foreach (var item in accountDtos)
            {
                accounts.Add(new CustomerAccount(item.Id));
            }

            await _customerAccountPersistence.StoreCustomerAccounts(accounts.ToArray());
        }
        else
        {
            Console.WriteLine("No accounts found in CSV.");
        }
    }
}