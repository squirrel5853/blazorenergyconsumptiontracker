namespace energyconsumptiontracker.Domain
{
    public class CustomerAccount
    {
        public CustomerAccount() { }

        public CustomerAccount(int accountId) { 
            AccountId = accountId;
        }

        public int AccountId { get; }
    }
}
