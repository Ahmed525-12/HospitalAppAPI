using HospitalDomain.Entites.App;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace HospitalInfrastructure.AppContext
{
    public class HospitalContext : DbContext
    {
        public HospitalContext(DbContextOptions<HospitalContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
            // Disable cascade delete for Department-Session relationship
            modelBuilder.Entity<Department>()
                .HasOne(d => d.Session)
                .WithMany()
                .HasForeignKey(d => d.SessionId)
                .OnDelete(DeleteBehavior.Restrict);

            // Disable cascade delete for Department-History relationship
            modelBuilder.Entity<Department>()
                .HasOne(d => d.History)
                .WithMany()
                .HasForeignKey(d => d.HistoryId)
                .OnDelete(DeleteBehavior.Restrict);
            // Configure the foreign key for Medicine-Pharmacy relationship
            modelBuilder.Entity<Medicines>()
                .HasOne(m => m.Pharmacy)
                .WithMany(p => p.Medicines)
                .HasForeignKey(m => m.PharmacyId)
                .OnDelete(DeleteBehavior.Restrict);
        }

        public DbSet<Category> Category { get; set; }
        public DbSet<Complaints> Complaints { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<History> History { get; set; }
        public DbSet<Medicines> Medicines { get; set; }
        public DbSet<Notes> Notes { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<Pharmacy> Pharmacy { get; set; }
        public DbSet<Reports> Reports { get; set; }
        public DbSet<Session> Session { get; set; }
    }
}