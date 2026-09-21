using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Backend_AguaTracker.Domain;

namespace Backend_AguaTracker.Repository
{
    public class AguaTracker_DbContext : DbContext
    {
        public AguaTracker_DbContext(DbContextOptions<AguaTracker_DbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Configure your entity mappings here

        }

        public DbSet<User> Users { get; set; }
        public DbSet<WaterIntake> WaterIntakes { get; set; }
        public DbSet<DailyResume> DailyResumes { get; set; }
    }
}
