using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity.Inventory
{
    public class SpareMaster
    {
        public SpareMaster()
        { }

        public int SpareId { get; set; }
        public string SpareName { get; set; }
        public int SpareType { get; set; }
        public string Description { get; set; }
        public int Yield { get; set; }
        public bool isTonner { get; set; }
    }
}
