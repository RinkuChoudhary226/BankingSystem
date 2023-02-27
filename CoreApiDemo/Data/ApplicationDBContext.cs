using BankingSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions options) : base(options) { }
        public DbSet<Bank> banks { get; set; }
        public DbSet<BankAccountType> bankAccountTypes { get; set; }
        public DbSet<Branch> branches { get; set; }
        public DbSet<Customer> customers { get; set; }
        public DbSet<CustomerAccount> customerAccounts { get; set; }
        public DbSet<Transaction> transactions { get; set; }
        public DbSet<TransactionType> transactionTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bank>();
            modelBuilder.Entity<BankAccountType>();
            modelBuilder.Entity<Branch>();
            modelBuilder.Entity<Customer>();
            modelBuilder.Entity<CustomerAccount>();
            modelBuilder.Entity<Transaction>();
            modelBuilder.Entity<TransactionType>();
            base.OnModelCreating(modelBuilder);
        }

    }
}