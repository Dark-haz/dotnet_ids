using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dotnetids.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;


namespace Dotnetids.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Guide> Guides { get; set; }
        public DbSet<Member> Members { get; set; }


        internal class DateOnlyConverter : ValueConverter<DateOnly, DateTime>
        {
            public DateOnlyConverter(): base(d => d.ToDateTime(TimeOnly.MinValue),d => DateOnly.FromDateTime(d)){ }
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder builder)
        {
            // Store DateOnly values as 'date' columns
            builder.Properties<DateOnly>()
                .HaveConversion<DateOnlyConverter>()
                .HaveColumnType("date");

            // Any other "global" model configurations go here
        }
    }

}