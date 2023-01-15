using ResortPlay.Data;
using ResortPlay.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AccomodationTypesService
    {
        public IEnumerable<AccomodationType> GetAllAccomodationTypes()
        {
            var context = new ResortPlayContext();

            return context.AccomodationTypes.ToList();
        }
        public AccomodationType GetAccomodationTypeById(int Id)
        {
            var context = new ResortPlayContext();

            return context.AccomodationTypes.Find(Id);
        }
        public bool SaveAccomodationTypes(AccomodationType accomodationType)
        {
            var context = new ResortPlayContext();

            context.AccomodationTypes.Add(accomodationType);
            return context.SaveChanges() > 0;
        }
        public bool EditAccomodationTypes(AccomodationType accomodationType)
        {
            var context = new ResortPlayContext();

            context.Entry(accomodationType).State = System.Data.Entity.EntityState.Modified;

            return context.SaveChanges() > 0;
        }
        public bool DeleteAccomodationTypes(AccomodationType accomodationType)
        {
            var context = new ResortPlayContext();

            context.AccomodationTypes.Remove(accomodationType);
            return context.SaveChanges() > 0;
        }

    }
}
