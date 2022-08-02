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
        public string CustomPhaseIdentifier { get; set; }
        public Guid Id { get; set; }
    }
}
