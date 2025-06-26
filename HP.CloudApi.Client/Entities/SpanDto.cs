using System;
using System.Collections.Generic;

namespace HeimdallPower.Entities
{
    public class SpanDto
    {
        public Guid Id { get; set; }
        public string MastName_A { get; set; }
        public string MastName_B { get; set; }
        public IEnumerable<SpanPhaseDto> SpanPhases { get; set; }
    }
}
