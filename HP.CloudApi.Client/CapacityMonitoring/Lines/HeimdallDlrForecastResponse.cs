using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HeimdallPower.CapacityMonitoring.Lines;

public record HeimdallDlrForecastResponse(
    [property: JsonPropertyName("metric")] string Metric, 
    [property: JsonPropertyName("unit")] string Unit, 
    [property: JsonPropertyName("updated_timestamp")] DateTime UpdatedTimestamp,
    [property: JsonPropertyName("heimdall_dlr_forecasts")] IReadOnlyCollection<ForecastDto> HeimdallDlrForecasts);