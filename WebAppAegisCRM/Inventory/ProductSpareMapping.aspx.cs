﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace WebAppAegisCRM.Inventory
{
    public partial class ProductSpareMapping : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadProductMaster();
                LoadSpare();
                Message.Show = false;
            }
        }

        protected void LoadProductMaster()
        {
            Business.Inventory.ProductMaster objProductMaster = new Business.Inventory.ProductMaster();
            Entity.Inventory.ProductMaster productMaster = new Entity.Inventory.ProductMaster();

            productMaster.CompanyMasterId = 1;
            DataTable dt = objProductMaster.GetAll(productMaster);
            if (dt != null)
            {
                ddlProduct.DataSource = dt;
                ddlProduct.DataTextField = "ProductName";
                ddlProduct.DataValueField = "ProductMasterId";
                ddlProduct.DataBind();
            }
            ddlProduct.Items.Insert(0, "--SELECT--");
        }

        protected void LoadSpare()
        {
            Business.Inventory.SpareMaster objSpareMaster = new Business.Inventory.SpareMaster();
            Entity.Inventory.SpareMaster spareMaster = new Entity.Inventory.SpareMaster();
            DataTable dt = objSpareMaster.GetAll(spareMaster);
            if (dt != null)
            {
                gvSpare.DataSource = dt;
                gvSpare.DataBind();
            }
        }

        protected void LoadProductSpareMapping()
        {
            LoadSpare();
            Business.Inventory.ProductMaster objProductMaster = new Business.Inventory.ProductMaster();
            DataTable dt = objProductMaster.ProductSpareMapping_GetById(long.Parse(ddlProduct.SelectedValue), Entity.Inventory.ItemType.None);
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    foreach (GridViewRow gvr in gvSpare.Rows)
                    {
                        if (dr["SpareId"].ToString() == gvSpare.DataKeys[gvr.RowIndex].Values[0].ToString())
                        {
                            CheckBox chkMap = (CheckBox)gvr.FindControl("chkMap");
                            chkMap.Checked = true;
                        }
                    }
                }
            }
        }

        protected void btnShow_Click(object sender, EventArgs e)
        {
            LoadProductSpareMapping();
        }

        protected void btnMap_Click(object sender, EventArgs e)
        {
            Business.Inventory.ProductMaster objProductMaster = new Business.Inventory.ProductMaster();
            Entity.Inventory.ProductMaster productMaster = new Entity.Inventory.ProductMaster();

            using (DataTable dt = new DataTable())
            {
                dt.Columns.Add("SpareId");
                foreach (GridViewRow gvr in gvSpare.Rows)
                {
                    CheckBox chkMap = (CheckBox)gvr.FindControl("chkMap");
                    if (chkMap.Checked)
                    {
                        dt.Rows.Add();
                        dt.Rows[dt.Rows.Count - 1]["SpareId"] = gvSpare.DataKeys[gvr.RowIndex].Values[0].ToString();
                        dt.AcceptChanges();
                    }
                }

                productMaster.ProductMasterId = int.Parse(ddlProduct.SelectedValue);
                productMaster.dtMapping = dt;

                int i = objProductMaster.ProductSpareMapping_Save(productMaster);

                if (i > 0)
                {
                    Message.IsSuccess = true;
                    Message.Text = "Product Spare Mapping Saved...";
                }
                else
                {
                    Message.IsSuccess = false;
                    Message.Text = "Mapping can not save!";
                }
                Message.Show = true;
            }
        }
    }
}