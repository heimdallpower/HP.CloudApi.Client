using System;

namespace HeimdallPower.Entities
{
    public class ForecastDto : DLRDto
    {
        public DateTime Timestamp { get; set; }
        public PredictionDto Prediction { get; set; }
    }
}
