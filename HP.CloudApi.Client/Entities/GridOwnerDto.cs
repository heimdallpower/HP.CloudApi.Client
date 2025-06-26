using System.Collections.Generic;
using Newtonsoft.Json;

namespace HeimdallPower.Entities;

public class GridOwnerDto
{
    public string Name { get; set; }
    public List<FacilitiesDto> Facilities { get; set; }
}

public class AssetsResponseObject
{
    public DataResponseObject Data { get; set; }
}

public class DataResponseObject
{
    [JsonProperty("grid_owners")]
    public List<GridOwnerDto> GridOwners { get; set; }
}