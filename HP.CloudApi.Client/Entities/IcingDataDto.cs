using System;

namespace HeimdallPower.Entities
{
    public class IcingDataDto
    {
        public DateTime? CalculatedTime { get; set; }
        public float IceWeightPerMeter { get; set; }
        public float Tension { get; set; }
        public float IceThickness { get; set; }
    }
}
