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
            Database.SetInitializer<CRMContext>(null);
            base.OnModelCreating(modelBuilder);

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
            modelBuilder.Entity<Sales.ContactsDbModel>()
               .ToTable("tbl_Sales_Contacts");
            modelBuilder.Entity<Sales.NotesDbModel>()
               .ToTable("tbl_Sales_Notes");
            modelBuilder.Entity<Sales.TaskPriorityDbModel>()
              .ToTable("tbl_Sales_TasksPriority");
            modelBuilder.Entity<Sales.TaskStatusDbModel>()
              .ToTable("tbl_Sales_TasksStatus");
            modelBuilder.Entity<Sales.TaskRelatedToDbModel>()
              .ToTable("tbl_Sales_TasksRelated");
            modelBuilder.Entity<Sales.TasksDbModel>()
              .ToTable("tbl_Sales_Tasks");
            modelBuilder.Entity<Sales.MeetingStatusDbModel>()
             .ToTable("tbl_Sales_MeetingStatus");
            modelBuilder.Entity<Sales.MeetingTypeDbModel>()
              .ToTable("tbl_Sales_MeetingType");
            modelBuilder.Entity<Sales.MeetingsDbModel>()
              .ToTable("tbl_Sales_Meetings");
            modelBuilder.Entity<Sales.CampaignDbModel>()
              .ToTable("tbl_Sales_Campaign");
            modelBuilder.Entity<Sales.DepartmentDbModel>()
              .ToTable("tbl_Sales_Department");
            modelBuilder.Entity<Sales.LeadsDbModel>()
              .ToTable("tbl_Sales_Leads");
            modelBuilder.Entity<Sales.CustomerTypeDbModel>()
              .ToTable("tbl_Sales_CustomerType");
            modelBuilder.Entity<Sales.LeadSourceDbModel>()
              .ToTable("tbl_Sales_LeadSource");
            modelBuilder.Entity<Sales.AccountsDbModel>()
              .ToTable("tbl_Sales_Accounts");
            modelBuilder.Entity<Sales.DesignationMasterDbModel>()
             .ToTable("tbl_HR_DesignationMaster");

        }
        public DbSet<Sales.CallsDbModel> Calls { get; set; }
        public DbSet<Sales.CallStatusDbModel> CallStatus { get; set; }
        public DbSet<Sales.CallRelatedDbModel> CallRelated { get; set; }
        public DbSet<Sales.CallRepeatTypeDbModel> CallRepeatType { get; set; }
        public DbSet<Sales.CallDirectionDbModel> CallDirection { get; set; }
        public DbSet<Sales.ContactsDbModel> Contacts { get; set; }
        public DbSet<Sales.NotesDbModel> Notes { get; set; }
        public DbSet<Sales.TaskPriorityDbModel> TaskPriority { get; set; }
        public DbSet<Sales.TaskRelatedToDbModel> TaskRelatedTo { get; set; }
        public DbSet<Sales.TaskStatusDbModel> TaskStatus { get; set; }
        public DbSet<Sales.TasksDbModel> Tasks{ get; set; }
        public DbSet<Sales.MeetingStatusDbModel> MeetingStatus { get; set; }
        public DbSet<Sales.MeetingTypeDbModel> MeetingType { get; set; }
        public DbSet<Sales.MeetingsDbModel> Meeting { get; set; }
        public DbSet<Sales.CampaignDbModel> Campaign { get; set; }
        public DbSet<Sales.DepartmentDbModel> Department { get; set; }
        public DbSet<Sales.LeadsDbModel> Leads { get; set; }
        public DbSet<Sales.CustomerTypeDbModel> CustomerType { get; set; }
        public DbSet<Sales.LeadSourceDbModel> LeadSource { get; set; }
        public DbSet<Sales.AccountsDbModel> Accounts { get; set; }
        public DbSet<Sales.DesignationMasterDbModel> Designations { get; set; }
    }
}
