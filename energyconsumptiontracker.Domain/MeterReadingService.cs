
namespace energyconsumptiontracker.Domain;

public class MeterReadingService : IMeterReadingService
{
    private ICustomerAccountPersistence _customerAccountPersistence;
    private IMeterReadingPersistence _meterReadingPersistence;

    public MeterReadingService(ICustomerAccountPersistence customerAccountPersistence, IMeterReadingPersistence meterReadingPersistence)
    { 
        _customerAccountPersistence = customerAccountPersistence;
        _meterReadingPersistence = meterReadingPersistence;
    }

    public async Task ClearMeterReadings()
    {
        await _meterReadingPersistence.Clear();
    }

    public async Task<StoreResult> StoreMeterReadings(MeterReading[] meterReadings)
    {
        var result = new StoreResult();
        //group by customer account
        var groupedMeterReadings = meterReadings.GroupBy(x => x.AccountId);

        //process batches of readings by customer
        foreach (IGrouping<int, MeterReading> customerMeterReadings in groupedMeterReadings)
        {
            try
            {
                if (await _customerAccountPersistence.ValidateCustomerId(customerMeterReadings.Key))
                {
                    await _meterReadingPersistence.StoreMeterReadings(customerMeterReadings.ToArray());
                    result.SuccessCount += customerMeterReadings.Count();
                }
                else
                {
                    result.FailureCount += customerMeterReadings.Count();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                result.FailureCount += customerMeterReadings.Count();
            }
        }

        return result;
    }
}
