namespace PVT15_8.Shared.DTOs;

public record WeatherResultDto
{
    public string? LocationName { get; set; }
    public List<WeatherForecastDto> Forecasts { get; set; } = new();
}

public record WeatherForecastDto
{
    public string? Time { get; set; }
    public int TemperatureC { get; set; }
    public double WindSpeedMps { get; set; }
    public int WindDirectionDeg { get; set; }
    public double PrecipMmh { get; set; }
    public string? FishingAdvice { get; set; }
    public int SymbolCode { get; set; }
}
