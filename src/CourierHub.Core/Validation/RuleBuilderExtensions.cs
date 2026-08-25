using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace CourierHub.Core.Validation;

/// <summary>
/// Rule builder extensions for common validation scenarios, providing a fluent interface for defining validation rules.
/// </summary>
internal static class RuleBuilderExtensions
{
    /// <summary>
    /// Ensures a value is not empty. 
    /// </summary>
    /// <typeparam name="T">The validated type.</typeparam>
    /// <param name="rule">The rule builder.</param>
    /// <returns>The updated rule builder.</returns>
    public static IRuleBuilder<T, string> NotEmpty<T>(this IRuleBuilder<T, string> rule) =>
        rule.Must(v => v.Length > 0, "must not be empty.");

    /// <summary>
    /// Ensures a value empty if not null. 
    /// </summary>
    /// <typeparam name="T">The validated type.</typeparam>
    /// <param name="rule">The rule builder.</param>
    /// <returns>The updated rule builder.</returns>
    public static IRuleBuilder<T, string?> NotEmptyNullable<T>(this IRuleBuilder<T, string?> rule) =>
        rule.Must(v => v?.Length > 0, "must not be empty.");

    /// <summary>
    /// Ensures a value is present.
    /// </summary>
    /// <typeparam name="T">The validated type.</typeparam>
    /// <typeparam name="TProperty">The validated property type.</typeparam>
    /// <param name="rule">The rule builder.</param>
    /// <returns>The updated rule builder.</returns>
    public static IRuleBuilder<T, TProperty> Required<T, TProperty>(this IRuleBuilder<T, TProperty> rule) =>
        rule.Must(v => v is not null, "must not be null.");

    /// <summary>
    /// Ensures a value satisfies the specified predicate.
    /// </summary>
    /// <typeparam name="T">The validated type.</typeparam>
    /// <typeparam name="TProperty">The validated property type.</typeparam>
    /// <param name="rule">The rule builder.</param>
    /// <param name="predicate">The validation predicate.</param>
    /// <returns>The updated rule builder.</returns>
    public static IRuleBuilder<T, TProperty> Must<T, TProperty>(
        this IRuleBuilder<T, TProperty> rule, Func<TProperty, bool> predicate) =>
        rule.Must(predicate, "is not valid.");

    /// <summary>
    /// Ensures a value is greater than the specified threshold.
    /// </summary>
    /// <typeparam name="T">The validated type.</typeparam>
    /// <typeparam name="TProperty">The validated property type.</typeparam>
    /// <param name="rule">The rule builder.</param>
    /// <param name="threshold">The lower bound.</param>
    /// <returns>The updated rule builder.</returns>
    public static IRuleBuilder<T, TProperty> GreaterThan<T, TProperty>(
        this IRuleBuilder<T, TProperty> rule, TProperty threshold)
        where TProperty : IComparable<TProperty> =>
        rule.Must(v => v.CompareTo(threshold) > 0, $"must be greater than {threshold}.");

    /// <summary>
    /// Ensures a value is less than the specified threshold.
    /// </summary>
    /// <typeparam name="T">The validated type.</typeparam>
    /// <typeparam name="TProperty">The validated property type.</typeparam>
    /// <param name="rule">The rule builder.</param>
    /// <param name="threshold">The upper bound.</param>
    /// <returns>The updated rule builder.</returns>
    public static IRuleBuilder<T, TProperty> LessThan<T, TProperty>(
        this IRuleBuilder<T, TProperty> rule, TProperty threshold)
        where TProperty : IComparable<TProperty> =>
        rule.Must(v => v.CompareTo(threshold) < 0, $"must be less than {threshold}.");

    /// <summary>
    /// Ensures a string value length is within the specified range.
    /// </summary>
    /// <typeparam name="T">The validated type.</typeparam>
    /// <param name="rule">The rule builder.</param>
    /// <param name="min">The minimum length.</param>
    /// <param name="max">The maximum length.</param>
    /// <returns>The updated rule builder.</returns>
    public static IRuleBuilder<T, string> Length<T>(this IRuleBuilder<T, string> rule, int min, int max) =>
        rule.Must(v => v.Length >= min && v.Length <= max,
            $"must be between {min} and {max} characters.");

    /// <summary>
    /// Ensures a string value length is within the specified range if not null.
    /// </summary>
    /// <typeparam name="T">The validated type.</typeparam>
    /// <param name="rule">The rule builder.</param>
    /// <param name="min">The minimum length.</param>
    /// <param name="max">The maximum length.</param>
    /// <returns>The updated rule builder.</returns>
    public static IRuleBuilder<T, string?> LengthNullable<T>(this IRuleBuilder<T, string?> rule, int min, int max) =>
        rule.Must(v => v?.Length >= min && v.Length <= max,
            $"must be between {min} and {max} characters.");

    /// <summary>
    /// Ensures a string value matches the specified regular expression pattern.
    /// </summary>
    /// <typeparam name="T">The validated type.</typeparam>
    /// <param name="rule">The rule builder.</param>
    /// <param name="pattern">The regular expression pattern.</param>
    /// <returns>The updated rule builder.</returns>
    public static IRuleBuilder<T, string> Matches<T>(this IRuleBuilder<T, string> rule, string pattern) =>
        rule.Must(v => Regex.IsMatch(v, pattern), "is not in the correct format.");

    /// <summary>
    /// Ensures a value falls within the specified inclusive range.
    /// </summary>
    /// <typeparam name="T">The validated type.</typeparam>
    /// <typeparam name="TProperty">The validated property type.</typeparam>
    /// <param name="rule">The rule builder.</param>
    /// <param name="min">The minimum allowed value (inclusive).</param>
    /// <param name="max">The maximum allowed value (inclusive).</param>
    /// <returns>The updated rule builder.</returns>
    public static IRuleBuilder<T, TProperty> HasRange<T, TProperty>(
        this IRuleBuilder<T, TProperty> rule, TProperty min, TProperty max)
        where TProperty : IComparable<TProperty> =>
        rule.Must(v => v.CompareTo(min) >= 0 && v.CompareTo(max) <= 0,
            $"must be between {min} and {max}.");
}