using Microsoft.AspNetCore.Mvc;

namespace WebApiRequestIdMiddleware.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController(ILogger<WeatherForecastController> logger) : ControllerBase
{
    private static readonly string[] Summaries =
    [
        "Freezing",
        "Bracing",
        "Chilly",
        "Cool",
        "Mild",
        "Warm",
        "Balmy",
        "Hot",
        "Sweltering",
        "Scorching"
    ];

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        var forecast = Enumerable
            .Range(1, 5)
            .Select(
                index =>
                    new WeatherForecast
                    {
                        Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                        TemperatureC = Random.Shared.Next(-20, 55),
                        Summary = Summaries[Random.Shared.Next(Summaries.Length)]
                    }
            )
            .ToArray();

        logger.LogInformation(
            "Date: {Date}, Temperature: {Temperature}",
            forecast[0].Date,
            forecast[0].TemperatureC
        );

        return forecast;
    }

    [HttpGet("throw")]
    public IActionResult Throw()
    {
        throw new Exception("This is an exception");
    }
}
