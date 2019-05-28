using Business.Common;
using Entity.HR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebAppAegisCRM.HR
{
    public partial class Claim : System.Web.UI.Page
    {
        private DataTable _ClaimDetails
        {
            get
            {
                return Business.Common.Context.ClaimDetails;
            }

            set { Business.Common.Context.ClaimDetails = value; }
        }

        private string _ClaimNo
        {
            get
            {
                return DateTime.Now.Ticks.ToString();
            }
        }
        private string _AttachmentName
        {
            get
            {
                return DateTime.Now.Ticks.ToString();
            }
        }

        private void ClearAllControl()
        {
            txtExpenseDate.Text = DateTime.Now.ToString("dd MMM yyyy");
            txtPeriodFrom.Text = DateTime.Now.ToString("dd MMM yyyy");
            txtPeriodTo.Text = DateTime.Now.ToString("dd MMM yyyy");
            ddlCategory.SelectedIndex = 0;
            txtCost.Text = "0";
            Message.Show = false;
            _ClaimDetails.Clear();
            LoadClaimDetails();
        }

        private void LoadClaimCategory()
        {
            ddlCategory.DataSource = Enum.GetValues(typeof(ClaimCategoryEnum));
            ddlCategory.DataBind();
            ddlCategory.InsertSelect();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadClaimCategory();
                ClearAllControl();
            }
        }

        protected void gvClaimDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "D")
            {
                string autoId = e.CommandArgument.ToString();

                if (DeleteItem(autoId))
                {
                    LoadClaimDetails();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "mmsg", "alert('Data can not be deleted!!!....');", true);
                }

            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Entity.HR.Claim claim = new Entity.HR.Claim()
            {
                ClaimDateTime = DateTime.Now,
                ClaimNo = _ClaimNo,
                ClaimStatus = ClaimStatusEnum.Pending,
                CreatedBy = int.Parse(HttpContext.Current.User.Identity.Name),
                EmployeeId = int.Parse(HttpContext.Current.User.Identity.Name),
                PeriodFrom = Convert.ToDateTime(txtPeriodFrom.Text.Trim()),
                PeriodTo = Convert.ToDateTime(txtPeriodTo.Text.Trim()),
                TotalAmount = 0
            };

            claim.ClaimId = new Business.HR.Claim().Claim_Save(claim);
            ClearAllControl();
            Message.IsSuccess = true;
            Message.Text = string.Format("Claim submitted successfully. YouR claim no is {0}.", claim.ClaimNo);
            Message.Show = true;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearAllControl();
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearDetailsControls();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (_ClaimDetails.Rows.Count == 0)
            {
                using (DataTable dtInstance = new DataTable())
                {
                    DataColumn column = new DataColumn("AutoId");
                    column.AutoIncrement = true;
                    column.ReadOnly = true;
                    column.Unique = false;

                    dtInstance.Columns.Add(column);
                    dtInstance.Columns.Add("ExpenseDate");
                    dtInstance.Columns.Add("CategoryName");
                    dtInstance.Columns.Add("CategoryId");
                    dtInstance.Columns.Add("Status");
                    dtInstance.Columns.Add("Cost");
                    dtInstance.Columns.Add("Attachment");
                    _ClaimDetails = dtInstance;
                }
            }

            DataRow drItem = _ClaimDetails.NewRow();
            drItem["ExpenseDate"] = txtExpenseDate.Text;
            drItem["CategoryName"] = ddlCategory.SelectedItem;
            drItem["CategoryId"] = ddlCategory.SelectedValue;
            drItem["Status"] = ClaimStatusEnum.Pending.ToString();
            drItem["Cost"] = txtCost.Text;
            drItem["Attachment"] = SaveAttachment();

            _ClaimDetails.Rows.Add(drItem);
            _ClaimDetails.AcceptChanges();

            LoadClaimDetails();
            ClearDetailsControls();
        }

        private void ClearDetailsControls()
        {
            txtExpenseDate.Text = DateTime.Now.ToString("dd MMM yyyy");
            ddlCategory.SelectedIndex = 0;
            txtCost.Text = "0";
        }

        private string SaveAttachment()
        {
            string retValue = string.Empty;
            if (fuAttachment.HasFile)
            {
                retValue = string.Format("{0}{1}", _AttachmentName, System.IO.Path.GetExtension(fuAttachment.FileName));
                fuAttachment.PostedFile.SaveAs(Server.MapPath(" ") + "\\ClaimAttachments\\" + retValue);
            }
            return retValue;
        }

        private void LoadClaimDetails()
        {
            gvClaimDetails.DataSource = _ClaimDetails;
            gvClaimDetails.DataBind();
        }

        private bool DeleteItem(string autoId)
        {
            bool retValue = false;
            int lastCount = 0;
            if (_ClaimDetails.Rows.Count > 0)
            {
                lastCount = _ClaimDetails.Rows.Count;
                _ClaimDetails.Rows[_ClaimDetails.Rows.IndexOf(_ClaimDetails.Select("AutoId='" + autoId + "'").FirstOrDefault())].Delete();
                _ClaimDetails.AcceptChanges();
            }
            if (lastCount > _ClaimDetails.Rows.Count)
            {
                retValue = true;
            }
            return retValue;
        }
    }
}