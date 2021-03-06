﻿using Entity.Inventory;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Sale
{
    public class SaleChallan
    {
        public SaleChallan()
        {
            SaleChallanDetailsCollection = new Collection<SaleChallanDetails>();
        }
        public int SaleChallanId { get; set; }
        public long CustomerMasterId { get; set; }
        public string CustomerName { get; set; }
        public string Note { get; set; }
        public string ChallanNo { get; set; }
        public string OrderNo { get; set; }
        public DateTime OrderDate { get; set; }
        public int ChallanTypeId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime OrderFromDate { get; set; }
        public DateTime OrderToDate { get; set; }
        public int ItemId { get; set; }
        public ItemType itemType { get; set; }

        public Collection<SaleChallanDetails> SaleChallanDetailsCollection { get; set; }
    }
}
