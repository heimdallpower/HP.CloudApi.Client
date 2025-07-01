using System;
using Newtonsoft.Json;

namespace HeimdallPower.Entities
{
    public class PredictionDto
    {
        public double Value { get; set; }
        [JsonProperty("at_span_id")]
        public Guid SpanId { get; set; }
    }
}