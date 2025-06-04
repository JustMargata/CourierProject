using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMarten.Model
{
    public class Parcel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; } 
        public decimal Price { get; set; }
        public decimal Weight { get; set; }

        public int ParcelTypeId { get; set; }
        public ParcelType ParcelTypes { get; set; }

    }
}
