using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using DataAccess.Common;

namespace DataAccess.Service
{
    public class Docket
    {
        public Docket()
        { }

        public static DataTable Service_Docket_Save(Entity.Service.Docket docket)
        {
            using (DataTable dt = new DataTable())
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandText = "usp_Service_Docket_Save";
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@DocketId", docket.DocketId);
                        cmd.Parameters.AddWithValue("@CustomerPurchaseId", docket.CustomerPurchaseId);
                        cmd.Parameters.AddWithValue("@DocketDateTime", docket.DocketDateTime);
                        cmd.Parameters.AddWithValue("@DocketType", docket.DocketType);
                        cmd.Parameters.AddWithValue("@Problem", docket.Problem);
                        cmd.Parameters.AddWithValue("@IsCustomerEntry", docket.isCustomerEntry);
                        cmd.Parameters.AddWithValue("@CallStatusId", docket.CallStatusId);
                        cmd.Parameters.AddWithValue("@CreatedBy", docket.CreatedBy);

                        if (con.State == ConnectionState.Closed)
                            con.Open();
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                        con.Close();
                    }
                }
                return dt;
            }
        }


        public static DataTable Service_Docket_GetAll(Entity.Service.Docket docket)
        {
            using (DataTable dt = new DataTable())
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        if (docket.DocketId == 0)
                            cmd.Parameters.AddWithValue("@DocketId", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@DocketId", docket.DocketId);
                        if (docket.DocketNo == "")
                            cmd.Parameters.AddWithValue("@DocketNo", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@DocketNo", docket.DocketNo);
                        if (docket.CustomerId == 0)
                            cmd.Parameters.AddWithValue("@CustomerId_FK", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@CustomerId_FK", docket.CustomerId);
                        if (docket.ProductId == 0)
                            cmd.Parameters.AddWithValue("@ProductId_FK", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@ProductId_FK", docket.ProductId);
                        if (docket.DocketFromDateTime == DateTime.MinValue)
                            cmd.Parameters.AddWithValue("@DocketFromDateTime", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@DocketFromDateTime", docket.DocketFromDateTime);
                        if (docket.DocketToDateTime == DateTime.MinValue)
                            cmd.Parameters.AddWithValue("@DocketToDateTime", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@DocketToDateTime", docket.DocketToDateTime);
                        if (docket.CallStatusId == 0)
                            cmd.Parameters.AddWithValue("@CallStatusId", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@CallStatusId", docket.CallStatusId);
                        if (docket.DocketType == "")
                            cmd.Parameters.AddWithValue("@DocketType", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@DocketType", docket.DocketType);
                        if (docket.AssignEngineer == 0)
                            cmd.Parameters.AddWithValue("@AssignEngineer", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@AssignEngineer", docket.AssignEngineer);

                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "usp_Service_Docket_GetAll";
                        if (con.State == ConnectionState.Closed)
                            con.Open();
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                        con.Close();
                    }
                }
                return dt;
            }
        }

        public static DataTable Service_Docket_GetLast10()
        {
            using (DataTable dt = new DataTable())
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "usp_Service_Docket_GetLast10";
                        if (con.State == ConnectionState.Closed)
                            con.Open();
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                        con.Close();
                    }
                }
                return dt;
            }
        }

        public static Entity.Service.Docket Service_Docket_GetByDocketId(int docketid)
        {
            Entity.Service.Docket docket = new Entity.Service.Docket();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Parameters.AddWithValue("@DocketId", docketid);

                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "usp_Service_Docket_GetByDocketId";
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        if (dr.HasRows)
                        {
                            docket.DocketId = docketid;
                            docket.DocketNo = dr["DocketNo"].ToString();
                            docket.CustomerId = (dr["CustomerId_FK"] == DBNull.Value) ? 0 : int.Parse(dr["CustomerId_FK"].ToString());
                            docket.ProductId = int.Parse(dr["ProductId_FK"].ToString());
                            docket.DocketDateTime = Convert.ToDateTime(dr["DocketDateTime"].ToString());
                            docket.Problem = dr["Problem"].ToString();
                            docket.isCustomerEntry = bool.Parse(dr["IsCustomerEntry"].ToString());
                            docket.CallStatusId = (dr["CallStatusId"] == DBNull.Value) ? 0 : int.Parse(dr["CallStatusId"].ToString());
                        }
                    }
                    con.Close();
                }
            }
            return docket;
        }

        public static int Service_Docket_Delete(int docketid)
        {
            int rowsAffacted = 0;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Parameters.AddWithValue("@DocketId", docketid);

                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "usp_Service_Docket_Delete";
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    rowsAffacted = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return rowsAffacted;
        }

        public static DataSet Service_Docket_GetByCallStatusIds(Entity.Service.Docket docket)
        {
            using (DataSet ds = new DataSet())
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Parameters.AddWithValue("@CallStatusId", docket.CallStatusIds);
                        if (docket.AssignEngineer == 0)
                            cmd.Parameters.AddWithValue("@AssignEngineer", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@AssignEngineer", docket.AssignEngineer);
                        cmd.InsertPaging(docket, docket.DocketId);

                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "usp_Service_Docket_GetByCallStatusIds";
                        if (con.State == ConnectionState.Closed)
                            con.Open();
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(ds);
                        }
                        con.Close();
                    }
                }
                return ds;
            }
        }
    }
}

