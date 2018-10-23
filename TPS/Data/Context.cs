using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using TPS.Models;

namespace TPS.Data
{
    public class Context : IdentityDbContext<UserLogin>
    {
        public DbSet<StaffingRequest> StaffingRequests { get; set; }
        public DbSet<StaffMember> StaffMembers { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<ContractManager> ContractManagers { get; set; }

        public Context()
        {
            //Database.SetInitializer(new DropCreateDatabaseIfModelChanges<Context>());
            Database.SetInitializer(new DropCreateDatabaseAlways<Context>());
        }
    }
}