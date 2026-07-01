using Atya.Foundation.Guards;
using BenchmarkDotNet.Attributes;

namespace Guards.Benchmarks;

public class StringGuardBenchmarks
{
    private readonly string _value = "Ada Lovelace";

    [Benchmark(Baseline = true)]
    public string ManualNullOrWhiteSpaceCheck()
    {
        if (string.IsNullOrWhiteSpace(_value))
        {
            throw new ArgumentException("String cannot be empty or whitespace.", nameof(_value));
        }

        return _value;
    }

    [Benchmark]
    public string GuardNullOrWhiteSpace()
    {
        return Guard.AgainstNullOrWhiteSpace(_value);
    }
}
