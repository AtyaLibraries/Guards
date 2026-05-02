using System.Collections;

namespace Atya.Foundation.Guards.UnitTests;

public sealed class GuardTests
{
    [Fact]
    public void AgainstNull_WithReferenceType_Should_Return_Value_When_NotNull()
    {
        string input = "value";

        string result = Guard.AgainstNull(input);

        result.Should().Be(input);
    }

    [Fact]
    public void AgainstNull_WithReferenceType_Should_ThrowArgumentNullException_When_Null()
    {
        string? input = null;

        Action action = () => Guard.AgainstNull(input);

        action.Should()
            .Throw<ArgumentNullException>()
            .Which.ParamName.Should()
            .Be("input");
    }

    [Fact]
    public void AgainstNull_WithNullableStruct_Should_Return_Value_When_NotNull()
    {
        int? input = 42;

        int result = Guard.AgainstNull(input);

        result.Should().Be(42);
    }

    [Fact]
    public void AgainstNull_WithNullableStruct_Should_ThrowArgumentNullException_When_Null()
    {
        int? input = null;

        Action action = () => Guard.AgainstNull(input);

        action.Should()
            .Throw<ArgumentNullException>()
            .Which.ParamName.Should()
            .Be("input");
    }

    [Fact]
    public void AgainstNullOrEmpty_Should_Return_Value_When_Valid()
    {
        const string input = "ABC";

        string result = Guard.AgainstNullOrEmpty(input);

        result.Should().Be(input);
    }

    [Fact]
    public void AgainstNullOrEmpty_Should_ThrowArgumentNullException_When_Null()
    {
        string? input = null;

        Action action = () => Guard.AgainstNullOrEmpty(input);

        action.Should()
            .Throw<ArgumentNullException>()
            .Which.ParamName.Should()
            .Be("input");
    }

    [Fact]
    public void AgainstNullOrEmpty_Should_ThrowArgumentException_When_Empty()
    {
        string input = string.Empty;

        Action action = () => Guard.AgainstNullOrEmpty(input);

        action.Should()
            .Throw<ArgumentException>()
            .Where(ex => ex.ParamName == "input" && ex.Message.Contains("String cannot be empty."));
    }

    [Fact]
    public void AgainstNullOrWhiteSpace_Should_Return_Value_When_Valid()
    {
        const string input = "Valid Name";

        string result = Guard.AgainstNullOrWhiteSpace(input);

        result.Should().Be(input);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("   ")]
    [InlineData("\t")]
    public void AgainstNullOrWhiteSpace_Should_ThrowArgumentException_When_Invalid(string input)
    {
        Action action = () => Guard.AgainstNullOrWhiteSpace(input);

        action.Should()
            .Throw<ArgumentException>()
            .Where(ex => ex.ParamName == "input" && ex.Message.Contains("String cannot be empty or whitespace."));
    }

    [Fact]
    public void AgainstDefault_Should_Return_Value_When_NotDefault()
    {
        int input = 5;

        int result = Guard.AgainstDefault(input);

        result.Should().Be(5);
    }

    [Fact]
    public void AgainstDefault_Should_ThrowArgumentException_When_Default()
    {
        int input = default;

        Action action = () => Guard.AgainstDefault(input);

        action.Should()
            .Throw<ArgumentException>()
            .Where(ex => ex.ParamName == "input" && ex.Message.Contains("Value cannot be the default value."));
    }

    [Fact]
    public void AgainstEmpty_Should_Return_Guid_When_NotEmpty()
    {
        Guid input = Guid.NewGuid();

        Guid result = Guard.AgainstEmpty(input);

        result.Should().Be(input);
    }

    [Fact]
    public void AgainstEmpty_Should_ThrowArgumentException_When_GuidEmpty()
    {
        Guid input = Guid.Empty;

        Action action = () => Guard.AgainstEmpty(input);

        action.Should()
            .Throw<ArgumentException>()
            .Where(ex => ex.ParamName == "input" && ex.Message.Contains("Guid cannot be empty."));
    }

    [Fact]
    public void AgainstNullOrEmpty_ForReadOnlyCollection_Should_Return_Value_When_NotEmpty()
    {
        IReadOnlyCollection<int> input = [1, 2, 3];

        IReadOnlyCollection<int> result = Guard.AgainstNullOrEmpty(input);

        result.Should().BeSameAs(input);
    }

    [Fact]
    public void AgainstNullOrEmpty_ForReadOnlyCollection_Should_ThrowArgumentNullException_When_Null()
    {
        IReadOnlyCollection<int>? input = null;

        Action action = () => Guard.AgainstNullOrEmpty(input);

        action.Should()
            .Throw<ArgumentNullException>()
            .Which.ParamName.Should()
            .Be("input");
    }

    [Fact]
    public void AgainstNullOrEmpty_ForReadOnlyCollection_Should_ThrowArgumentException_When_Empty()
    {
        IReadOnlyCollection<int> input = Array.Empty<int>();

        Action action = () => Guard.AgainstNullOrEmpty(input);

        action.Should()
            .Throw<ArgumentException>()
            .Where(ex => ex.ParamName == "input" && ex.Message.Contains("Collection cannot be empty."));
    }

    [Fact]
    public void AgainstNullOrEmpty_ForEnumerable_Should_Return_Value_When_NotEmpty()
    {
        IEnumerable<int> input = Yield(1, 2, 3);

        IEnumerable<int> result = Guard.AgainstNullOrEmpty(input);

        result.Should().BeSameAs(input);
    }

    [Fact]
    public void AgainstNullOrEmpty_ForEnumerable_Should_ThrowArgumentNullException_When_Null()
    {
        IEnumerable<int>? input = null;

        Action action = () => Guard.AgainstNullOrEmpty(input);

        action.Should()
            .Throw<ArgumentNullException>()
            .Where(ex => ex.ParamName == "input");
    }

    [Fact]
    public void AgainstNullOrEmpty_ForEnumerable_Should_Not_Enumerate_ReadOnlyCollection()
    {
        IEnumerable<int> input = new CountingReadOnlyCollection<int>([1, 2, 3]);

        IEnumerable<int> result = Guard.AgainstNullOrEmpty(input);

        result.Should().BeSameAs(input);
        ((CountingReadOnlyCollection<int>)input).GetEnumeratorCallCount.Should().Be(0);
    }

    [Fact]
    public void AgainstNullOrEmpty_ForEnumerable_Should_ThrowArgumentException_When_ReadOnlyCollection_Empty()
    {
        IEnumerable<int> input = new CountingReadOnlyCollection<int>([]);

        Action action = () => Guard.AgainstNullOrEmpty(input);

        action.Should()
            .Throw<ArgumentException>()
            .Where(ex => ex.ParamName == "input" && ex.Message.Contains("Collection cannot be empty."));
    }

    [Fact]
    public void AgainstNullOrEmpty_ForEnumerable_Should_Return_Value_When_NonGenericCollection_NotEmpty()
    {
        IEnumerable<int> input = new CountingCollection<int>([1, 2, 3]);

        IEnumerable<int> result = Guard.AgainstNullOrEmpty(input);

        result.Should().BeSameAs(input);
        ((CountingCollection<int>)input).GetEnumeratorCallCount.Should().Be(0);
    }

    [Fact]
    public void AgainstNullOrEmpty_ForEnumerable_Should_ThrowArgumentException_When_NonGenericCollection_Empty()
    {
        IEnumerable<int> input = new CountingCollection<int>([]);

        Action action = () => Guard.AgainstNullOrEmpty(input);

        action.Should()
            .Throw<ArgumentException>()
            .Where(ex => ex.ParamName == "input" && ex.Message.Contains("Collection cannot be empty."));
    }

    [Fact]
    public void AgainstNullOrEmpty_ForEnumerable_Should_ThrowArgumentException_When_Empty()
    {
        IEnumerable<int> input = Yield();

        Action action = () => Guard.AgainstNullOrEmpty(input);

        action.Should()
            .Throw<ArgumentException>()
            .Where(ex => ex.ParamName == "input" && ex.Message.Contains("Collection cannot be empty."));
    }

    [Theory]
    [InlineData(1, 1, 10)]
    [InlineData(10, 1, 10)]
    [InlineData(5, 1, 10)]
    public void AgainstOutOfRange_ForInt_Should_Return_Value_When_InRange(int input, int minimum, int maximum)
    {
        int result = Guard.AgainstOutOfRange(input, minimum, maximum);

        result.Should().Be(input);
    }

    [Fact]
    public void AgainstOutOfRange_ForInt_Should_ThrowArgumentOutOfRangeException_When_OutOfRange()
    {
        int input = 11;

        Action action = () => Guard.AgainstOutOfRange(input, 1, 10);

        action.Should()
            .Throw<ArgumentOutOfRangeException>()
            .Where(ex => ex.ParamName == "input" && ex.Message.Contains("between 1 and 10"));
    }

    [Fact]
    public void AgainstOutOfRange_ForInt_Should_ThrowArgumentOutOfRangeException_When_BelowRange()
    {
        int input = 0;

        Action action = () => Guard.AgainstOutOfRange(input, 1, 10);

        action.Should()
            .Throw<ArgumentOutOfRangeException>()
            .Where(ex => ex.ParamName == "input" && ex.Message.Contains("between 1 and 10"));
    }

    [Fact]
    public void AgainstOutOfRange_Should_ThrowArgumentException_When_Minimum_GreaterThanMaximum()
    {
        const int input = 5;

        Action action = () => Guard.AgainstOutOfRange(input, 10, 1);

        action.Should()
            .Throw<ArgumentException>()
            .Where(ex => ex.ParamName == "minimum" && ex.Message.Contains("Minimum cannot be greater than maximum."));
    }

    [Fact]
    public void AgainstOutOfRange_ForLong_Should_Return_Value_When_InRange()
    {
        long input = 5L;

        long result = Guard.AgainstOutOfRange(input, 1L, 10L);

        result.Should().Be(input);
    }

    [Fact]
    public void AgainstOutOfRange_ForLong_Should_ThrowArgumentOutOfRangeException_When_OutOfRange()
    {
        long input = 11L;

        Action action = () => Guard.AgainstOutOfRange(input, 1L, 10L);

        action.Should()
            .Throw<ArgumentOutOfRangeException>()
            .Where(ex => ex.ParamName == "input" && ex.Message.Contains("between 1 and 10"));
    }

    [Fact]
    public void AgainstOutOfRange_ForLong_Should_ThrowArgumentOutOfRangeException_When_BelowRange()
    {
        long input = 0L;

        Action action = () => Guard.AgainstOutOfRange(input, 1L, 10L);

        action.Should()
            .Throw<ArgumentOutOfRangeException>()
            .Where(ex => ex.ParamName == "input" && ex.Message.Contains("between 1 and 10"));
    }

    [Fact]
    public void AgainstOutOfRange_ForLong_Should_ThrowArgumentException_When_Minimum_GreaterThanMaximum()
    {
        const long input = 5L;

        Action action = () => Guard.AgainstOutOfRange(input, 10L, 1L);

        action.Should()
            .Throw<ArgumentException>()
            .Where(ex => ex.ParamName == "minimum" && ex.Message.Contains("Minimum cannot be greater than maximum."));
    }

    [Fact]
    public void AgainstOutOfRange_ForDecimal_Should_Return_Value_When_InRange()
    {
        decimal input = 5m;

        decimal result = Guard.AgainstOutOfRange(input, 1m, 10m);

        result.Should().Be(input);
    }

    [Fact]
    public void AgainstOutOfRange_ForDecimal_Should_ThrowArgumentOutOfRangeException_When_OutOfRange()
    {
        decimal input = 11m;

        Action action = () => Guard.AgainstOutOfRange(input, 1m, 10m);

        action.Should()
            .Throw<ArgumentOutOfRangeException>()
            .Where(ex => ex.ParamName == "input" && ex.Message.Contains("between 1 and 10"));
    }

    [Fact]
    public void AgainstOutOfRange_ForDecimal_Should_ThrowArgumentOutOfRangeException_When_BelowRange()
    {
        decimal input = 0m;

        Action action = () => Guard.AgainstOutOfRange(input, 1m, 10m);

        action.Should()
            .Throw<ArgumentOutOfRangeException>()
            .Where(ex => ex.ParamName == "input" && ex.Message.Contains("between 1 and 10"));
    }

    [Fact]
    public void AgainstOutOfRange_ForDecimal_Should_ThrowArgumentException_When_Minimum_GreaterThanMaximum()
    {
        const decimal input = 5m;

        Action action = () => Guard.AgainstOutOfRange(input, 10m, 1m);

        action.Should()
            .Throw<ArgumentException>()
            .Where(ex => ex.ParamName == "minimum" && ex.Message.Contains("Minimum cannot be greater than maximum."));
    }

    [Fact]
    public void AgainstNegative_ForInt_Should_Return_Value_When_Zero()
    {
        int input = 0;

        int result = Guard.AgainstNegative(input);

        result.Should().Be(0);
    }

    [Fact]
    public void AgainstNegative_ForLong_Should_Return_Value_When_Zero()
    {
        long input = 0L;

        long result = Guard.AgainstNegative(input);

        result.Should().Be(0L);
    }

    [Fact]
    public void AgainstNegative_ForDecimal_Should_Return_Value_When_Zero()
    {
        decimal input = 0m;

        decimal result = Guard.AgainstNegative(input);

        result.Should().Be(0m);
    }

    [Fact]
    public void AgainstNegative_ForDecimal_Should_ThrowArgumentOutOfRangeException_When_Negative()
    {
        decimal input = -0.01m;

        Action action = () => Guard.AgainstNegative(input);

        action.Should()
            .Throw<ArgumentOutOfRangeException>()
            .Where(ex => ex.ParamName == "input" && ex.Message.Contains("Value cannot be negative."));
    }

    [Fact]
    public void AgainstNegative_ForInt_Should_ThrowArgumentOutOfRangeException_When_Negative()
    {
        int input = -1;

        Action action = () => Guard.AgainstNegative(input);

        action.Should()
            .Throw<ArgumentOutOfRangeException>()
            .Where(ex => ex.ParamName == "input" && ex.Message.Contains("Value cannot be negative."));
    }

    [Fact]
    public void AgainstNegative_ForLong_Should_ThrowArgumentOutOfRangeException_When_Negative()
    {
        long input = -1L;

        Action action = () => Guard.AgainstNegative(input);

        action.Should()
            .Throw<ArgumentOutOfRangeException>()
            .Where(ex => ex.ParamName == "input" && ex.Message.Contains("Value cannot be negative."));
    }

    [Fact]
    public void AgainstZeroOrNegative_ForInt_Should_Return_Value_When_GreaterThanZero()
    {
        int input = 1;

        int result = Guard.AgainstZeroOrNegative(input);

        result.Should().Be(1);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void AgainstZeroOrNegative_ForInt_Should_ThrowArgumentOutOfRangeException_When_Invalid(int input)
    {
        Action action = () => Guard.AgainstZeroOrNegative(input);

        action.Should()
            .Throw<ArgumentOutOfRangeException>()
            .Where(ex => ex.ParamName == "input" && ex.Message.Contains("Value must be greater than zero."));
    }

    [Fact]
    public void AgainstZeroOrNegative_ForLong_Should_Return_Value_When_GreaterThanZero()
    {
        long input = 1L;

        long result = Guard.AgainstZeroOrNegative(input);

        result.Should().Be(1L);
    }

    [Theory]
    [InlineData(0L)]
    [InlineData(-1L)]
    public void AgainstZeroOrNegative_ForLong_Should_ThrowArgumentOutOfRangeException_When_Invalid(long input)
    {
        Action action = () => Guard.AgainstZeroOrNegative(input);

        action.Should()
            .Throw<ArgumentOutOfRangeException>()
            .Where(ex => ex.ParamName == "input" && ex.Message.Contains("Value must be greater than zero."));
    }

    [Fact]
    public void AgainstZeroOrNegative_ForDecimal_Should_Return_Value_When_GreaterThanZero()
    {
        decimal input = 1m;

        decimal result = Guard.AgainstZeroOrNegative(input);

        result.Should().Be(1m);
    }

    [Theory]
    [InlineData("0")]
    [InlineData("-1")]
    public void AgainstZeroOrNegative_ForDecimal_Should_ThrowArgumentOutOfRangeException_When_Invalid(string inputText)
    {
        decimal input = decimal.Parse(inputText, System.Globalization.CultureInfo.InvariantCulture);

        Action action = () => Guard.AgainstZeroOrNegative(input);

        action.Should()
            .Throw<ArgumentOutOfRangeException>()
            .Where(ex => ex.ParamName == "input" && ex.Message.Contains("Value must be greater than zero."));
    }

    private static IEnumerable<int> Yield(params int[] values)
    {
        foreach (int value in values)
        {
            yield return value;
        }
    }

    private sealed class CountingReadOnlyCollection<T> : IReadOnlyCollection<T>
    {
        private readonly IReadOnlyCollection<T> _items;

        public CountingReadOnlyCollection(IReadOnlyCollection<T> items)
        {
            _items = items;
        }

        public int Count => _items.Count;

        public int GetEnumeratorCallCount
        {
            get; private set;
        }

        public IEnumerator<T> GetEnumerator()
        {
            GetEnumeratorCallCount++;
            return _items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    private sealed class CountingCollection<T> : ICollection, IEnumerable<T>
    {
        private readonly IReadOnlyCollection<T> _items;

        public CountingCollection(IReadOnlyCollection<T> items)
        {
            _items = items;
        }

        public int Count => _items.Count;

        public bool IsSynchronized => false;

        public object SyncRoot => this;

        public int GetEnumeratorCallCount
        {
            get; private set;
        }

        public void CopyTo(Array array, int index)
        {
            throw new NotSupportedException();
        }

        public IEnumerator<T> GetEnumerator()
        {
            GetEnumeratorCallCount++;
            return _items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
