using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework
{
    public class NorthWindContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=LAPTOP-AEBEQNQ5\UGURSERVER;Database=Northwind;Trusted_Connection=true");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ViewProductDetail>(entity =>
            {
                entity.HasNoKey();
            });
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ViewProductDetail> ViewProductDetail { get; set; }


        public DbSet<OperationClaimUsers> OperationClaimUsers { get; set; }
        public DbSet<OperationClaim> OperationClaim { get; set; }
        public DbSet<User> Users { get; set; }

    }
}
