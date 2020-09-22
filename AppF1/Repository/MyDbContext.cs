using AppF1.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace AppF1.Repository
{
    class MyDbContext : DbContext
    {
        public DbSet<Person> People { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=TestDatabase.db", options =>
            {
                options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
            });
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.PAI);
                entity.Property(e => e.DN).HasDefaultValueSql("CURRENT_TIMESTAMP");
            });

            modelBuilder.Entity<Person>()
                .HasMany(p => p.People)
                .WithOne(answer => answer.Parent)
                .HasForeignKey(answer => answer.ParentId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
