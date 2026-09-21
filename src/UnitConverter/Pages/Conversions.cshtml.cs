using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace UnitConverter.Pages;

public class ConversionsModel : PageModel
{

    [BindProperty(SupportsGet = true)]
    public ConversionModel Conversion { get; set; } = new();

    [BindProperty(SupportsGet = true)]
    public string ConversionType { get; set; } = string.Empty;

    [BindProperty(SupportsGet = true)]
    public string Input { get; set; } = string.Empty;

    public string Output { get; set; } = string.Empty;

    public void OnGet()
    {

        if (!string.IsNullOrEmpty(ConversionType))
        {
            Conversion.ConversionType = ConversionType;
        }

        if (!string.IsNullOrEmpty(Input))
        {
            Conversion.Input = Input;
        }

        if (string.IsNullOrEmpty(Conversion.ConversionType))
        {
            Conversion.ConversionType = ConversionTypes.MilesToKilometers;
        }

        if (string.IsNullOrEmpty(Conversion.Input))
        {
            Conversion.Input = "3.1415";
        }

        ConversionType = Conversion.ConversionType;
        Input = Conversion.Input;

        if (ConversionTypes.All.TryGetValue(
                Conversion.ConversionType,
                out string? displayName))
        {
            ViewData["ConversionType"] = displayName;
        }
        else
        {
            ViewData["ErrorMessage"] = "Unknown conversion type.";
            return;
        }

        ViewData["Title"] = "Conversions";

        double inputValue;

        try
        {
            inputValue = Convert.ToDouble(Conversion.Input);
        }
        catch (FormatException)
        {
            ViewData["ErrorMessage"] = "Input must be a valid number.";
            return;
        }
        catch (OverflowException)
        {
            ViewData["ErrorMessage"] = "Input must be a valid number.";
            return;
        }

        double convertedValue;

        try
        {
            convertedValue = Conversion.ConversionType switch
            {
                ConversionTypes.MilesToKilometers =>
                    new UnitOf.Length()
                        .FromMiles(inputValue)
                        .ToKilometers(),

                ConversionTypes.KilometersToMiles =>
                    new UnitOf.Length()
                        .FromKilometers(inputValue)
                        .ToMiles(),

                ConversionTypes.FahrenheitToCelsius =>
                    new UnitOf.Temperature()
                        .FromFahrenheit(inputValue)
                        .ToCelsius(),

                ConversionTypes.CelsiusToFahrenheit =>
                    new UnitOf.Temperature()
                        .FromCelsius(inputValue)
                        .ToFahrenheit(),

                ConversionTypes.PoundsToKilograms =>
                    new UnitOf.Mass()
                        .FromPounds(inputValue)
                        .ToKilograms(),

                ConversionTypes.KilogramsToPounds =>
                    new UnitOf.Mass()
                        .FromKilograms(inputValue)
                        .ToPounds(),

                ConversionTypes.InchesToCentimeters =>
                    new UnitOf.Length()
                        .FromInches(inputValue)
                        .ToCentimeters(),

                ConversionTypes.CentimetersToInches =>
                    new UnitOf.Length()
                        .FromCentimeters(inputValue)
                        .ToInches(),

                _ => double.NaN
            };
        }
        catch (Exception)
        {
            ViewData["ErrorMessage"] = "Unable to perform conversion.";
            return;
        }

        if (double.IsNaN(convertedValue))
        {
            ViewData["ErrorMessage"] = "Unknown conversion type.";
            return;
        }

        Conversion.Output = convertedValue.ToString();

        Output = Conversion.Output;
    }
}
