﻿using System;

namespace HeimdallPower.Entities
{
    public class DLRDto
    {
        private double _ampacity;
        public DateTime Timestamp { get; set; }
        public double Ampacity
        {
            get => _ampacity;
            set => _ampacity = Math.Round(value);
        }
    }
}