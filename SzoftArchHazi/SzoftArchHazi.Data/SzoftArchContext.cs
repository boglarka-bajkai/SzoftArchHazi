using System.Data.Entity;

namespace SzoftArchHazi.Data
{
    public class SzoftArchContext : DbContext
    {
        public SzoftArchContext() : base("SzoftArchDB") 
        {
            Database.SetInitializer<SzoftArchContext>(new DropCreateDatabaseAlways<SzoftArchContext>());
        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<OnDuty> OnDuties { get; set; }
        public DbSet<OnDutyDate> OnDutyDates { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().ToTable("Employee");
            modelBuilder.Entity<Project>().ToTable("Project");
            modelBuilder.Entity<OnDuty>().ToTable("OnDuty");
            modelBuilder.Entity<OnDutyDate>().ToTable("OnDutyDate");
        }
    }
}
