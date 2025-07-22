using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HeimdallPower.Assets;

public record GridOwnerDto(
    [property: JsonPropertyName("name")] string Name, 
    [property: JsonPropertyName("facilities")] IReadOnlyCollection<FacilityDto> Facilities);