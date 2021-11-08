using System;
using System.Collections.Generic;

namespace HeimdallPower.Entities
{
    public class SpanDto<T> where T : class
    {
        public Guid Id { get; set; }
        public IEnumerable<SpanPhaseDto<T>> SpanPhases { get; set; }
    }
    public class SpanDto
    {
        public Guid Id { get; set; }
        public IEnumerable<SpanPhaseDto> SpanPhases { get; set; }
    }
}
