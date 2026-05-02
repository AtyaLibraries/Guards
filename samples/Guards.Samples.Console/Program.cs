using Atya.Foundation.Guards;

namespace Guards.Samples.Console;

public static class Program
{
    public static void Main()
    {
        System.Console.WriteLine("Atya.Foundation.Guards sample");

        OrderRequest request = new(
            Guid.NewGuid(),
            "Ada Lovelace",
            "ada@example.com",
            25);

        System.Console.WriteLine($"Validated request for {request.CustomerName} with quantity {request.Quantity}.");

        try
        {
            _ = new OrderRequest(Guid.Empty, " ", "ada@example.com", 0);
        }
        catch (ArgumentException exception)
        {
            System.Console.WriteLine($"Expected guard exception: {exception.Message}");
        }
    }
}

public sealed class OrderRequest(Guid customerId, string customerName, string email, int quantity)
{
    public Guid CustomerId
    {
        get;
    } = Guard.AgainstEmpty(customerId);

    public string CustomerName
    {
        get;
    } = Guard.AgainstNullOrWhiteSpace(customerName);

    public string Email
    {
        get;
    } = Guard.AgainstNullOrWhiteSpace(email);

    public int Quantity
    {
        get;
    } = Guard.AgainstZeroOrNegative(quantity);
}
