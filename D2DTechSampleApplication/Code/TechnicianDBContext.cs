using System.Data.Entity;

namespace D2DTechSampleApplication.Models
{
    public class TechnicianDBContext : DbContext
    {
        const string ConnectionStringName = "TechnicianDB";

        public TechnicianDBContext(): base(ConnectionStringName)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //many-to-many
            modelBuilder.Entity<Technician>().HasMany(t => t.Skills).WithMany();
            
            //one-to-many 
            modelBuilder.Entity<WorkTask>()
                        .HasRequired<Technician>(wt => wt.Technician)
                        .WithMany(tech => tech.WorkTasks)
                        .HasForeignKey(wt => wt.TechId)
                        .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Technician> Technicians { get; set; }

        public System.Data.Entity.DbSet<D2DTechSampleApplication.Models.Location> Locations { get; set; }

        public System.Data.Entity.DbSet<D2DTechSampleApplication.Models.Skill> Skills { get; set; }

        public System.Data.Entity.DbSet<D2DTechSampleApplication.Models.WorkTask> WorkTasks { get; set; }

        public System.Data.Entity.DbSet<D2DTechSampleApplication.Models.Customer> Customers { get; set; }
    }
}