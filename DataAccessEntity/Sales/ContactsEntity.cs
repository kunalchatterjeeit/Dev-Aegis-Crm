using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessEntity.Sales
{
    public class ContactsDbModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? AccountId { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public int? DesignationId { get; set; }
        public string GSTNo { get; set; }
        public string OfficePhone { get; set; }
        public int CreatedBy { get; set; }      
        public bool IsActive { get; set; }
    }
    public class DesignationMasterDbModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DesignationMasterId { get; set; }

        public string DesignationName { get; set; }
    }
    public class GetContactsDbModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string AccountName { get; set; }
    }
    public class GetContactsParamDbModel
    {
        public string Name { get; set; }
        public string Mobile { get; set; }       
        public int? AccountId { get; set; }
        public int AssignEngineer { get; set; }
    }
}
