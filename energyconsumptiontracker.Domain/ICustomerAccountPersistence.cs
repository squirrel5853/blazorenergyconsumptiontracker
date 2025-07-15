namespace energyconsumptiontracker.Domain;

public interface ICustomerAccountPersistence
{
    Task<List<CustomerAccount>> GetCustomersByIds(int[] accountIds);

    Task StoreCustomerAccounts(CustomerAccount[] customerAccounts);

    Task<bool> ValidateCustomerId(int customerAccountId);
}