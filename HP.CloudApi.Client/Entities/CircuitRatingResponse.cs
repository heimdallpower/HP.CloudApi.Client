namespace HeimdallPower.Entities;

public record CircuitRatingResponse(
    string Metric, 
    string Unit, 
    CircuitRatingDto CircuitRating);