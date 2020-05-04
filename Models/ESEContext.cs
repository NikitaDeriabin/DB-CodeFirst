using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
//using System.Data.Entity;

namespace EasySportEvent.Models
{
    public class ESEContext : DbContext
    {
        public virtual DbSet<Sport> Sports { get; set; }
        public virtual DbSet<Region> Regions { get; set; }
        public virtual DbSet<League> Leagues { get; set; }
        public virtual DbSet<Person> Persons { get; set; }
        public virtual DbSet<Team> Teams { get; set; }
        public virtual DbSet<Squad> Squads { get; set; }
        public virtual DbSet<Event> Events { get; set; }

        public ESEContext(DbContextOptions<ESEContext> options):base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Region>()
                        .HasOne(r => r.Sport)
                        .WithMany(e => e.Regions)
                        .HasForeignKey(s => s.SportId)
                        .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
