using System;
using System.Collections.Generic;

namespace HeimdallPower.Entities
{
    public class SpanPhaseDto<T> where T : class
    {
        public Guid Id { get; set; }
        public IEnumerable<T> Data { get; set; }
    }
    public class SpanPhaseDto
    {
        public Guid Id { get; set; }
    }
}
