using System;

namespace HeimdallApi.Entities
{
    public class SagAndClearanceDto
    {
        public DateTime Timestamp { get; set; }
        public double Sag { get; set; }
        public double Clearance { get; set; }
    }
}
