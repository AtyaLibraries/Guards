// <copyright file="Guard.cs" company="Atya">
// Copyright (c) Atya. All rights reserved.
// </copyright>

using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Atya.Foundation.Guards.Internal;

namespace Atya.Foundation.Guards;

/// <summary>
/// Provides lightweight guard clauses for validating arguments and protecting invariants.
/// </summary>
public static class Guard
{
    /// <summary>
    /// Ensures that a reference type argument is not null.
    /// </summary>
    /// <typeparam name="T">The argument type.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <param name="paramName">The argument name.</param>
    /// <returns>The validated value.</returns>
    [return: NotNull]
    public static T AgainstNull<T>(
        [NotNull] T? value,
        [CallerArgumentExpression(nameof(value))] string? paramName = null)
        where T : class
    {
        if (value is null)
        {
            ThrowHelper.ThrowArgumentNull(paramName);
        }

        return value;
    }

    /// <summary>
    /// Ensures that a nullable value type argument is not null.
    /// </summary>
    /// <typeparam name="T">The value type.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <param name="paramName">The argument name.</param>
    /// <returns>The validated non-null value.</returns>
    public static T AgainstNull<T>(
        T? value,
        [CallerArgumentExpression(nameof(value))] string? paramName = null)
        where T : struct
    {
        if (!value.HasValue)
        {
            ThrowHelper.ThrowArgumentNull(paramName);
        }

        return value.Value;
    }

    /// <summary>
    /// Ensures that a string argument is not null or empty.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="paramName">The argument name.</param>
    /// <returns>The validated string.</returns>
    public static string AgainstNullOrEmpty(
        string? value,
        [CallerArgumentExpression(nameof(value))] string? paramName = null)
    {
        string validatedValue = AgainstNull(value, paramName);

        if (validatedValue.Length == 0)
        {
            ThrowHelper.ThrowArgumentException("String cannot be empty.", paramName);
        }

        return validatedValue;
    }

    /// <summary>
    /// Ensures that a string argument is not null, empty, or whitespace.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="paramName">The argument name.</param>
    /// <returns>The validated string.</returns>
    public static string AgainstNullOrWhiteSpace(
        string? value,
        [CallerArgumentExpression(nameof(value))] string? paramName = null)
    {
        string validatedValue = AgainstNull(value, paramName);

        if (string.IsNullOrWhiteSpace(validatedValue))
        {
            ThrowHelper.ThrowArgumentException("String cannot be empty or whitespace.", paramName);
        }

        return validatedValue;
    }

    /// <summary>
    /// Ensures that a value type argument is not its default value.
    /// </summary>
    /// <typeparam name="T">The value type.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <param name="paramName">The argument name.</param>
    /// <returns>The validated value.</returns>
    public static T AgainstDefault<T>(
        T value,
        [CallerArgumentExpression(nameof(value))] string? paramName = null)
        where T : struct
    {
        if (EqualityComparer<T>.Default.Equals(value, default))
        {
            ThrowHelper.ThrowArgumentException("Value cannot be the default value.", paramName);
        }

        return value;
    }

    /// <summary>
    /// Ensures that a guid argument is not empty.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="paramName">The argument name.</param>
    /// <returns>The validated guid.</returns>
    public static Guid AgainstEmpty(
        Guid value,
        [CallerArgumentExpression(nameof(value))] string? paramName = null)
    {
        if (value == Guid.Empty)
        {
            ThrowHelper.ThrowArgumentException("Guid cannot be empty.", paramName);
        }

        return value;
    }

    /// <summary>
    /// Ensures that a read-only collection argument is not null or empty.
    /// </summary>
    /// <typeparam name="T">The item type.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <param name="paramName">The argument name.</param>
    /// <returns>The validated collection.</returns>
    public static IReadOnlyCollection<T> AgainstNullOrEmpty<T>(
        IReadOnlyCollection<T>? value,
        [CallerArgumentExpression(nameof(value))] string? paramName = null)
    {
        IReadOnlyCollection<T> validatedValue = AgainstNull(value, paramName);

        if (validatedValue.Count == 0)
        {
            ThrowHelper.ThrowArgumentException("Collection cannot be empty.", paramName);
        }

        return validatedValue;
    }

    /// <summary>
    /// Ensures that an enumerable argument is not null or empty.
    /// </summary>
    /// <typeparam name="T">The item type.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <param name="paramName">The argument name.</param>
    /// <returns>The validated enumerable.</returns>
    public static IEnumerable<T> AgainstNullOrEmpty<T>(
        IEnumerable<T>? value,
        [CallerArgumentExpression(nameof(value))] string? paramName = null)
    {
        IEnumerable<T> validatedValue = AgainstNull(value, paramName);

        if (validatedValue is IReadOnlyCollection<T> readOnlyCollection)
        {
            if (readOnlyCollection.Count == 0)
            {
                ThrowHelper.ThrowArgumentException("Collection cannot be empty.", paramName);
            }

            return validatedValue;
        }

        if (validatedValue is ICollection collection)
        {
            if (collection.Count == 0)
            {
                ThrowHelper.ThrowArgumentException("Collection cannot be empty.", paramName);
            }

            return validatedValue;
        }

        using IEnumerator<T> enumerator = validatedValue.GetEnumerator();
        if (!enumerator.MoveNext())
        {
            ThrowHelper.ThrowArgumentException("Collection cannot be empty.", paramName);
        }

        return validatedValue;
    }

    /// <summary>
    /// Ensures that an integer value is within the specified inclusive range.
    /// </summary>
    public static int AgainstOutOfRange(
        int value,
        int minimum,
        int maximum,
        [CallerArgumentExpression(nameof(value))] string? paramName = null)
    {
        if (minimum > maximum)
        {
            ThrowHelper.ThrowArgumentException("Minimum cannot be greater than maximum.", nameof(minimum));
        }

        if (value < minimum || value > maximum)
        {
            ThrowHelper.ThrowArgumentOutOfRange(paramName, value, minimum, maximum);
        }

        return value;
    }

    /// <summary>
    /// Ensures that a long value is within the specified inclusive range.
    /// </summary>
    public static long AgainstOutOfRange(
        long value,
        long minimum,
        long maximum,
        [CallerArgumentExpression(nameof(value))] string? paramName = null)
    {
        if (minimum > maximum)
        {
            ThrowHelper.ThrowArgumentException("Minimum cannot be greater than maximum.", nameof(minimum));
        }

        if (value < minimum || value > maximum)
        {
            ThrowHelper.ThrowArgumentOutOfRange(paramName, value, minimum, maximum);
        }

        return value;
    }

    /// <summary>
    /// Ensures that a decimal value is within the specified inclusive range.
    /// </summary>
    public static decimal AgainstOutOfRange(
        decimal value,
        decimal minimum,
        decimal maximum,
        [CallerArgumentExpression(nameof(value))] string? paramName = null)
    {
        if (minimum > maximum)
        {
            ThrowHelper.ThrowArgumentException("Minimum cannot be greater than maximum.", nameof(minimum));
        }

        if (value < minimum || value > maximum)
        {
            ThrowHelper.ThrowArgumentOutOfRange(paramName, value, minimum, maximum);
        }

        return value;
    }

    /// <summary>
    /// Ensures that an integer value is not negative.
    /// </summary>
    public static int AgainstNegative(
        int value,
        [CallerArgumentExpression(nameof(value))] string? paramName = null)
    {
        if (value < 0)
        {
            ThrowHelper.ThrowArgumentOutOfRange(paramName, value, "Value cannot be negative.");
        }

        return value;
    }

    /// <summary>
    /// Ensures that a long value is not negative.
    /// </summary>
    public static long AgainstNegative(
        long value,
        [CallerArgumentExpression(nameof(value))] string? paramName = null)
    {
        if (value < 0)
        {
            ThrowHelper.ThrowArgumentOutOfRange(paramName, value, "Value cannot be negative.");
        }

        return value;
    }

    /// <summary>
    /// Ensures that a decimal value is not negative.
    /// </summary>
    public static decimal AgainstNegative(
        decimal value,
        [CallerArgumentExpression(nameof(value))] string? paramName = null)
    {
        if (value < 0)
        {
            ThrowHelper.ThrowArgumentOutOfRange(paramName, value, "Value cannot be negative.");
        }

        return value;
    }

    /// <summary>
    /// Ensures that an integer value is greater than zero.
    /// </summary>
    public static int AgainstZeroOrNegative(
        int value,
        [CallerArgumentExpression(nameof(value))] string? paramName = null)
    {
        if (value <= 0)
        {
            ThrowHelper.ThrowArgumentOutOfRange(paramName, value, "Value must be greater than zero.");
        }

        return value;
    }

    /// <summary>
    /// Ensures that a long value is greater than zero.
    /// </summary>
    public static long AgainstZeroOrNegative(
        long value,
        [CallerArgumentExpression(nameof(value))] string? paramName = null)
    {
        if (value <= 0)
        {
            ThrowHelper.ThrowArgumentOutOfRange(paramName, value, "Value must be greater than zero.");
        }

        return value;
    }

    /// <summary>
    /// Ensures that a decimal value is greater than zero.
    /// </summary>
    public static decimal AgainstZeroOrNegative(
        decimal value,
        [CallerArgumentExpression(nameof(value))] string? paramName = null)
    {
        if (value <= 0)
        {
            ThrowHelper.ThrowArgumentOutOfRange(paramName, value, "Value must be greater than zero.");
        }

        return value;
    }
}
