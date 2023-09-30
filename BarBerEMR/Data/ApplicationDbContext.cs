using BarBerEMR.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using BarBerEMR.ViewModels;

namespace BarBerEMR.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ApplicationUser>().ToTable("Users");
            builder.Entity<IdentityRole>().ToTable("Roles");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogin");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");
            builder.Entity<PaymentMethod>().ToTable("PaymentMethod");
            builder.Entity<Packages>().ToTable("Packages");
            builder.Entity<Invoice>().ToTable("Invoice");
            builder.Entity<Invoice>().HasMany(x => x.PackagesInvoice).WithOne(y => y.Invoice)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<Packages>().HasMany(x => x.PackagesInvoice).WithOne(y => y.Packages)
                .OnDelete(DeleteBehavior.SetNull);
            builder.Entity<InvoicePackagesvm>().Ignore(x => x.SkillID);

        }

        public virtual DbSet<PaymentMethod> PaymentMethod { get; set; }
        public virtual DbSet<Packages> Packages { get; set; }
        public virtual DbSet<Invoice> Invoice { get; set; }
        public DbSet<BarBerEMR.ViewModels.InvoicePackagesvm> InvoicePackagesvm { get; set; }
        //public virtual DbSet<InvoicePackages> InvoicePackages { get; set; }

    }
}
