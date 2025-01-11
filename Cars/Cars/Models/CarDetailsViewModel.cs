namespace Cars.Models
{
    public class CarDetailsViewModel
    {
        public Guid Id { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public string Plate { get; set; }
        public double Mileage { get; set; }
        public DateTime Made { get; set; }
        public DateTime Added { get; set; }
    }
}
