using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HeimdallPower.Assets;

public record SpanDto(
    [property: JsonPropertyName("id")] Guid Id, 
    [property: JsonPropertyName("mast_name_a")] string MastNameA,
    [property: JsonPropertyName("mast_name_b")] string MastNameB, 
    [property: JsonPropertyName("span_phases")] IReadOnlyCollection<SpanPhaseDto> SpanPhases);