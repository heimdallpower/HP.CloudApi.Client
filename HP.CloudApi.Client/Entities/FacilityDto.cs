using System;

namespace HeimdallPower.Entities;

public sealed class FacilityDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public LineDto Line { get; set; }

    public FacilityDto(string name, LineDto line)
    {
        Name = name;
        Line = line;  
    }
}
