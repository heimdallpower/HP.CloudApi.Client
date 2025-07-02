using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace HeimdallPower.Entities;
    public class SpanDto
    {
        public Guid Id { get; set; }
        [JsonProperty("mast_name_a")]
        public string MastName_A { get; set; }
        [JsonProperty("mast_name_b")]
        public string MastName_B { get; set; }
        [JsonProperty("span_phases")]
        public IEnumerable<SpanPhaseDto> SpanPhases { get; set; }
    }
