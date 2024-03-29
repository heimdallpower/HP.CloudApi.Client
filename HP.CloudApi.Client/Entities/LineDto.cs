﻿using System;
using System.Collections.Generic;
namespace HeimdallPower.Entities
{
    public class LineDto<T> where T : class
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Owner { get; set; }
        public IEnumerable<SpanDto<T>> Spans { get; set; }
    }
    public class LineDto
    {
        public Guid Id { get; set; }
        public string Name;
        public string Owner;
        public List<SpanDto> Spans;
    }
}
