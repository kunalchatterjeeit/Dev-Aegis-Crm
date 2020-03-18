using Business.Common;
using Entity.Inventory;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;
using System.Reflection;
using Entity.Common;
using log4net;

namespace WebAppAegisCRM.Sales
{
    public partial class Quote : System.Web.UI.Page
    {
        ILog logger = log4net.LogManager.GetLogger("ErrorLog");
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    // Business.Common.Context.ReferralUrl = HttpContext.Current.Request.UrlReferrer.AbsoluteUri;
                    LoadQuoteList();
                    LoadQuoteDropdowns();
                    LoadAllItem();
                    Message.Show = false;
                    if (QuoteId > 0)
                    {
                        GetQuoteById();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.WriteException();
                logger.Error(ex.Message);
                Message.IsSuccess = false;
                Message.Text = ex.Message;
                Message.Show = true;
            }
        }
        public int QuoteId
        {
            get { return Convert.ToInt32(ViewState["Id"]); }
            set { ViewState["Id"] = value; }
        }
        private void LoadQuoteList()
        {
            Business.Sales.Quote Obj = new Business.Sales.Quote();
            Entity.Sales.GetQuoteParam Param = new Entity.Sales.GetQuoteParam
            {
                QuoteNumber = null
            };
            gvQuote.DataSource = Obj.GetAllQuotes(Param);
            gvQuote.DataBind();
        }
        private DataTable _ItemsList
        {
            get
            {
                return (Session["ItemsList"] != null)
                    ? (DataTable)Session["ItemsList"]
                    : new DataTable();
            }

            set { Session["ItemsList"] = value; }
        }
        private void LoadQuoteDropdowns()
        {
            Business.Sales.Opportunity Obj = new Business.Sales.Opportunity();
            Business.Sales.Quote ObjQuote = new Business.Sales.Quote();
            Entity.Sales.GetOpportunityParam Param = new Entity.Sales.GetOpportunityParam
            {
                BestPrice = null,
                CommitStageId = null,
                Name = null,
                SourceActivityTypeId = Convert.ToInt32(ActityType.Lead),
                ChildActivityTypeId = Convert.ToInt32(ActityType.Opportunity)
            };
            ddlOpportunity.DataSource = Obj.GetAllOpportunity(Param);
            ddlOpportunity.DataTextField = "Name";
            ddlOpportunity.DataValueField = "Id";
            ddlOpportunity.DataBind();
            ddlOpportunity.InsertSelect();

            ddlPaymentTerm.DataSource = ObjQuote.GetPaymentTerm();
            ddlPaymentTerm.DataTextField = "Name";
            ddlPaymentTerm.DataValueField = "Id";
            ddlPaymentTerm.DataBind();
            ddlPaymentTerm.InsertSelect();

            ddlQuoteStage.DataSource = ObjQuote.GetQuoteStage();
            ddlQuoteStage.DataTextField = "Name";
            ddlQuoteStage.DataValueField = "Id";
            ddlQuoteStage.DataBind();
            ddlQuoteStage.InsertSelect();
        }
        private void LoadAllItem()
        {
            using (DataTable dtItem = new DataTable())
            {
                dtItem.Columns.Add("ItemIdType");
                dtItem.Columns.Add("ItemName");

                Business.Inventory.ProductMaster objProductMaster = new Business.Inventory.ProductMaster();

                foreach (DataRow drItem in objProductMaster.GetAll(new Entity.Inventory.ProductMaster() { CompanyMasterId = 1 }).Rows)
                {
                    DataRow drNewItem = dtItem.NewRow();
                    drNewItem["ItemIdType"] = drItem["ProductMasterId"].ToString() + "|" + (int)ItemType.Product;
                    drNewItem["ItemName"] = drItem["ProductName"].ToString() + " (P)";
                    dtItem.Rows.Add(drNewItem);
                    dtItem.AcceptChanges();
                }

                Business.Inventory.SpareMaster objSpareMaster = new Business.Inventory.SpareMaster();

                foreach (DataRow drItem in objSpareMaster.GetAll(new Entity.Inventory.SpareMaster() { }).Rows)
                {
                    DataRow drNewItem = dtItem.NewRow();
                    drNewItem["ItemIdType"] = drItem["SpareId"].ToString() + "|" + (int)ItemType.Spare;
                    drNewItem["ItemName"] = drItem["SpareName"].ToString() + " (S)";
                    dtItem.Rows.Add(drNewItem);
                    dtItem.AcceptChanges();
                }

                ddlItem.DataSource = dtItem;
                ddlItem.DataValueField = "ItemIdType";
                ddlItem.DataTextField = "ItemName";
                ddlItem.DataBind();
                ddlItem.InsertSelect();
            }
        }
        private void ClearControls()
        {
            QuoteId = 0;
            Message.Show = false;
            txtActualCloseDate.Text = string.Empty;
            txtCurrencyCode.Text = string.Empty;
            txtCurrencyName.Text = string.Empty;
            txtDateShipped.Text = string.Empty;
            txtOriginalPODate.Text = string.Empty;
            txtPurchaseOrderNo.Text = string.Empty;
            txtQuoteNumber.Text = string.Empty;
            txtShippingProvider.Text = string.Empty;
            txtTaxRate.Text = string.Empty;
            txtValidTillDate.Text = string.Empty;
            ddlOpportunity.SelectedIndex = 0;
            ddlPaymentTerm.SelectedIndex = 0;
            ddlQuoteStage.SelectedIndex = 0;
            btnSave.Text = "Save";
        }
        private void ClearItemControls()
        {
            ddlItem.SelectedIndex = 0;
            txtDiscount.Text = string.Empty;
            txtPartnumber.Text = string.Empty;
            txtRate.Text = string.Empty;
            txtQuantity.Text = string.Empty;
            //_ItemsList = null;
            ddlItem.Focus();
        }
        private bool QuoteControlValidation()
        {
            if (txtQuoteNumber.Text.Trim() == string.Empty)
            {
                Message.IsSuccess = false;
                Message.Text = "Please Enter Opportunity Name";
                Message.Show = true;
                return false;
            }
            if (ddlQuoteStage.SelectedIndex == 0)
            {
                Message.IsSuccess = false;
                Message.Text = "Please Select Quote Stage";
                Message.Show = true;
                return false;
            }
            return true;
        }
        private void LoadItemList()
        {
            gvItem.DataSource = _ItemsList;
            gvItem.DataBind();
        }
        private bool DeleteItem(string itemIdType)
        {
            bool retValue = false;
            int lastCount = 0;
            if (_ItemsList.Rows.Count > 0)
            {
                lastCount = _ItemsList.Rows.Count;
                _ItemsList.Rows[_ItemsList.Rows.IndexOf(_ItemsList.Select("ItemId='" + itemIdType + "'").FirstOrDefault())].Delete();
                _ItemsList.AcceptChanges();
            }
            if (lastCount > _ItemsList.Rows.Count)
            {
                retValue = true;
            }
            return retValue;
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (_ItemsList.Rows.Count == 0)
                {
                    using (DataTable dtInstance = new DataTable())
                    {
                        dtInstance.Columns.Add("ItemId");
                        dtInstance.Columns.Add("ItemName");
                        dtInstance.Columns.Add("PartNumber");
                        dtInstance.Columns.Add("UnitPrice");
                        dtInstance.Columns.Add("Discount");
                        dtInstance.Columns.Add("Quantity");
                        dtInstance.Columns.Add("IsActive");
                        _ItemsList = dtInstance;
                    }
                }

                DataRow drItem = _ItemsList.NewRow();
                drItem["ItemId"] = ddlItem.SelectedValue;
                drItem["ItemName"] = ddlItem.SelectedItem.Text;
                drItem["PartNumber"] = txtPartnumber.Text.Trim();
                drItem["UnitPrice"] = txtRate.Text.Trim();
                drItem["Discount"] = txtDiscount.Text.Trim();
                drItem["Quantity"] = txtQuantity.Text.Trim();
                drItem["IsActive"] = true;
                _ItemsList.Rows.Add(drItem);
                _ItemsList.AcceptChanges();

                LoadItemList();
                ClearItemControls();
            }
            catch (Exception ex)
            {
                ex.WriteException();
                logger.Error(ex.Message);
                Message.IsSuccess = false;
                Message.Text = ex.Message;
                Message.Show = true;
            }
        }
        protected void gvItem_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "D")
                {
                    string itemIdType = e.CommandArgument.ToString();

                    if (DeleteItem(itemIdType))
                    {
                        LoadItemList();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "mmsg", "alert('Data can not be deleted!!!....');", true);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.WriteException();
                logger.Error(ex.Message);
                Message.IsSuccess = false;
                Message.Text = ex.Message;
                Message.Show = true;
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                ClearControls();
            }
            catch (Exception ex)
            {
                ex.WriteException();
                logger.Error(ex.Message);
                Message.IsSuccess = false;
                Message.Text = ex.Message;
                Message.Show = true;
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Save();
            }
            catch (Exception ex)
            {
                ex.WriteException();
                logger.Error(ex.Message);
                Message.IsSuccess = false;
                Message.Text = ex.Message;
                Message.Show = true;
            }
        }
        protected void gvQuote_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Ed")
                {
                    QuoteId = Convert.ToInt32(e.CommandArgument.ToString());
                    GetQuoteById();
                    Message.Show = false;
                    btnSave.Text = "Update";
                }
                else if (e.CommandName == "View")
                {
                    QuoteId = Convert.ToInt32(e.CommandArgument.ToString());
                    GetQuoteById();
                }
                else if (e.CommandName == "Print")
                {
                    QuoteId = Convert.ToInt32(e.CommandArgument.ToString());
                    Response.Redirect("QuoteBeforePrint.aspx?QuoteId=" + QuoteId + "");
                }
            }
            catch (Exception ex)
            {
                ex.WriteException();
                logger.Error(ex.Message);
                Message.IsSuccess = false;
                Message.Text = ex.Message;
                Message.Show = true;
            }
        }
        private void Save()
        {
            if (QuoteControlValidation())
            {
                List<Entity.Sales.QuoteLineItem> lineItem = new List<Entity.Sales.QuoteLineItem>();
                foreach (DataRow drItem in _ItemsList.Rows)
                {
                    lineItem.Add(new Entity.Sales.QuoteLineItem
                    {
                        ItemId = Convert.ToInt32(drItem["ItemId"].ToString().Split('|')[0]),
                        Discount = (!string.IsNullOrEmpty(drItem["Discount"].ToString())) ? Convert.ToDecimal(drItem["Discount"].ToString()) : 0,
                        Quantity = (!string.IsNullOrEmpty(drItem["Quantity"].ToString())) ? Convert.ToDecimal(drItem["Quantity"].ToString()) : 0,
                        PartNumber = drItem["PartNumber"].ToString(),
                        ItemName = drItem["ItemName"].ToString(),
                        IsActive = true,
                        UnitPrice = (!string.IsNullOrEmpty(drItem["UnitPrice"].ToString())) ? Convert.ToDecimal(drItem["UnitPrice"].ToString()) : 0
                    });

                }
                JavaScriptSerializer jss = new JavaScriptSerializer();
                string items = jss.Serialize(lineItem);
                Business.Sales.Quote Obj = new Business.Sales.Quote();
                Entity.Sales.Quote Model = new Entity.Sales.Quote
                {
                    Id = QuoteId,
                    CreatedBy = Convert.ToInt32(HttpContext.Current.User.Identity.Name),
                    QuoteNumber = txtQuoteNumber.Text,
                    PurchaseOrderNo = txtPurchaseOrderNo.Text,
                    ShippingProvider = txtShippingProvider.Text,
                    CurrencyCode = txtCurrencyCode.Text,
                    CurrencyName = txtCurrencyName.Text,
                    OpportunityId = ddlOpportunity.SelectedValue == "" ? (int?)null : Convert.ToInt32(ddlOpportunity.SelectedValue),
                    PaymentTermId = ddlPaymentTerm.SelectedValue == "" ? (int?)null : Convert.ToInt32(ddlPaymentTerm.SelectedValue),
                    QuoteStageId = ddlQuoteStage.SelectedValue == "" ? (int?)null : Convert.ToInt32(ddlQuoteStage.SelectedValue),
                    OriginalPODate = txtOriginalPODate.Text == "" ? (DateTime?)null : Convert.ToDateTime(txtOriginalPODate.Text),
                    ActualCloseDate = txtActualCloseDate.Text == "" ? (DateTime?)null : Convert.ToDateTime(txtActualCloseDate.Text),
                    DateShipped = txtDateShipped.Text == "" ? (DateTime?)null : Convert.ToDateTime(txtDateShipped.Text),
                    ValidTillDate = txtValidTillDate.Text == "" ? (DateTime?)null : Convert.ToDateTime(txtValidTillDate.Text),
                    IsActiveQuote = true,
                    IsActiveQuoteHistory = true,
                    IsActiveQuoteSettings = true,
                    TaxRate = txtTaxRate.Text == "" ? (decimal?)null : Convert.ToDecimal(txtTaxRate.Text),
                    QuoteLineItem = items
                };
                Model.QuoteJSON = jss.Serialize(Model);
                int rows = Obj.SaveQuote(Model);
                if (rows > 0)
                {
                    ClearControls();
                    LoadQuoteList();
                    QuoteId = 0;
                    Message.IsSuccess = true;
                    Message.Text = "Saved Successfully";
                }
                else
                {
                    Message.IsSuccess = false;
                    Message.Text = "Unable to save data.";
                }
                Message.Show = true;
            }
        }
        private void GetQuoteById()
        {
            Business.Sales.Quote Obj = new Business.Sales.Quote();
            Entity.Sales.Quote Quote = Obj.GetQuoteById(QuoteId);
            if (Quote.Id != 0)
            {
                txtActualCloseDate.Text = Quote.ActualCloseDate == null ? string.Empty : Quote.ActualCloseDate.GetValueOrDefault().ToString("dd MMM yyyy");
                txtDateShipped.Text = Quote.DateShipped == null ? string.Empty : Quote.DateShipped.GetValueOrDefault().ToString("dd MMM yyyy");
                txtValidTillDate.Text = Quote.ValidTillDate == null ? string.Empty : Quote.ValidTillDate.GetValueOrDefault().ToString("dd MMM yyyy");
                txtOriginalPODate.Text = Quote.OriginalPODate == null ? string.Empty : Quote.OriginalPODate.GetValueOrDefault().ToString("dd MMM yyyy");
                txtQuoteNumber.Text = Quote.QuoteNumber;
                txtPurchaseOrderNo.Text = Quote.PurchaseOrderNo;
                txtShippingProvider.Text = Quote.ShippingProvider;
                txtCurrencyCode.Text = Quote.CurrencyCode;
                txtCurrencyName.Text = Quote.CurrencyName;
                txtTaxRate.Text = Quote.TaxRate == null ? string.Empty : Quote.TaxRate.ToString();
                ddlOpportunity.SelectedValue = Quote.OpportunityId == null ? "0" : Quote.OpportunityId.ToString();
                ddlPaymentTerm.SelectedValue = Quote.PaymentTermId == null ? "0" : Quote.PaymentTermId.ToString();
                ddlQuoteStage.SelectedValue = Quote.QuoteStageId == null ? "0" : Quote.QuoteStageId.ToString();
                JavaScriptSerializer ser = new JavaScriptSerializer();
                List<Entity.Sales.QuoteLineItem> lst = ser.Deserialize<List<Entity.Sales.QuoteLineItem>>(Quote.QuoteLineItem);
                ListtoDataTable lsttodt = new ListtoDataTable();
                _ItemsList = lsttodt.ToDataTable(lst);
                LoadItemList();
            }
        }
        public class ListtoDataTable
        {
            public DataTable ToDataTable<T>(List<T> items)
            {
                DataTable dataTable = new DataTable(typeof(T).Name);
                //Get all the properties by using reflection   
                PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach (PropertyInfo prop in Props)
                {
                    //Setting column names as Property names  
                    dataTable.Columns.Add(prop.Name);
                }
                foreach (T item in items)
                {
                    var values = new object[Props.Length];
                    for (int i = 0; i < Props.Length; i++)
                    {

                        values[i] = Props[i].GetValue(item, null);
                    }
                    dataTable.Rows.Add(values);
                }

                return dataTable;
            }
        }
    }
}