using System;
using System.Text.Json.Serialization;

namespace HeimdallPower.Assets;

public record SpanPhaseDto(
    [property: JsonPropertyName("name")] string Name, 
    [property: JsonPropertyName("id")] Guid Id);
