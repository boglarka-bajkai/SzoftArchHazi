using System.Data.Entity;

namespace SzoftArchHazi.Data
{
    public class SzoftArchContext : DbContext
    {
        public SzoftArchContext() : base("SzoftArchDB") 
        {
            Database.SetInitializer<SzoftArchContext>(new SzoftArchDBInitializer());
        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<OnDuty> OnDuties { get; set; }
        public DbSet<OnDutyDate> OnDutyDates { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
