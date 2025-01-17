﻿namespace Cars.Models
{
    public class AddCarViewModel
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
