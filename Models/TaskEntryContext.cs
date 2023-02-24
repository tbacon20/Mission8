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
        public DbSet<Category> Category { get; set; }

    }
}
