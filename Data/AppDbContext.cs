using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dotnetids.Models;
using Microsoft.EntityFrameworkCore;


namespace Dotnetids.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Guide> Guides { get; set; }
        public DbSet<Member> Members { get; set; }

        //! SEEDING

    }
}