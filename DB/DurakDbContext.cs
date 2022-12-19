using DB.Entities;
using DB.Schema;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DB
{
    public class DurakDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DurakDbContext(DbContextOptions<DurakDbContext> options): base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            modelBuilder.ApplyConfiguration(new UserEntitySchemaDefinition());
            base.OnModelCreating(modelBuilder);
        }
    }
}
