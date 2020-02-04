using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiAppAegisCRM.Models
{
    public class StockSnapModel : BaseModel
    {
        public string ProductName { get; set; }
        public string SpareName { get; set; }
        public string Location { get; set; }
        public string Quantity { get; set; }
        public string ItemId { get; set; }
        public string ItemType { get; set; }
        public string AssetLocationId { get; set; }
    }
}