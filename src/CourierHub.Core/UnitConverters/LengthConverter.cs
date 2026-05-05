using System;
using CourierHub.Abstractions.Enums;

namespace CourierHub.Core.UnitConverters;

/// <summary>
/// Provides static methods for converting between different length units.
/// </summary>
public static class LengthConverter
{
    /// <summary>
    /// Converts a length value from one unit to another.
    /// </summary>
    /// <param name="value">The length value to convert.</param>
    /// <param name="from">The source unit.</param>
    /// <param name="to">The target unit.</param>
    /// <returns>The converted length value.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when an unsupported unit is provided.</exception>
    public static decimal Convert(decimal value, LengthUnit from, LengthUnit to)
    {
        if (from == to)
            return value;

        // Convert to base unit (millimeters)
        var millimeters = ConvertToMillimeters(value, from);

        // Convert from millimeters to target unit
        return ConvertFromMillimeters(millimeters, to);
    }

    /// <summary>
    /// Converts a length value to millimeters.
    /// </summary>
    private static decimal ConvertToMillimeters(decimal value, LengthUnit unit) =>
        unit switch
        {
            LengthUnit.Mm => value,
            LengthUnit.Cm => value * 10,
            LengthUnit.M => value * 1000,
            LengthUnit.In => value * 25.4m,
            LengthUnit.Ft => value * 304.8m,
            _ => throw new ArgumentOutOfRangeException(nameof(unit), $"Unsupported length unit: {unit}")
        };

    /// <summary>
    /// Converts from millimeters to the target unit.
    /// </summary>
    private static decimal ConvertFromMillimeters(decimal millimeters, LengthUnit unit) =>
        unit switch
        {
            LengthUnit.Mm => millimeters,
            LengthUnit.Cm => millimeters / 10,
            LengthUnit.M => millimeters / 1000,
            LengthUnit.In => millimeters / 25.4m,
            LengthUnit.Ft => millimeters / 304.8m,
            _ => throw new ArgumentOutOfRangeException(nameof(unit), $"Unsupported length unit: {unit}")
        };
}
