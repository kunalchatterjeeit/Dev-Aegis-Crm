using Business.Common;

using System;
using System.Web.UI.WebControls;

namespace WebAppAegisCRM.Common
{
    public partial class City : System.Web.UI.Page
    {
        
        public int CityId
        {
            get { return Convert.ToInt32(ViewState["CityId"]); }
            set { ViewState["CityId"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    ClearControls();
                    LoadCity();
                }
                catch (Exception ex)
                {
                    ex.WriteException();
                    
                }
            }
        }

        private void ClearControls()
        {
            CityId = 0;
            ddlState.SelectedIndex = 0;
            ddlDistrict.SelectedIndex = 0;
            txtCity.Text = "";
            txtSTD.Text = "";
            Message.Show = false;
        }

        private void LoadCity()
        {
            Business.Common.City objCity = new Business.Common.City();
            Entity.Common.City city = new Entity.Common.City();

            city.CityName = string.Empty;
            city.STD = string.Empty;
            gvCity.DataSource = objCity.GetAll(city);
            gvCity.DataBind();

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Business.Common.City objCity = new Business.Common.City();
                Entity.Common.City city = new Entity.Common.City();

                city.CityId = CityId;
                city.CountryId = 1;
                city.StateId = 1;
                city.DistrictId = 1;
                city.CityName = txtCity.Text;
                city.STD = txtSTD.Text;

                int i = objCity.Save(city);

                if (i > 0)
                {
                    ClearControls();
                    LoadCity();
                    Message.IsSuccess = true;
                    Message.Text = "City saved successfully...";
                }
                else
                {
                    Message.IsSuccess = false;
                    Message.Text = "Can not save!!!";
                }
                Message.Show = true;
            }
            catch (Exception ex)
            {
                ex.WriteException();
                
            }
        }

        protected void gvCity_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                Business.Common.City objCity = new Business.Common.City();
                Entity.Common.City city = new Entity.Common.City();

                if (e.CommandName == "Ed")
                {
                    int cityId = int.Parse(e.CommandArgument.ToString());
                    city = objCity.GetById(cityId);
                    CityId = city.CityId;
                    txtCity.Text = city.CityName;
                    txtSTD.Text = city.STD;
                    ddlState.SelectedValue = Convert.ToString(city.StateId);
                    ddlDistrict.SelectedValue = Convert.ToString(city.DistrictId);
                }
                else if (e.CommandName == "Del")
                {
                    int cityId = int.Parse(e.CommandArgument.ToString());
                    int i = objCity.Delete(cityId);

                    if (i > 0)
                    {
                        ClearControls();
                        LoadCity();
                        Message.IsSuccess = true;
                        Message.Text = "City deleted successfully...";
                    }
                    else
                    {
                        Message.IsSuccess = false;
                        Message.Text = "Can not delete!!!";
                    }
                    Message.Show = true;
                }
            }
            catch (Exception ex)
            {
                ex.WriteException();
                
            }
        }

        protected void gvCity_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvCity.PageIndex = e.NewPageIndex;
                LoadCity();
            }
            catch (Exception ex)
            {
                ex.WriteException();
                
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
                
            }
        }
    }
}