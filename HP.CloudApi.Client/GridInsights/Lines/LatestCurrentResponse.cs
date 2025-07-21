using System.Text.Json.Serialization;

namespace HeimdallPower.GridInsights.Lines;

public record LatestCurrentResponse(
    [property: JsonPropertyName("metric")] string Metric, 
    [property: JsonPropertyName("unit")] string Unit, 
    [property: JsonPropertyName("current")] CurrentDto Current);