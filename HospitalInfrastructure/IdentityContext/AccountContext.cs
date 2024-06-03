using HospitalDomain.Entites.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalInfrastructure.IdentityContext
{
    public class AccountContext : IdentityDbContext
    {
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Guest> Guest { get; set; }
        public DbSet<Account> Accounts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Account>(entity => { entity.ToTable("Accounts"); });
            builder.Entity<Employee>(entity => { entity.ToTable("Employee"); });
            builder.Entity<Guest>(entity => { entity.ToTable("Guest"); });
        }

        public AccountContext(DbContextOptions<AccountContext> options)
            : base(options)
        {
        }
    }
}