using energyconsumptiontracker.Domain;
using energyconsumptiontracker.Persistence;
using Microsoft.EntityFrameworkCore;

public class CustomerAccountPersistence : ICustomerAccountPersistence
{
    private readonly MeterReadingDbContext _context;

    public CustomerAccountPersistence(MeterReadingDbContext context)
    {
        _context = context;
    }

    public async Task StoreCustomerAccounts(CustomerAccount[] customerAccounts)
    {
        await _context.Customers.AddRangeAsync(customerAccounts);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ValidateCustomerId(int customerAccountId)
    {
        return await _context.Customers.AnyAsync(x => x.AccountId == customerAccountId);
    }
}