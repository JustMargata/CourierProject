using ProjectMarten.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMarten.Controller
{
    public class ParcelTypeController
    {
        private CourierContext _courierContext = new CourierContext();
        public List<ParcelType> GetParcelTypes()
        {
            return _courierContext.ParcelTypes.ToList();
        }
        public string GetParcelTypeById(int id)
        {
            return _courierContext.ParcelTypes.Find(id).Name;
        }
    }
}
