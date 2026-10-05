using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using UnitConverter.Models;
using UnitConverter.Services;

namespace UnitConverter.Pages;

public class QuickConversionsModel : PageModel
{
    private readonly IConversionService _conversionService;

    public string Output { get; set; } = string.Empty;

    public string ErrorMessage { get; set; } = string.Empty;

    public QuickConversionsModel(IConversionService conversionService)
    {
        _conversionService = conversionService;
    }

    public IEnumerable<SelectListItem> PoundOptions =>
    [
        new SelectListItem("1 pound", "1"),
        new SelectListItem("5 pounds", "5"),
        new SelectListItem("10 pounds", "10"),
        new SelectListItem("25 pounds", "25"),
        new SelectListItem("50 pounds", "50")
    ];

    public IActionResult OnGetMilesToKilometers(string input)
    {
        return PerformConversion(input, ConversionTypes.MilesToKilometers);
    }

    public IActionResult OnGetKilometersToMiles(string input)
    {
        return PerformConversion(input, ConversionTypes.KilometersToMiles);
    }

    public IActionResult OnGetFahrenheitToCelsius(string input)
    {
        return PerformConversion(input, ConversionTypes.FahrenheitToCelsius);
    }

    public IActionResult OnGetCelsiusToFahrenheit(string input)
    {
        return PerformConversion(input, ConversionTypes.CelsiusToFahrenheit);
    }

    public IActionResult OnGetPoundsToKilograms(string input)
    {
        return PerformConversion(input, ConversionTypes.PoundsToKilograms);
    }

    public IActionResult OnGetKilogramsToPounds(string input)
    {
        return PerformConversion(input, ConversionTypes.KilogramsToPounds);
    }

    public IActionResult OnGetInchesToCentimeters(string input)
    {
        return PerformConversion(input, ConversionTypes.InchesToCentimeters);
    }

    public IActionResult OnGetCentimetersToInches(string input)
    {
        return PerformConversion(input, ConversionTypes.CentimetersToInches);
    }

    private IActionResult PerformConversion(string input, string conversionType)
    {
        decimal value;

        if (!decimal.TryParse(input, out value))
        {
            ErrorMessage = "Input must be a valid number.";
            return Page();
        }

        try
        {
            decimal result = _conversionService.Convert(value, conversionType);
            Output = result.ToString();
        }
        catch (ArgumentException)
        {
            ErrorMessage = "Unknown conversion type.";
        }

        return Page();
    }
}
