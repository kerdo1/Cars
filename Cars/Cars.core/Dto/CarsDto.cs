﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cars.core.Dto
{
    public class CarsDto
    {
        public Guid Id { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public string Plate { get; set; }
        public int Mileage { get; set; }
        public DateTime Made { get; set; }
        public DateTime Added { get; set; }
    }
}
