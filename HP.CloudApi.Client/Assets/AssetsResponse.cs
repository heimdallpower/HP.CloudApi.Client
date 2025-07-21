using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HeimdallPower.Assets;

public record AssetsResponse([property: JsonPropertyName("grid_owners")] IReadOnlyCollection<GridOwnerDto> GridOwners);