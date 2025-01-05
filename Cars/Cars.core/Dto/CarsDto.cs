using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cars.core.Dto
{
    public class CarsDto
    {
        public string Model { get; set; }
        public string Color { get; set; }
        public string Plate { get; set; }
        public DateTime Mileage { get; set; }
        public DateTime Made { get; set; }
    }
}
