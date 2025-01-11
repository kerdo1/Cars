namespace Cars.Models
{
    public class CarsViewModel
    {
        public Guid Id { get; set; }
        public string Model {  get; set; }
        public string Color { get; set; }
        public string Plate { get; set; }
        public int Mileage { get; set; }
        public DateTime Made { get; set; }

        public List<CarsViewModel> Cars { get; set; } = new List<CarsViewModel>();


    }
}
