using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebAppAegisCRM.Service
{
    public partial class ProblemObserved : System.Web.UI.Page
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
                LoadProblemObserved();
            }
        }

        protected void ClearControls()
        {
            Id = 0;
            txtName.Text = "";
            Message.Show = false;
        }

        protected void LoadProblemObserved()
        {
            Business.Service.ProblemMasters objProblemObserved = new Business.Service.ProblemMasters();
            gvProblemObserved.DataSource = objProblemObserved.GetAll(1);
            gvProblemObserved.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Business.Service.ProblemMasters objProblemObserved = new Business.Service.ProblemMasters();
            Entity.Service.ProblemMasters problemObserved = new Entity.Service.ProblemMasters();

            problemObserved.Id = Id;
            problemObserved.Name = txtName.Text;
            problemObserved.Type = 1; //1 for Problem Observed master data

            int i = objProblemObserved.Save(problemObserved);

            if (i > 0)
            {
                ClearControls();
                LoadProblemObserved();
                Message.IsSuccess = true;
                Message.Text = "Problem observed saved successfully...";
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

        protected void gvProblemObserved_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvProblemObserved.PageIndex = e.NewPageIndex;
            LoadProblemObserved();
        }

        protected void gvProblemObserved_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Business.Service.ProblemMasters objProblemObserved = new Business.Service.ProblemMasters();
            Entity.Service.ProblemMasters problemObserved = new Entity.Service.ProblemMasters();

            if (e.CommandName == "Ed")
            {
                Id = int.Parse(e.CommandArgument.ToString());
                problemObserved = objProblemObserved.GetById(Id);

                txtName.Text = problemObserved.Name;
            }
            else if (e.CommandName == "Del")
            {
                int i = objProblemObserved.Delete(int.Parse(e.CommandArgument.ToString()));
                if (i > 0)
                {
                    ClearControls();
                    LoadProblemObserved();
                    Message.IsSuccess = true;
                    Message.Text = "Problem observed deleted successfully...";
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