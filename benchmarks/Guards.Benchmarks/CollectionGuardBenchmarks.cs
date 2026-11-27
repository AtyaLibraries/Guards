using Atya.Foundation.Guards;
using BenchmarkDotNet.Attributes;

namespace Guards.Benchmarks;

/// <summary>
/// Benchmarks read-only collection guard checks against equivalent manual checks.
/// </summary>
public class ReadOnlyCollectionGuardBenchmarks
{
    private int[] _items = [];

    /// <summary>
    /// Gets or sets the number of items used in each benchmark invocation.
    /// </summary>
    [Params(1, 8, 64)]
    public int Count { get; set; }

    /// <summary>
    /// Creates the benchmark input collection.
    /// </summary>
    [GlobalSetup]
    public void Setup()
    {
        _items = Enumerable.Range(1, Count).ToArray();
    }

    /// <summary>
    /// Performs a manual read-only collection count check.
    /// </summary>
    /// <returns>The validated collection.</returns>
    [Benchmark(Baseline = true)]
    public IReadOnlyCollection<int> ManualReadOnlyCollectionCountCheck()
    {
        if (_items.Length == 0)
        {
            throw new ArgumentException("Collection cannot be empty.", nameof(_items));
        }

        return _items;
    }

    /// <summary>
    /// Performs the read-only collection guard check.
    /// </summary>
    /// <returns>The validated collection.</returns>
    [Benchmark]
    public IReadOnlyCollection<int> GuardReadOnlyCollectionNullOrEmpty()
    {
        return Guard.AgainstNullOrEmpty((IReadOnlyCollection<int>)_items);
    }
}

/// <summary>
/// Benchmarks enumerable guard checks against equivalent manual checks.
/// </summary>
public class EnumerableGuardBenchmarks
{
    private int[] _items = [];
    private IEnumerable<int> _enumerable = [];

    /// <summary>
    /// Gets or sets the number of items used in each benchmark invocation.
    /// </summary>
    [Params(1, 8, 64)]
    public int Count { get; set; }

    /// <summary>
    /// Creates the benchmark input enumerable.
    /// </summary>
    [GlobalSetup]
    public void Setup()
    {
        _items = Enumerable.Range(1, Count).ToArray();
        _enumerable = Yield(_items);
    }

    /// <summary>
    /// Performs a manual enumerable emptiness check.
    /// </summary>
    /// <returns><see langword="true"/> when the enumerable contains at least one item.</returns>
    [Benchmark(Baseline = true)]
    public bool ManualEnumerableAnyCheck()
    {
        if (!_enumerable.Any())
        {
            throw new ArgumentException("Collection cannot be empty.", nameof(_enumerable));
        }

        return true;
    }

    /// <summary>
    /// Performs the enumerable guard check.
    /// </summary>
    /// <returns><see langword="true"/> when the guard check succeeds.</returns>
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
