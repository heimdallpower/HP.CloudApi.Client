using System.Text.Json.Serialization;

namespace HeimdallPower.CapacityMonitoring.Lines;

public record LatestHeimdallDlrResponse(
    [property: JsonPropertyName("metric")] string Metric, 
    [property: JsonPropertyName("unit")] string Unit, 
    [property: JsonPropertyName("heimdall_dlr")] HeimdallDlrDto HeimdallDlr);