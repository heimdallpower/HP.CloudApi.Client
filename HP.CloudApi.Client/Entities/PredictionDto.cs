using System;
using Newtonsoft.Json;

namespace HeimdallPower.Entities;
public sealed class PredictionDto
{
    public double Value { get; }
    [JsonProperty("at_span_id")]
    public Guid SpanId { get; }

    public PredictionDto(double value, Guid spanId)
    {
        Value = value;
        SpanId = spanId;
    }
}