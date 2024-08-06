namespace FinancialManager.Core;

public class Transaction
{
    public required string TransactionName { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public ushort DaysRecurring { get; set; }
}

public class Income : Transaction
{
}

public class Expense : Transaction
{
}
