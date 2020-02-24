using Business.Common;
using log4net;
using System;
using System.Web.UI.WebControls;

namespace WebAppAegisCRM.Sales
{
    public partial class MeetingType : System.Web.UI.Page
    {
        ILog logger = log4net.LogManager.GetLogger("ErrorLog");
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    LoadMeetingTypeList();
                    Message.Show = false;
                    if (MeetingTypeId > 0)
                    {
                        GetMeetingTypeById();
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
        public int MeetingTypeId
        {
            get { return Convert.ToInt32(ViewState["Id"]); }
            set { ViewState["Id"] = value; }
        }
        private void LoadMeetingTypeList()
        {
            Business.Sales.MeetingType Obj = new Business.Sales.MeetingType();
            gvMeetingType.DataSource = Obj.GetAllMeetingType();
            gvMeetingType.DataBind();
        }
        private void ClearControls()
        {
            MeetingTypeId = 0;
            Message.Show = false;
            txtDescription.Text = string.Empty;
            txtName.Text = string.Empty;
            btnSave.Text = "Save";
        }
        private bool CallStatusControlValidation()
        {
            if (txtName.Text.Trim() == string.Empty)
            {
                Message.IsSuccess = false;
                Message.Text = "Please Enter Meeting Type Name";
                Message.Show = true;
                return false;
            }
            return true;
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
        protected void gvMeetingType_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Ed")
                {
                    MeetingTypeId = Convert.ToInt32(e.CommandArgument.ToString());
                    GetMeetingTypeById();
                    Message.Show = false;
                    btnSave.Text = "Update";
                }
                else if (e.CommandName == "Del")
                {
                    Business.Sales.MeetingType Obj = new Business.Sales.MeetingType();
                    int rows = Obj.DeleteMeetingType(Convert.ToInt32(e.CommandArgument.ToString()));
                    if (rows > 0)
                    {
                        ClearControls();
                        LoadMeetingTypeList();
                        Message.IsSuccess = true;
                        Message.Text = "Deleted Successfully";
                    }
                    else
                    {
                        Message.IsSuccess = false;
                        Message.Text = "Data Dependency Exists";
                    }
                    Message.Show = true;
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
        private void GetMeetingTypeById()
        {
            Business.Sales.MeetingType Obj = new Business.Sales.MeetingType();
            Entity.Sales.MeetingType meetingType = Obj.GetMeetingTypeById(MeetingTypeId);
            if (meetingType.Id != 0)
            {
                txtDescription.Text = meetingType.Description;
                txtName.Text = meetingType.Name;
            }
        }
        private void Save()
        {
            if (CallStatusControlValidation())
            {
                Business.Sales.MeetingType Obj = new Business.Sales.MeetingType();
                Entity.Sales.MeetingType Model = new Entity.Sales.MeetingType
                {
                    Id = MeetingTypeId,
                    Name = txtName.Text,
                    Description = txtDescription.Text,

                };
                int rows = Obj.SaveMeetingType(Model);
                if (rows > 0)
                {
                    ClearControls();
                    LoadMeetingTypeList();
                    MeetingTypeId = 0;
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
    }
}