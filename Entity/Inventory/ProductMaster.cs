using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Entity.Inventory
{
    public class ProductMaster
    {
        public ProductMaster()
        { }

        private int brandId = 0;
        private int productMasterId = 0;
        private int parentProductMasterId = 0;
        private string productCode = string.Empty;
        private string productName = string.Empty;  
        private string productSpecification = string.Empty;
        private Int64 productAmcId = 0;
        private decimal amcAmount = 0;
        private int amcFreeCall = 0;
        private int amcId = 0;
        private decimal productCostPrice = 0;
        private decimal productMRP = 0;
        private decimal productServiceCharge = 0;
        private Int64 productCostId = 0;
        private int productDealerCostId = 0;
        private int partyId = 0;
        private decimal productDealerPrice = 0;
        private int machineLife = 0;
        private int spareId = 0;
        public int ProductCategoryId { get; set; }
        public int MCBF { get; set; }
        public int MTBF { get; set; }


        public int BrandId
        {
            get { return brandId; }
            set { brandId = value; }
        }
        public int SpareId
        {
            get { return spareId; }
            set { spareId = value; }
        }

        public int MachineLife
        {
            get { return machineLife; }
            set { machineLife = value; }
        }

        public decimal ProductDealerPrice
        {
            get { return productDealerPrice; }
            set { productDealerPrice = value; }
        }

        public int PartyId
        {
            get { return partyId; }
            set { partyId = value; }
        }

        public int ProductDealerCostId
        {
            get { return productDealerCostId; }
            set { productDealerCostId = value; }
        }

        public Int64 ProductCostId
        {
            get { return productCostId; }
            set { productCostId = value; }
        }

        public decimal ProductServiceCharge
        {
            get { return productServiceCharge; }
            set { productServiceCharge = value; }
        }

        public decimal ProductMRP
        {
            get { return productMRP; }
            set { productMRP = value; }
        }

        public decimal ProductCostPrice
        {
            get { return productCostPrice; }
            set { productCostPrice = value; }
        }

        public int AmcId
        {
            get { return amcId; }
            set { amcId = value; }
        }
        private int uOMId = 0;
        private int reOrderLevel = 0;
        private int leadTime = 0;
        private int point = 0;
        private int type = 0;
        private int nature = 0;
        private int userId = 0;
        private int companyMasterId = 0;
        private DataTable productGroupProductMapping = null;
        private DataTable dtmapping = null;
        private int productgroupid = 0;

        public int AmcFreeCall
        {
            get { return amcFreeCall; }
            set { amcFreeCall = value; }
        }

        public decimal AmcAmount
        {
            get { return amcAmount; }
            set { amcAmount = value; }
        }
        public Int64 ProductAmcId
        {
            get { return productAmcId; }
            set { productAmcId = value; }
        }

        public int Productgroupid
        {
            get { return productgroupid; }
            set { productgroupid = value; }
        }

        public DataTable dtMapping
        {
            get { return dtmapping; }
            set { dtmapping = value; }
        }

        public DataTable ProductGroupProductMapping
        {
            get { return productGroupProductMapping; }
            set { productGroupProductMapping = value; }
        }

        public int ProductMasterId
        {
            get { return productMasterId; }
            set { productMasterId = value; }
        }
        public int ParentProductMasterId
        {
            get { return parentProductMasterId; }
            set { parentProductMasterId = value; }
        }
        public string ProductCode
        {
            get { return productCode; }
            set { productCode = value; }
        }
        public string ProductName
        {
            get { return productName; }
            set { productName = value; }
        }
        public string ProductSpecification
        {
            get { return productSpecification; }
            set { productSpecification = value; }
        }

        public int UOMId
        {
            get { return uOMId; }
            set { uOMId = value; }
        }
        public int ReOrderLevel
        {
            get { return reOrderLevel; }
            set { reOrderLevel = value; }
        }
        public int LeadTime
        {
            get { return leadTime; }
            set { leadTime = value; }
        }
        public int Point
        {
            get { return point; }
            set { point = value; }
        }
        public int Type
        {
            get { return type; }
            set { type = value; }
        }
        public int Nature
        {
            get { return nature; }
            set { nature = value; }
        }
        public int UserId
        {
            get { return userId; }
            set { userId = value; }
        }
        public int CompanyMasterId
        {
            get { return companyMasterId; }
            set { companyMasterId = value; }
        }
    }
}
