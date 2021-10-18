using System;

namespace HeimdallPower.Entities
{
    public class DLRDto
    {
        private double _ampacity;
        public DateTime IntervalStartTime { get; set; }
        public double Ampacity
        {
            get => _ampacity;
            set => _ampacity = Math.Round(value);
        }
    }
}
