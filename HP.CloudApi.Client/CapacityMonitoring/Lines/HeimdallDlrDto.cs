using System;
using System.Text.Json.Serialization;

namespace HeimdallPower.CapacityMonitoring.Lines;

public record HeimdallDlrDto(
    [property: JsonPropertyName("timestamp")] DateTime Timestamp, 
    [property: JsonPropertyName("value")] double Value);