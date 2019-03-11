using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Entity;

namespace EntityDataAccess
{
    public class CRMContext:DbContext
    {
        public CRMContext()
            : base("ConStr")
        {
            Database.SetInitializer<CRMContext>(new CreateDatabaseIfNotExists<CRMContext>());
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sales.CallsEntity>()
                .ToTable("tbl_Sales_Calls");
            modelBuilder.Entity<Sales.CallStatus>()
                .ToTable("tbl_Sales_CallStatus");
            modelBuilder.Entity<Sales.CallRepeatType>()
                .ToTable("tbl_Sales_CallRepeatType");
            modelBuilder.Entity<Sales.CallRelated>()
                .ToTable("tbl_Sales_CallRelated");
            modelBuilder.Entity<Sales.CallDirection>()
                .ToTable("tbl_Sales_CallDirection");

        }
        public DbSet<Sales.CallsEntity> Calls { get; set; }
        public DbSet<Sales.CallStatus> CallStatus { get; set; }
        public DbSet<Sales.CallRelated> CallRelated { get; set; }
        public DbSet<Sales.CallRepeatType> CallRepeatType { get; set; }
        public DbSet<Sales.CallDirection> CallDirection { get; set; }
    }
}
