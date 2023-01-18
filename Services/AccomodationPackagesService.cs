using ResortPlay.Data;
using ResortPlay.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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

            return context.AccomodationPackages.ToList();
        }
        //Search in data table
        public IEnumerable<AccomodationPackage> SearchAccomodationPackages(string searchTerm, int? accomodationTypeId, int page, int recordSize)
        {
            var context = new ResortPlayContext();

            var accomodationPackages = context.AccomodationPackages.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                accomodationPackages = accomodationPackages.Where(a => a.Name.ToLower().Contains(searchTerm.ToLower()));
            }
            if (accomodationTypeId.HasValue && accomodationTypeId.Value > 0)
            {
                accomodationPackages = accomodationPackages.Where(a => a.AccomodationTypeId ==  accomodationTypeId.Value);
            }

            //that is for the passed pages and how many packages you have skipped.
            var skip = (page - 1) * recordSize;
            // skip = (1 - 1) = 0 * 3 = 0
            // skip = 1 * 3 = 3




            return accomodationPackages.OrderBy(x => x.AccomodationTypeId).Skip(skip).Take(recordSize).ToList();
        }
        public int SearchAccomodationPackagesCount(string searchTerm, int? accomodationTypeId)
        {
            var context = new ResortPlayContext();

            var accomodationPackages = context.AccomodationPackages.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                accomodationPackages = accomodationPackages.Where(a => a.Name.ToLower().Contains(searchTerm.ToLower()));
            }
            if (accomodationTypeId.HasValue && accomodationTypeId.Value > 0)
            {
                accomodationPackages = accomodationPackages.Where(a => a.AccomodationTypeId == accomodationTypeId.Value);
            }
            return accomodationPackages.Count();
        }
        //GetById service
        public AccomodationPackage GetAccomodationPackageById(int Id)
        {
            using (var context = new ResortPlayContext())
            {
                return context.AccomodationPackages.Find(Id);
            }
            

            
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
