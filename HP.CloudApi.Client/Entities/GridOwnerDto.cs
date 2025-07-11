using System.Collections.Generic;

namespace HeimdallPower.Entities;

public sealed class GridOwnerDto
{
    public string Name { get; }
    public List<FacilityDto> Facilities { get; }

    public GridOwnerDto(string name, List<FacilityDto> facilities)
    {
        Name = name;
        Facilities = facilities;
    }
}

public record AssetsResponseObject
{
    public DataResponseObject Data { get; }

    public AssetsResponseObject(DataResponseObject data)
    {
        Data = data;
    }
}

public record DataResponseObject
{
    public List<GridOwnerDto> GridOwners { get; }

    public DataResponseObject(List<GridOwnerDto> gridOwners)
    {
        GridOwners = gridOwners;
    }
}