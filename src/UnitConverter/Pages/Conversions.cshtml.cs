using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace UnitConverter.Pages;

public class ConversionsModel : PageModel
{
    [BindProperty(SupportsGet = true)]
    public string ConversionType { get; set; } = string.Empty;

    [BindProperty(SupportsGet = true)]
    public string Input { get; set; } = string.Empty;

    public string Output { get; set; } = string.Empty;

    public void OnGet()
    {
        double inputValue;

        try
        {
            inputValue = Convert.ToDouble(Input);
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

        ViewData["Title"] = "Conversions";

        double convertedValue;

        try
        {
            convertedValue = ConversionType switch
            {
                "MilesToKilometers" => new UnitOf.Length().FromMiles(inputValue).ToKilometers(),
                "KilometersToMiles" => new UnitOf.Length().FromKilometers(inputValue).ToMiles(),
                "FahrenheitToCelsius" => new UnitOf.Temperature().FromFahrenheit(inputValue).ToCelsius(),
                "CelsiusToFahrenheit" => new UnitOf.Temperature().FromCelsius(inputValue).ToFahrenheit(),
                "PoundsToKilograms" => new UnitOf.Mass().FromPounds(inputValue).ToKilograms(),
                "KilogramsToPounds" => new UnitOf.Mass().FromKilograms(inputValue).ToPounds(),
                "InchesToCentimeters" => new UnitOf.Length().FromInches(inputValue).ToCentimeters(),
                "CentimetersToInches" => new UnitOf.Length().FromCentimeters(inputValue).ToInches(),

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

        Output = convertedValue.ToString();
    }
}
