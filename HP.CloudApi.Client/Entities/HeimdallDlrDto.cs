using System;

namespace HeimdallPower.Entities;

public class HeimdallDlrDto
{
    public HeimdallDlr Dlr { get; set; }
}

public class HeimdallDlr
{
    public DateTime Timestamp { get; set; }
    public double Value { get; set; }
}