using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TPS.Models;

namespace TPS.Data
{

    public class LocationRepository
    {
        public List<Location> GetLocations()
        {
            List<Location> locations = new List<Location>();

            using (var context = new Context())
            {
                locations = context.Locations
                    .OrderByDescending(l => l.ID)
                    .ToList();
            }

            return locations;
        }

        public Location GetLocation(int id)
        {
            Location location = new Location();

            using (var context = new Context())
            {
                location = context.Locations
                    .Where(s => s.ID == id)
                    .FirstOrDefault();
            }

            return location;
        }

        public void AddLocation(Location location)
        {
            using (var context = new Context())
            {
                context.Locations.Add(new Location()
                {
                    ID = location.ID,
                    Name = location.Name,
                    Address = location.Address
                });
                context.SaveChanges();
            }
        }

        public void UpdateLocation(Location location)
        {
            using (var context = new Context())
            {
                var locationToUpdate =
                (from s in context.Locations
                 where s.ID == location.ID
                 select s).First();

                context.Entry(locationToUpdate).CurrentValues
                    .SetValues(location);

                context.SaveChanges();
            }
        }

        public void DeleteLocation(int id)
        {
            using (var context = new Context())
            {
                Location locationToDelete = context.Locations
                    .Where(s => s.ID == id)
                    .SingleOrDefault();

                context.Locations.Remove(locationToDelete);
                context.SaveChanges();
            }
        }
    }
}