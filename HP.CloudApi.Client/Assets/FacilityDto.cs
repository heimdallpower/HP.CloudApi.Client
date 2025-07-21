using System;
using System.Text.Json.Serialization;

namespace HeimdallPower.Assets;

public record FacilityDto(
    [property: JsonPropertyName("id")] Guid Id, 
    [property: JsonPropertyName("name")] string Name, 
    [property: JsonPropertyName("line")] LineDto Line);