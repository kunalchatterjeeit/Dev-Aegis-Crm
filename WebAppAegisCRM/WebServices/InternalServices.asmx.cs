using Business.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace WebAppAegisCRM.WebServices
{
    /// <summary>
    /// Summary description for InternalServices
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class InternalServices : System.Web.Services.WebService
    {

        [WebMethod]
        public List<string> LoadAutoCompleteItems(string searchContent)
        {
            List<string> result = new List<string>();

            DataTable dtAssets = GlobalCache.ExecuteCache<DataTable>(typeof(Business.Inventory.Inventory), "Inventory_GetAll", DateTime.MinValue, DateTime.MinValue);//Business.Inventory.Inventory.Inventory_GetAll(DateTime.MinValue, DateTime.MinValue);
            using (DataView dvAssets = new DataView(dtAssets))
            {
                dvAssets.RowFilter = "AssetId LIKE '%" + searchContent + "%'";
                dtAssets = dvAssets.ToTable();
            }
            result = dtAssets.AsEnumerable().Select(x => x[1].ToString()).ToList();
            return result;
        }
    }
}
