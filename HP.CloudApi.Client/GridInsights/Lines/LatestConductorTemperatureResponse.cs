using System.Text.Json.Serialization;

namespace HeimdallPower.GridInsights.Lines;

public record LatestConductorTemperatureResponse(
    [property: JsonPropertyName("metric")] string Metric, 
    [property: JsonPropertyName("unit")] string Unit, 
    [property: JsonPropertyName("conductor_temperature")] ConductorTemperatureDto ConductorTemperature);