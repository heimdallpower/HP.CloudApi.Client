using System;

namespace HeimdallPower.Entities;

public sealed class FacilityDto
{
    public Guid Id { get; set; }
    public string Name { get; }
    public LineDto Line { get; }

    public FacilityDto(string name, LineDto line)
    {
        Name = name;
        Line = line;  
    }
}
