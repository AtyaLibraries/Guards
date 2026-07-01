using Atya.Foundation.Guards;
using BenchmarkDotNet.Attributes;

namespace Guards.Benchmarks;

public class ReadOnlyCollectionGuardBenchmarks
{
    private int[] _items = [];

    [Params(1, 8, 64)]
    public int Count { get; set; }

    [GlobalSetup]
    public void Setup()
    {
        _items = Enumerable.Range(1, Count).ToArray();
    }

    [Benchmark(Baseline = true)]
    public IReadOnlyCollection<int> ManualReadOnlyCollectionCountCheck()
    {
        if (_items.Length == 0)
        {
            throw new ArgumentException("Collection cannot be empty.", nameof(_items));
        }

        return _items;
    }

    [Benchmark]
    public IReadOnlyCollection<int> GuardReadOnlyCollectionNullOrEmpty()
    {
        return Guard.AgainstNullOrEmpty((IReadOnlyCollection<int>)_items);
    }
}

public class EnumerableGuardBenchmarks
{
    private int[] _items = [];
    private IEnumerable<int> _enumerable = [];

    [Params(1, 8, 64)]
    public int Count { get; set; }

    [GlobalSetup]
    public void Setup()
    {
        _items = Enumerable.Range(1, Count).ToArray();
        _enumerable = Yield(_items);
    }

    [Benchmark(Baseline = true)]
    public bool ManualEnumerableAnyCheck()
    {
        if (!_enumerable.Any())
        {
            throw new ArgumentException("Collection cannot be empty.", nameof(_enumerable));
        }

        return true;
    }

    [Benchmark]
    public bool GuardEnumerableNullOrEmpty()
    {
        _ = Guard.AgainstNullOrEmpty(_enumerable);

        return true;
    }

    private static IEnumerable<int> Yield(IEnumerable<int> values)
    {
        foreach (int value in values)
        {
            yield return value;
        }
    }
}
