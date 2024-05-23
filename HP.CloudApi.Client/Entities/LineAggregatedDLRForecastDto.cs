using System;

namespace HeimdallPower.Entities
{
    public class LineAggregatedDLRForecastDto : DLRDto
    {
        public Guid SpanId { get; set; }
        public PredictionIntervalDto PredictionInterval { get; set; }
    }
}
