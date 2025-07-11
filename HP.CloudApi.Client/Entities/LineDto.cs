using System;
using System.Collections.Generic;

namespace HeimdallPower.Entities;

public class LineDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int AvailableForecastHours;
    public string GridOwnerName {get; set;}
    public List<SpanDto> Spans;
}
