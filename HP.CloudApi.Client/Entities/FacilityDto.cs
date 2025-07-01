namespace HeimdallPower.Entities;

public sealed class FacilityDto
{
    public string Name { get; }
    public LineDto Line { get; }

    public FacilityDto(string name, LineDto line)
    {
        Name = name;
        Line = line;  
    }
}
