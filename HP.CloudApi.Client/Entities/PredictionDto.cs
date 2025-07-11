using System;

namespace HeimdallPower.Entities;

public sealed class PredictionDto
{
    public double Value { get; }
    public Guid SpanId { get; }

    public PredictionDto(double value, Guid spanId)
    {
        Value = value;
        SpanId = spanId;
    }
}