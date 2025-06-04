using ProjectMarten.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMarten.Controller
{
    public class ParcelController
    {
        private CourierContext _courierContext = new CourierContext();
        public Parcel Get(int id)
        {
            Parcel findedParcel = _courierContext.Parcels.Find(id);
            if (findedParcel != null)
            {
                _courierContext.Entry(findedParcel).Reference(x => x.ParcelTypes).Load();
            }
            return findedParcel;
        }
        public List<Parcel> GetAll()
        {
            return _courierContext.Parcels.Include("ParcelType").ToList();
        }
        public void Create(Parcel parcel)
        {
            _courierContext.Parcels.Add(parcel);
            _courierContext.SaveChanges();
        }
        public void Update(int id, Parcel parcel)
        {
            Parcel findedParcel = _courierContext.Parcels.Find(id);
            if (findedParcel != null)
            {
                return;
            }
            findedParcel.Price = parcel.Price;
            findedParcel.Name = parcel.Name;
            findedParcel.Weight = parcel.Weight;
            findedParcel.Description = parcel.Description;
            findedParcel.ParcelTypeId = parcel.ParcelTypeId;
            _courierContext.SaveChanges();
        }
        public void Delete(int id)
        {
            Parcel findedParcel = _courierContext.Parcels.Find(id);
            _courierContext.Parcels.Remove(findedParcel);
            _courierContext.SaveChanges();
        }
    }
}