using System.Text.Json.Serialization;

namespace HeimdallPower.CapacityMonitoring.Facilities;

public record CircuitRatingResponse(
    [property: JsonPropertyName("metric")] string Metric, 
    [property: JsonPropertyName("unit")] string Unit, 
    [property: JsonPropertyName("circuit_rating")] CircuitRatingDto CircuitRating);