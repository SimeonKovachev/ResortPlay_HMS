using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using ResortPlay.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ResortPlay.Data
{
    public class ResortPlayContext : IdentityDbContext<HMS_User>
    {
        public ResortPlayContext() : base("HMSConnectionString")
        {
        }
        public static ResortPlayContext Create()
        {
            return new ResortPlayContext();
        }

        public DbSet<AccomodationType> AccomodationTypes { get; set; }
        public DbSet<AccomodationPackage> AccomodationPackages { get; set; }
        public DbSet<Accomodation> Accomodations { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        


    }
}
