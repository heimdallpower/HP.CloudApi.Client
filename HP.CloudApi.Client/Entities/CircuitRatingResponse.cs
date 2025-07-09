using System;
using Newtonsoft.Json;

namespace HeimdallPower.Entities;

public record CircuitRatingResponse(
    string Metric, 
    string Unit, 
    [JsonProperty("circuit_rating")]CircuitRatingDto CircuitRating);