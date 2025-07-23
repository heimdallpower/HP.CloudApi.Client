using System.Collections.Generic;

namespace HeimdallPower.Assets;

public class AssetsResponse
{
    /// <summary>
    /// List of grid owners the API consumer has access to.
    /// </summary>
    public IReadOnlyCollection<GridOwnerDto> GridOwners { get; init; }
}