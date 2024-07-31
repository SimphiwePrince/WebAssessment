using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Restul_Web_Assessment.Repository.Models;

namespace Restul_Web_Assessment.Repository;

public partial class BankingDbContext : DbContext
{
    public BankingDbContext()
    {
    }

    public BankingDbContext(DbContextOptions<BankingDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Models.Account> Accounts { get; set; }

    public virtual DbSet<UserModel> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=TestDB;Integrated Security=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Models.Account>(entity =>
        {
            entity.Property(e => e.AccountNumber).ValueGeneratedOnAdd();
            entity.Property(e => e.AccountType).IsFixedLength();

        });

        modelBuilder.Entity<UserModel>().HasData(
           new UserModel
           {
               UserId = -2,
               FirstName = "John",
               LastName = "Doe",
               DateOfBirth = DateOnly.Parse("2011-04-30"),
               Idnumber = "123456",
               ResidentialAddress = "Pretoria",
               MobileNumber = "73822434",
               EmailAddress = "s@phiwe",
               Password = "password123",
               
           },
           new UserModel
           {
               UserId = -1,
               FirstName = "Jane",
               LastName = "Doe",
               DateOfBirth = DateOnly.Parse("2006-11-03"),
               Idnumber = "789012",
               ResidentialAddress = "Durban",
               MobileNumber = "572498",
               EmailAddress = "s@tseke",
               Password = "password789"
               
           }
       );

        modelBuilder.Entity<Models.Account>().HasData(
        new Models.Account
        {
            AccountNumber = -1,
            AccountType = "Cheque",
            AccountHolder = "Simphiwe",
            IsActive = "True",
            Balance = 1000,
            DateModified = DateTime.Now,
        },
        new Models.Account
        {
            AccountNumber = -2,
            AccountType = "Cheque",
            AccountHolder = "Thabiso",
            IsActive = "True",
            Balance = 350,
            DateModified = DateTime.Now,
        }
    );


        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
