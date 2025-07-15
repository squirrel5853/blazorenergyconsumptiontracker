using CsvHelper.Configuration;
using energyconsumptiontracker.Application.Models;

namespace energyconsumptiontracker.Application.DataImport;

internal class AccountDtoMap : ClassMap<AccountDto>
{
    private const string FirstName = "FirstName";
    private const string LastName = "LastName";
    private const string AccountId = "AccountId";

    public AccountDtoMap()
    {
        Map(m => m.Id).Name(AccountId);
        Map(m => m.FirstName).Name(FirstName);
        Map(m => m.LastName).Name(LastName);
    }
}