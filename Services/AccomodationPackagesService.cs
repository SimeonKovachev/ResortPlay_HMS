using ResortPlay.Data;
using ResortPlay.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AccomodationPackagesService
    {
        //Get everything in the data table
        public IEnumerable<AccomodationPackage> GetAllAccomodationPackages()
        {
            var context = new ResortPlayContext();

            return context.AccomodationPackages.AsEnumerable();
        }
        //Search in data table
        public IEnumerable<AccomodationPackage> SearchAccomodationPackages(string searchTerm)
        {
            var context = new ResortPlayContext();

            var accomodationPackages = context.AccomodationPackages.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                accomodationPackages = accomodationPackages.Where(a => a.Name.ToLower().Contains(searchTerm.ToLower()));
            }

            return accomodationPackages.AsEnumerable();
        }
        //GetById service
        public AccomodationPackage GetAccomodationPackageById(int Id)
        {
            var context = new ResortPlayContext();

            return context.AccomodationPackages.Find(Id);
        }
        //Create Service
        public bool SaveAccomodationPackage(AccomodationPackage accomodationPackage)
        {
            var context = new ResortPlayContext();

            context.AccomodationPackages.Add(accomodationPackage);
            return context.SaveChanges() > 0;
        }
        //Edit Service
        public bool EditAccomodationPackage(AccomodationPackage accomodationPackage)
        {
            var context = new ResortPlayContext();

            context.Entry(accomodationPackage).State = System.Data.Entity.EntityState.Modified;

            return context.SaveChanges() > 0;
        }
        //Delete service
        public bool DeleteAccomodationPackage(AccomodationPackage accomodationPackage)
        {
            var context = new ResortPlayContext();

            context.Entry(accomodationPackage).State = System.Data.Entity.EntityState.Deleted;

            return context.SaveChanges() > 0;
        }
    }
}
