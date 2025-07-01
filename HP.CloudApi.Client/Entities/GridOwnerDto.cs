using System.Collections.Generic;
using Newtonsoft.Json;

namespace HeimdallPower.Entities;

public record GridOwnerDto
{
    public string Name { get; set; }
    public List<FacilityDto> Facilities { get; set; }
}

public record AssetsResponseObject
{
    public DataResponseObject Data { get; set; }
}

public record DataResponseObject
{
    [JsonProperty("grid_owners")]
    public List<GridOwnerDto> GridOwners { get; set; }
}