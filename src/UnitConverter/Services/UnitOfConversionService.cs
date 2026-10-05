using UnitConverter.Models;

namespace UnitConverter.Services;

public class UnitOfConversionService : IConversionService
{
    public decimal Convert(decimal value, string conversionType)
    {
        double input = (double)value;
        double result;

        if (conversionType == ConversionTypes.MilesToKilometers)
        {
            result = new UnitOf.Length()
                .FromMiles(input)
                .ToKilometers();
        }
        else if (conversionType == ConversionTypes.KilometersToMiles)
        {
            result = new UnitOf.Length()
                .FromKilometers(input)
                .ToMiles();
        }
        else if (conversionType == ConversionTypes.FahrenheitToCelsius)
        {
            result = new UnitOf.Temperature()
                .FromFahrenheit(input)
                .ToCelsius();
        }
        else if (conversionType == ConversionTypes.CelsiusToFahrenheit)
        {
            result = new UnitOf.Temperature()
                .FromCelsius(input)
                .ToFahrenheit();
        }
        else if (conversionType == ConversionTypes.PoundsToKilograms)
        {
            result = new UnitOf.Mass()
                .FromPounds(input)
                .ToKilograms();
        }
        else if (conversionType == ConversionTypes.KilogramsToPounds)
        {
            result = new UnitOf.Mass()
                .FromKilograms(input)
                .ToPounds();
        }
        else if (conversionType == ConversionTypes.InchesToCentimeters)
        {
            result = new UnitOf.Length()
                .FromInches(input)
                .ToCentimeters();
        }
        else if (conversionType == ConversionTypes.CentimetersToInches)
        {
            result = new UnitOf.Length()
                .FromCentimeters(input)
                .ToInches();
        }
        else
        {
            throw new ArgumentException("Unknown conversion type.");
        }

        return (decimal)result;
    }
}
