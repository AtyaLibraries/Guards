using Atya.Foundation.Guards;

namespace Guards.Samples.Console;

/// <summary>
/// Runs the Atya.Foundation.Guards console sample.
/// </summary>
public static class Program
{
    /// <summary>
    /// Demonstrates constructing a guarded request and handling expected guard failures.
    /// </summary>
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

/// <summary>
/// Represents an order request whose constructor arguments are validated with guard clauses.
/// </summary>
/// <param name="customerId">The non-empty customer identifier.</param>
/// <param name="customerName">The non-empty customer display name.</param>
/// <param name="email">The non-empty customer email address.</param>
/// <param name="quantity">The positive ordered quantity.</param>
public sealed class OrderRequest(Guid customerId, string customerName, string email, int quantity)
{
    /// <summary>
    /// Gets the validated customer identifier.
    /// </summary>
    public Guid CustomerId
    {
        get;
    } = Guard.AgainstEmpty(customerId);

    /// <summary>
    /// Gets the validated customer display name.
    /// </summary>
    public string CustomerName
    {
        get;
    } = Guard.AgainstNullOrWhiteSpace(customerName);

    /// <summary>
    /// Gets the validated customer email address.
    /// </summary>
    public string Email
    {
        get;
    } = Guard.AgainstNullOrWhiteSpace(email);

    /// <summary>
    /// Gets the validated ordered quantity.
    /// </summary>
    public int Quantity
    {
        get;
    } = Guard.AgainstZeroOrNegative(quantity);
}
