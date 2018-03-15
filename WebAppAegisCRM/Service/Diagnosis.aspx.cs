using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebAppAegisCRM.Service
{
    public partial class Diagnosis : System.Web.UI.Page
    {
        public int Id
        {
            get { return Convert.ToInt32(ViewState["Id"]); }
            set { ViewState["Id"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ClearControls();
                LoadDiagonosis();
            }
        }

        protected void ClearControls()
        {
            Id = 0;
            txtName.Text = "";
            Message.Show = false;
        }

        protected void LoadDiagonosis()
        {
            Business.Service.ProblemMasters objProblemObserved = new Business.Service.ProblemMasters();
            gvDiagonosis.DataSource = objProblemObserved.GetAll(2);
            gvDiagonosis.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Business.Service.ProblemMasters objProblemMasters = new Business.Service.ProblemMasters();
            Entity.Service.ProblemMasters problemMasters = new Entity.Service.ProblemMasters();

            problemMasters.Id = Id;
            problemMasters.Name = txtName.Text;
            problemMasters.Type = 2; //1 for Diagnosis master data

            int i = objProblemMasters.Save(problemMasters);

            if (i > 0)
            {
                ClearControls();
                LoadDiagonosis();
                Message.IsSuccess = true;
                Message.Text = "Diagnosis saved successfully...";
            }
            else
            {
                Message.IsSuccess = false;
                Message.Text = "Can not save!!!";
            }
            Message.Show = true;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearControls();
        }

        protected void gvDiagonosis_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDiagonosis.PageIndex = e.NewPageIndex;
            LoadDiagonosis();
        }

        protected void gvDiagonosis_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Business.Service.ProblemMasters objProblemMasters = new Business.Service.ProblemMasters();
            Entity.Service.ProblemMasters problemMasters = new Entity.Service.ProblemMasters();

            if (e.CommandName == "Ed")
            {
                Id = int.Parse(e.CommandArgument.ToString());
                problemMasters = objProblemMasters.GetById(Id);

                txtName.Text = problemMasters.Name;
            }
            else if (e.CommandName == "Del")
            {
                int i = objProblemMasters.Delete(int.Parse(e.CommandArgument.ToString()));
                if (i > 0)
                {
                    ClearControls();
                    LoadDiagonosis();
                    Message.IsSuccess = true;
                    Message.Text = "Diagnosis deleted successfully...";
                }
                else
                {
                    Message.IsSuccess = false;
                    Message.Text = "Sorry, can not delete!";
                }
                Message.Show = true;
            }
        }
    }
}