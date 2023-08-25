using CarRental.Models;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarRental.Models
{
    public class Rental
    {
        public int Id { get; set; }

        [ForeignKey("Client")]
        public int ClientId { get; set; }
        public virtual Client? Client { get; set; }

        [ForeignKey("Car")]
        public int CarId { get; set; }
        public virtual Car? Car { get; set; }

        public DateTime PickupDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public decimal TotalPrice { get; set; }
        public string ClientUsername { get; set; }
        public string CarManufacturer { get; set; }
        public string CarModel { get; set; }
    }
}
