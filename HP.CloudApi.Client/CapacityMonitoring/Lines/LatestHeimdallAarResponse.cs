using System.Text.Json.Serialization;

namespace HeimdallPower.CapacityMonitoring.Lines;

public record LatestHeimdallAarResponse(
    [property: JsonPropertyName("metric")] string Metric, 
    [property: JsonPropertyName("unit")] string Unit, 
    [property: JsonPropertyName("heimdall_aar")] HeimdallAarDto HeimdallAar);