namespace HeimdallPower.Entities;

public record FacilityDto
{
    public string Name { get; set; }
    public LineDto Line { get; set; }
}
