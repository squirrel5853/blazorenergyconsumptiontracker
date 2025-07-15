namespace energyconsumptiontracker.Domain;

public interface ICustomerAccountPersistence
{
    Task StoreCustomerAccounts(CustomerAccount[] customerAccounts);

    Task<bool> ValidateCustomerId(int customerAccountId);
}