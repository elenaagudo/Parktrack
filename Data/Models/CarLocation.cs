using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class CarLocation
    {
        public int Id { get; set; }
        public Car Car { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public DateTime ParkingDate { get; set; }
    }
}
