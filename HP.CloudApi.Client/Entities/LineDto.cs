using System;
using System.Collections.Generic;
namespace HeimdallPower.Entities
{
    public class LineDto
    {
        public Guid Id { get; set; }
        public string Name;
        public int AvailableForecastHours;
        public List<SpanDto> Spans;
    }
}
