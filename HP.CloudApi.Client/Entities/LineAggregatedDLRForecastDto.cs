using System;
using System.Collections.Generic;

namespace HeimdallPower.Entities
{
    public class LineAggregatedDLRForecastDto : DLRDto
    {
        public Guid SpanId { get; set; }
        public PredictionIntervalDto PredictionInterval { get; set; }
    }
}
