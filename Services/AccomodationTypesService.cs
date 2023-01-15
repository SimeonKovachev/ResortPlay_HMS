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
        //Get everything in the data table
        public IEnumerable<AccomodationType> GetAllAccomodationTypes()
        {
            var context = new ResortPlayContext();

            return context.AccomodationTypes.AsEnumerable();
        }
        //Search in data table
        public IEnumerable<AccomodationType> SearchAccomodationTypes(string searchTerm)
        {
            var context = new ResortPlayContext();

            var accomodationTypes = context.AccomodationTypes.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                accomodationTypes = accomodationTypes.Where(a => a.Name.ToLower().Contains(searchTerm.ToLower()));
            }

            return accomodationTypes.AsEnumerable();
        }
        //GetById service
        public AccomodationType GetAccomodationTypeById(int Id)
        {
            var context = new ResortPlayContext();

            return context.AccomodationTypes.Find(Id);
        }
        //Create Service
        public bool SaveAccomodationTypes(AccomodationType accomodationType)
        {
            var context = new ResortPlayContext();

            context.AccomodationTypes.Add(accomodationType);
            return context.SaveChanges() > 0;
        }
        //Edit Service
        public bool EditAccomodationTypes(AccomodationType accomodationType)
        {
            var context = new ResortPlayContext();

            context.Entry(accomodationType).State = System.Data.Entity.EntityState.Modified;

            return context.SaveChanges() > 0;
        }
        //Delete service
        public bool DeleteAccomodationTypes(AccomodationType accomodationType)
        {
            var context = new ResortPlayContext();

            context.Entry(accomodationType).State = System.Data.Entity.EntityState.Deleted;

            return context.SaveChanges() > 0;
        }

    }
}
