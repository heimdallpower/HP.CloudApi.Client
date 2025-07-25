﻿using System;
using System.Collections.Generic;

namespace HeimdallPower.Assets;

public record SpanDto
{
    /// <summary>
    /// Unique identifier of the span.
    /// </summary>
    /// <example>00000000-0000-0000-0000-000000000000</example>
    public Guid Id { get; init; }

    /// <summary>
    /// Name of the first mast in the span.
    /// </summary>
    /// <example>Mast A</example>
    public string? MastNameA { get; init; }

    /// <summary>
    /// Name of the second mast in the span
    /// </summary>
    /// <example>Mast B</example>
    public string? MastNameB { get; init; }

    /// <summary>
    /// List of span phases associated with the span.
    /// </summary>
    public IReadOnlyCollection<SpanPhaseDto> SpanPhases { get; init; }
}