using System;
using System.Collections.Generic;
using System.Text;

namespace HeimdallPower.Entities;

public class CurrentDto
{
    public string Unit {  get; set; }
    public Current Current { get; set; }
}

public class Current
{
    public DateTime Timestamp { get; set; }
    public double Value { get; set; }
}
