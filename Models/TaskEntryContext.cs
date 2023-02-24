using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group2_3_Mission8.Models
{
    public class TaskEntryContext : DbContext
    {
        public TaskEntryContext(DbContextOptions<TaskEntryContext> options) : base(options)
        {
            // Leave blank for now
        }

        public DbSet<TaskFormResponse> Task { get; set; }
        public DbSet<Category> CategorySet { get; set; }


        // Seeding the data
        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.Entity<Category>().HasData(
                new Category
                {
                    CategoryId = 1,
                    CategoryName = "Home"
                },
                new Category
                {
                    CategoryId = 2,
                    CategoryName = "School"
                },
                new Category
                {
                    CategoryId = 3,
                    CategoryName = "Work"
                },
                new Category
                {
                    CategoryId = 4,
                    CategoryName = "Church"
                }
                );
        }

    }
}
