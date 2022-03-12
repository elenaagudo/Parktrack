using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string Name { get; set; }
        // esto de momento no lo vamos a guardar, en el futuro se puede guardar en la base de datos en base 64
        //public string Image { get; set; }
        public User User { get; set; }
        public Manufacturer Manufacturer { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DeleteDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
