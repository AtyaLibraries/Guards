using Atya.Foundation.Guards;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace Guards.Benchmarks;

public static class Program
{
    public static void Main(string[] args)
    {
        _ = args;
        BenchmarkRunner.Run<GuardBenchmarks>();
    }
}

[MemoryDiagnoser]
[ShortRunJob]
public class GuardBenchmarks
{
    private readonly string _name = "Ada Lovelace";
    private readonly Guid _id = Guid.Parse("11111111-1111-1111-1111-111111111111");
    private readonly int[] _items = [1, 2, 3, 4, 5];
    private readonly IEnumerable<int> _yieldedItems = Yield(1, 2, 3, 4, 5);

    [Benchmark]
    public string AgainstNullOrWhiteSpace()
    {
        return Guard.AgainstNullOrWhiteSpace(_name);
    }

    [Benchmark]
    public IReadOnlyCollection<int> AgainstNullOrEmptyCollection()
    {
        return Guard.AgainstNullOrEmpty(_items);
    }

    [Benchmark]
    public int AgainstNullOrEmptyEnumerable()
    {
        return Guard.AgainstNullOrEmpty(_yieldedItems).First();
    }

    [Benchmark]
    public Guid AgainstEmptyGuid()
    {
        return Guard.AgainstEmpty(_id);
    }

    [Benchmark]
    public int AgainstOutOfRange()
    {
        return Guard.AgainstOutOfRange(5, 1, 10);
    }

    [Benchmark]
    public int AgainstNegative()
    {
        return Guard.AgainstNegative(5);
    }

    [Benchmark]
    public int AgainstZeroOrNegative()
    {
        return Guard.AgainstZeroOrNegative(5);
    }

    private static IEnumerable<int> Yield(params int[] values)
    {
        foreach (int value in values)
        {
            yield return value;
        }
    }
}
