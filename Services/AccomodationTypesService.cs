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

            return context.AccomodationTypes;
        }
        public bool SaveAccomodationTypes(AccomodationType accomodationType)
        {
            var context = new ResortPlayContext();

            context.AccomodationTypes.Add(accomodationType);
            return context.SaveChanges() > 0;
        }
    }
}
