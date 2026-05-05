using System;
using CourierHub.Abstractions.Enums;

namespace CourierHub.Core.UnitConverters;

/// <summary>
/// Provides static methods for converting between different weight units.
/// </summary>
public static class WeightConverter
{
    /// <summary>
    /// Converts a weight value from one unit to another.
    /// </summary>
    /// <param name="value">The weight value to convert.</param>
    /// <param name="from">The source unit.</param>
    /// <param name="to">The target unit.</param>
    /// <returns>The converted weight value.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when an unsupported unit is provided.</exception>
    public static decimal Convert(decimal value, WeightUnit from, WeightUnit to)
    {
        if (from == to)
            return value;

        // Convert to base unit (grams)
        var grams = ConvertToGrams(value, from);

        // Convert from grams to target unit
        return ConvertFromGrams(grams, to);
    }

    /// <summary>
    /// Converts a weight value to grams.
    /// </summary>
    private static decimal ConvertToGrams(decimal value, WeightUnit unit) =>
        unit switch
        {
            WeightUnit.G => value,
            WeightUnit.Kg => value * 1000,
            WeightUnit.Lb => value * 453.592m,
            WeightUnit.Oz => value * 28.3495m,
            _ => throw new ArgumentOutOfRangeException(nameof(unit), $"Unsupported weight unit: {unit}")
        };

    /// <summary>
    /// Converts from grams to the target unit.
    /// </summary>
    private static decimal ConvertFromGrams(decimal grams, WeightUnit unit) =>
        unit switch
        {
            WeightUnit.G => grams,
            WeightUnit.Kg => grams / 1000,
            WeightUnit.Lb => grams / 453.592m,
            WeightUnit.Oz => grams / 28.3495m,
            _ => throw new ArgumentOutOfRangeException(nameof(unit), $"Unsupported weight unit: {unit}")
        };
}
