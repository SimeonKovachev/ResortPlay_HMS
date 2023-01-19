using ResortPlay.Data;
using ResortPlay.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AccomodationsService
    {
        //Get everything in the data table
        public IEnumerable<Accomodation> GetAllAccomodations()
        {
            var context = new ResortPlayContext();

            return context.Accomodations.ToList();
        }
        //Search in data table
        public IEnumerable<Accomodation> SearchAccomodations(string searchTerm, int? accomodationPackageId, int page, int recordSize)
        {
            var context = new ResortPlayContext();

            var accomodations = context.Accomodations.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                accomodations = accomodations.Where(a => a.Name.ToLower().Contains(searchTerm.ToLower()));
            }
            if (accomodationPackageId.HasValue && accomodationPackageId.Value > 0)
            {
                accomodations = accomodations.Where(a => a.AccomodationPackageId == accomodationPackageId.Value);
            }

            //that is for the passed pages and how many packages you have skipped.
            var skip = (page - 1) * recordSize;
            // skip = (1 - 1) = 0 * 3 = 0
            // skip = 1 * 3 = 3




            return accomodations.OrderBy(x => x.AccomodationPackageId).Skip(skip).Take(recordSize).ToList();
        }
        public int SearchAccomodationsCount(string searchTerm, int? accomodationPackageId)
        {
            var context = new ResortPlayContext();

            var accomodations = context.Accomodations.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                accomodations = accomodations.Where(a => a.Name.ToLower().Contains(searchTerm.ToLower()));
            }
            if (accomodationPackageId.HasValue && accomodationPackageId.Value > 0)
            {
                accomodations = accomodations.Where(a => a.AccomodationPackageId == accomodationPackageId.Value);
            }
            return accomodations.Count();
        }
        //GetById service
        public Accomodation GetAccomodationById(int Id)
        {
            using (var context = new ResortPlayContext())
            {
                return context.Accomodations.Find(Id);
            }
        }
        //Create Service
        public bool SaveAccomodation(Accomodation accomodation)
        {
            var context = new ResortPlayContext();

            context.Accomodations.Add(accomodation);
            return context.SaveChanges() > 0;
        }
        //Edit Service
        public bool EditAccomodation(Accomodation accomodation)
        {
            var context = new ResortPlayContext();

            context.Entry(accomodation).State = System.Data.Entity.EntityState.Modified;

            return context.SaveChanges() > 0;
        }
        //Delete service
        public bool DeleteAccomodation(Accomodation accomodation)
        {
            var context = new ResortPlayContext();

            context.Entry(accomodation).State = System.Data.Entity.EntityState.Deleted;

            return context.SaveChanges() > 0;
        }
    }
}
