using Atya.Foundation.Guards;
using BenchmarkDotNet.Attributes;

namespace Guards.Benchmarks;

/// <summary>
/// Benchmarks integer range guard checks against equivalent manual checks.
/// </summary>
public class IntRangeGuardBenchmarks
{
    /// <summary>
    /// Gets or sets the integer value used in each benchmark invocation.
    /// </summary>
    [Params(5, 500)]
    public int IntValue { get; set; }

    /// <summary>
    /// Performs a manual integer range check.
    /// </summary>
    /// <returns>The validated integer value.</returns>
    [Benchmark(Baseline = true)]
    public int ManualIntRangeCheck()
    {
        if (IntValue < 1 || IntValue > 1_000)
        {
            throw new ArgumentOutOfRangeException(nameof(IntValue), IntValue, "Value must be between 1 and 1000.");
        }

        return IntValue;
    }

    /// <summary>
    /// Performs the integer range guard check.
    /// </summary>
    /// <returns>The validated integer value.</returns>
    [Benchmark]
    public int GuardIntOutOfRange()
    {
        return Guard.AgainstOutOfRange(IntValue, 1, 1_000);
    }
}

/// <summary>
/// Benchmarks negative integer guard checks against equivalent manual checks.
/// </summary>
public class IntNegativeGuardBenchmarks
{
    /// <summary>
    /// Gets or sets the integer value used in each benchmark invocation.
    /// </summary>
    [Params(5, 500)]
    public int IntValue { get; set; }

    /// <summary>
    /// Performs a manual negative integer check.
    /// </summary>
    /// <returns>The validated integer value.</returns>
    [Benchmark(Baseline = true)]
    public int ManualIntNegativeCheck()
    {
        if (IntValue < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(IntValue), IntValue, "Value cannot be negative.");
        }

        return IntValue;
    }

    /// <summary>
    /// Performs the negative integer guard check.
    /// </summary>
    /// <returns>The validated integer value.</returns>
    [Benchmark]
    public int GuardIntNegative()
    {
        return Guard.AgainstNegative(IntValue);
    }
}

/// <summary>
/// Benchmarks zero-or-negative integer guard checks against equivalent manual checks.
/// </summary>
public class IntZeroOrNegativeGuardBenchmarks
{
    /// <summary>
    /// Gets or sets the integer value used in each benchmark invocation.
    /// </summary>
    [Params(5, 500)]
    public int IntValue { get; set; }

    /// <summary>
    /// Performs a manual zero-or-negative integer check.
    /// </summary>
    /// <returns>The validated integer value.</returns>
    [Benchmark(Baseline = true)]
    public int ManualIntZeroOrNegativeCheck()
    {
        if (IntValue <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(IntValue), IntValue, "Value must be greater than zero.");
        }

        return IntValue;
    }

    /// <summary>
    /// Performs the zero-or-negative integer guard check.
    /// </summary>
    /// <returns>The validated integer value.</returns>
    [Benchmark]
    public int GuardIntZeroOrNegative()
    {
        return Guard.AgainstZeroOrNegative(IntValue);
    }
}
