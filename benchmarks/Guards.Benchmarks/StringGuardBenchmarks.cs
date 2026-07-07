using Atya.Foundation.Guards;
using BenchmarkDotNet.Attributes;

namespace Guards.Benchmarks;

/// <summary>
/// Benchmarks string guard checks against equivalent manual checks.
/// </summary>
public class StringGuardBenchmarks
{
    private readonly string _value = "Ada Lovelace";

    /// <summary>
    /// Performs a manual null-or-whitespace string check.
    /// </summary>
    /// <returns>The validated string value.</returns>
    [Benchmark(Baseline = true)]
    public string ManualNullOrWhiteSpaceCheck()
    {
        if (string.IsNullOrWhiteSpace(_value))
        {
            throw new ArgumentException("String cannot be empty or whitespace.", nameof(_value));
        }

        return _value;
    }

    /// <summary>
    /// Performs the null-or-whitespace string guard check.
    /// </summary>
    /// <returns>The validated string value.</returns>
    [Benchmark]
    public string GuardNullOrWhiteSpace()
    {
        return Guard.AgainstNullOrWhiteSpace(_value);
    }
}
