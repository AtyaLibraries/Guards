using Atya.Foundation.Guards;
using BenchmarkDotNet.Attributes;

namespace Guards.Benchmarks;

public class IntRangeGuardBenchmarks
{
    [Params(5, 500)]
    public int IntValue { get; set; }

    [Benchmark(Baseline = true)]
    public int ManualIntRangeCheck()
    {
        if (IntValue < 1 || IntValue > 1_000)
        {
            throw new ArgumentOutOfRangeException(nameof(IntValue), IntValue, "Value must be between 1 and 1000.");
        }

        return IntValue;
    }

    [Benchmark]
    public int GuardIntOutOfRange()
    {
        return Guard.AgainstOutOfRange(IntValue, 1, 1_000);
    }
}

public class IntNegativeGuardBenchmarks
{
    [Params(5, 500)]
    public int IntValue { get; set; }

    [Benchmark(Baseline = true)]
    public int ManualIntNegativeCheck()
    {
        if (IntValue < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(IntValue), IntValue, "Value cannot be negative.");
        }

        return IntValue;
    }

    [Benchmark]
    public int GuardIntNegative()
    {
        return Guard.AgainstNegative(IntValue);
    }
}

public class IntZeroOrNegativeGuardBenchmarks
{
    [Params(5, 500)]
    public int IntValue { get; set; }

    [Benchmark(Baseline = true)]
    public int ManualIntZeroOrNegativeCheck()
    {
        if (IntValue <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(IntValue), IntValue, "Value must be greater than zero.");
        }

        return IntValue;
    }

    [Benchmark]
    public int GuardIntZeroOrNegative()
    {
        return Guard.AgainstZeroOrNegative(IntValue);
    }
}
