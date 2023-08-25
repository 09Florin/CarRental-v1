namespace CarRental.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Color { get; set; }
        public decimal RentalPricePerDay { get; set; }
        public string Image { get; set; }

        public Car()
        {
            Manufacturer = string.Empty;
            Model = string.Empty;
            Color = string.Empty;
        }
    }
}
