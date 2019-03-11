using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace DataAccessEntity
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
            modelBuilder.Entity<Sales.CallsDbModel>()
                .ToTable("tbl_Sales_Calls");
            modelBuilder.Entity<Sales.CallStatusDbModel>()
                .ToTable("tbl_Sales_CallStatus");
            modelBuilder.Entity<Sales.CallRepeatTypeDbModel>()
                .ToTable("tbl_Sales_CallRepeatType");
            modelBuilder.Entity<Sales.CallRelatedDbModel>()
                .ToTable("tbl_Sales_CallRelated");
            modelBuilder.Entity<Sales.CallDirectionDbModel>()
                .ToTable("tbl_Sales_CallDirection");

        }
        public DbSet<Sales.CallsDbModel> Calls { get; set; }
        public DbSet<Sales.CallStatusDbModel> CallStatus { get; set; }
        public DbSet<Sales.CallRelatedDbModel> CallRelated { get; set; }
        public DbSet<Sales.CallRepeatTypeDbModel> CallRepeatType { get; set; }
        public DbSet<Sales.CallDirectionDbModel> CallDirection { get; set; }
    }
}
