using System;
using Newtonsoft.Json;

namespace HeimdallPower.Entities;

public class DLRDto
{
    public string Unit {  get; set; }
    public DLR Dlr { get; set; }
}

public class DLR
{
    public DateTime Timestamp { get; set; }
    [JsonProperty("min_amapacity")]
    public double MinAmpacity { get; set; }
}
