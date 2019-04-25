using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Sales
{
    public class Contacts
    {
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
    public class DesignationMaster
    {
        public int DesignationMasterId { get; set; }

        public string DesignationName { get; set; }
    }
    public class GetContacts
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string AccountName { get; set; }
    }
    public class GetContactsParam
    {
        public string Name { get; set; }
        public string Mobile { get; set; }
        public int? AccountId { get; set; }
    }
}
