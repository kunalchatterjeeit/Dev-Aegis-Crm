using DataAccess.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using DataAccess.Common;
using Entity.Service;

namespace DataAccess.Service
{
    public class TonnerRequest
    {
        public TonnerRequest()
        { }

        public static DataTable Service_TonerRequest_Save(Entity.Service.TonerRequest tonerRequest)
        {
            using (DataTable response = new DataTable())
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
                {
                    using (SqlCommand sqlCommand = new SqlCommand())
                    {
                        sqlCommand.Connection = sqlConnection;
                        sqlCommand.CommandText = "usp_Service_TonnerRequest_Save";
                        sqlCommand.CommandType = CommandType.StoredProcedure;

                        sqlCommand.Parameters.AddWithValue("@TonnerRequestId", tonerRequest.TonerRequestId);
                        sqlCommand.Parameters.AddWithValue("@CustomerPurchaseId", tonerRequest.CustomerPurchaseId);
                        sqlCommand.Parameters.AddWithValue("@RequestDateTime", tonerRequest.RequestDateTime);
                        if (tonerRequest.A3BWMeterReading == null)
                            sqlCommand.Parameters.AddWithValue("@A3BWMeterReading", DBNull.Value);
                        else
                            sqlCommand.Parameters.AddWithValue("@A3BWMeterReading", tonerRequest.A3BWMeterReading);
                        if (tonerRequest.A4BWMeterReading == null)
                            sqlCommand.Parameters.AddWithValue("@A4BWMeterReading", DBNull.Value);
                        else
                            sqlCommand.Parameters.AddWithValue("@A4BWMeterReading", tonerRequest.A4BWMeterReading);
                        if (tonerRequest.A3CLMeterReading == null)
                            sqlCommand.Parameters.AddWithValue("@A3CLMeterReading", DBNull.Value);
                        else
                            sqlCommand.Parameters.AddWithValue("@A3CLMeterReading", tonerRequest.A3CLMeterReading);
                        if (tonerRequest.A4CLMeterReading == null)
                            sqlCommand.Parameters.AddWithValue("@A4CLMeterReading", DBNull.Value);
                        else
                            sqlCommand.Parameters.AddWithValue("@A4CLMeterReading", tonerRequest.A4CLMeterReading);
                        sqlCommand.Parameters.AddWithValue("@SpareId", tonerRequest.SpareIds.FirstOrDefault().TonerId); //Passing the first spareId
                        sqlCommand.Parameters.AddWithValue("@TonerQty", tonerRequest.SpareIds.FirstOrDefault().Quantity); //Passing the first spareId
                        if (tonerRequest.Remarks == "")
                            sqlCommand.Parameters.AddWithValue("@Remarks", DBNull.Value);
                        else
                            sqlCommand.Parameters.AddWithValue("@Remarks", tonerRequest.Remarks);
                        sqlCommand.Parameters.AddWithValue("@IsCustomerEntry", tonerRequest.isCustomerEntry);
                        sqlCommand.Parameters.AddWithValue("@CallStatus", tonerRequest.CallStatusId);
                        sqlCommand.Parameters.AddWithValue("@CreatedBy", tonerRequest.CreatedBy);

                        if (sqlConnection.State == ConnectionState.Closed)
                            sqlConnection.Open();
                        using (SqlDataAdapter da = new SqlDataAdapter(sqlCommand))
                        {
                            da.Fill(response);
                            sqlConnection.Close();
                        }

                        //Inserting rest spareIds
                        if (response.Rows.Count > 0)//Valid response
                        {
                            for (int spareIndex = 1; spareIndex < tonerRequest.SpareIds.Count(); spareIndex++)
                            {
                                sqlCommand.Parameters.Clear();
                                sqlCommand.CommandText = "usp_Service_TonerRequestValue_Save";
                                sqlCommand.CommandType = CommandType.StoredProcedure;
                                sqlCommand.Parameters.AddWithValue("@TonerRequestValueId", tonerRequest.TonerRequestValueId);
                                sqlCommand.Parameters.AddWithValue("@TonerRequestId", response.Rows[0]["TonnerRequestId"].ToString());
                                sqlCommand.Parameters.AddWithValue("@TonerId", tonerRequest.SpareIds[spareIndex].TonerId);
                                sqlCommand.Parameters.AddWithValue("@TonerQty", tonerRequest.SpareIds[spareIndex].Quantity);
                                if (sqlConnection.State == ConnectionState.Closed)
                                    sqlConnection.Open();
                                sqlCommand.ExecuteNonQuery();
                            }
                        }
                        sqlConnection.Close();
                    }
                }
                return response;
            }
        }

        public static DataSet Service_TonnerRequest_GetAll(Entity.Service.TonerRequest tonerRequest)
        {
            using (DataSet response = new DataSet())
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        if (tonerRequest.TonerRequestId == 0)
                            cmd.Parameters.AddWithValue("@TonerRequestId", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@TonerRequestId", tonerRequest.TonerRequestId);
                        if (tonerRequest.RequestNo == "")
                            cmd.Parameters.AddWithValue("@RequestNo", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@RequestNo", tonerRequest.RequestNo);
                        if (tonerRequest.CustomerId == 0)
                            cmd.Parameters.AddWithValue("@CustomerId_FK", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@CustomerId_FK", tonerRequest.CustomerId);
                        if (tonerRequest.ProductId == 0)
                            cmd.Parameters.AddWithValue("@ProductId_FK", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@ProductId_FK", tonerRequest.ProductId);
                        if (tonerRequest.RequestFromDateTime == DateTime.MinValue)
                            cmd.Parameters.AddWithValue("@RequestFromDateTime", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@RequestFromDateTime", tonerRequest.RequestFromDateTime);
                        if (tonerRequest.RequestToDateTime == DateTime.MinValue)
                            cmd.Parameters.AddWithValue("@RequestToDateTime", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@RequestToDateTime", tonerRequest.RequestToDateTime);
                        if (string.IsNullOrEmpty(tonerRequest.MultipleCallStatusFilter))
                        {
                            if (tonerRequest.CallStatusId == 0)
                                cmd.Parameters.AddWithValue("@CallStatus", DBNull.Value);
                            else
                                cmd.Parameters.AddWithValue("@CallStatus", tonerRequest.CallStatusId);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@CallStatus", tonerRequest.MultipleCallStatusFilter);
                        }
                        if (tonerRequest.AssignEngineer == 0)
                            cmd.Parameters.AddWithValue("@AssignEngineer", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@AssignEngineer", tonerRequest.AssignEngineer);
                        cmd.InsertPaging(tonerRequest, tonerRequest.TonerRequestId);

                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "usp_Service_TonnerRequest_GetAll";
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            if (con.State == ConnectionState.Closed)
                                con.Open();
                            da.Fill(response);
                            con.Close();
                        }
                    }
                    return response;
                }
            }
        }

        public static DataTable Service_TonnerRequest_GetLast10()
        {
            using (DataTable response = new DataTable())
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "usp_Service_TonnerRequest_GetLast10";
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            if (con.State == ConnectionState.Closed)
                                con.Open();
                            da.Fill(response);
                            con.Close();
                        }
                    }
                }
                return response;
            }
        }

        public static DataTable Service_Tonner_GetAllByCustomerId(Int64 CustomerPurchaseId)
        {
            using (DataTable response = new DataTable())
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandText = "USP_Service_Tonner_GetAllByCustomerId";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CustomerPurchaseId", CustomerPurchaseId);
                        if (con.State == ConnectionState.Closed)
                            con.Open();
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(response);
                            con.Close();
                        }
                    }
                }
                return response;
            }
        }

        public static Entity.Service.TonerRequest Service_TonnerRequest_GetByTonnerRequestId(int tonnerRequestid)
        {
            Entity.Service.TonerRequest tonnerRequest = new Entity.Service.TonerRequest();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "usp_Service_TonnerRequest_GetByRequestId";

                    cmd.Parameters.AddWithValue("@TonnerRequestId", tonnerRequestid);

                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            if (dr.HasRows)
                            {
                                tonnerRequest.TonerRequestId = tonnerRequestid;
                                tonnerRequest.RequestNo = dr["RequestNo"].ToString();
                                tonnerRequest.CustomerId = (dr["CustomerId_FK"] == DBNull.Value) ? 0 : int.Parse(dr["CustomerId_FK"].ToString());
                                tonnerRequest.ProductId = int.Parse(dr["ProductId_FK"].ToString());
                                tonnerRequest.RequestDateTime = Convert.ToDateTime(dr["RequestDateTime"].ToString());
                                tonnerRequest.A3BWMeterReading = (dr["A3BWMeterReading"] == DBNull.Value) ? 0 : int.Parse(dr["A3BWMeterReading"].ToString());
                                tonnerRequest.A4BWMeterReading = (dr["A4BWMeterReading"] == DBNull.Value) ? 0 : int.Parse(dr["A4BWMeterReading"].ToString());
                                tonnerRequest.A3CLMeterReading = (dr["A3CLMeterReading"] == DBNull.Value) ? 0 : int.Parse(dr["A3CLMeterReading"].ToString());
                                tonnerRequest.A4CLMeterReading = (dr["A4CLMeterReading"] == DBNull.Value) ? 0 : int.Parse(dr["A4CLMeterReading"].ToString());
                                tonnerRequest.Remarks = dr["Remarks"].ToString();
                                tonnerRequest.isCustomerEntry = bool.Parse(dr["IsCustomerEntry"].ToString());
                                tonnerRequest.CallStatusId = (dr["CallStatus"] == DBNull.Value) ? 0 : int.Parse(dr["CallStatus"].ToString());
                            }
                        }
                        con.Close();
                    }
                }
                return tonnerRequest;
            }
        }

        public static int Service_TonnerRequest_Delete(int tonnerRequestid)
        {
            int rowsAffacted = 0;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Parameters.AddWithValue("@TonnerRequestId", tonnerRequestid);

                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "usp_Service_TonnerRequest_Delete";
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    rowsAffacted = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return rowsAffacted;
        }

        public static bool Service_TonerLowYieldCheck(Entity.Service.TonerRequest tonerRequest)
        {
            bool retValue = false;
            foreach (TonerIdQuantity toner in tonerRequest.SpareIds)
            {
                using (DataTable dt = new DataTable())
                {
                    using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
                    {
                        using (SqlCommand sqlCommand = new SqlCommand())
                        {
                            sqlCommand.Connection = con;
                            sqlCommand.CommandType = CommandType.StoredProcedure;
                            sqlCommand.CommandText = "usp_Service_TonerLowYieldCheck";
                            sqlCommand.Parameters.AddWithValue("@CustomerPurchaseId", tonerRequest.CustomerPurchaseId);
                            sqlCommand.Parameters.AddWithValue("@TonerId", toner.TonerId);
                            sqlCommand.Parameters.AddWithValue("@TonerQuantity", toner.Quantity);
                            if (tonerRequest.A3BWMeterReading == null)
                                sqlCommand.Parameters.AddWithValue("@A3BWCurrentMeterReading", 0);
                            else
                                sqlCommand.Parameters.AddWithValue("@A3BWCurrentMeterReading", tonerRequest.A3BWMeterReading);
                            if (tonerRequest.A4BWMeterReading == null)
                                sqlCommand.Parameters.AddWithValue("@A4BWCurrentMeterReading", 0);
                            else
                                sqlCommand.Parameters.AddWithValue("@A4BWCurrentMeterReading", tonerRequest.A4BWMeterReading);
                            if (tonerRequest.A3CLMeterReading == null)
                                sqlCommand.Parameters.AddWithValue("@A3CLCurrentMeterReading", 0);
                            else
                                sqlCommand.Parameters.AddWithValue("@A3CLCurrentMeterReading", tonerRequest.A3CLMeterReading);
                            if (tonerRequest.A4CLMeterReading == null)
                                sqlCommand.Parameters.AddWithValue("@A4CLCurrentMeterReading", 0);
                            else
                                sqlCommand.Parameters.AddWithValue("@A4CLCurrentMeterReading", tonerRequest.A4CLMeterReading);
                            if (con.State == ConnectionState.Closed)
                                con.Open();
                            using (SqlDataAdapter da = new SqlDataAdapter(sqlCommand))
                            {
                                da.Fill(dt);
                            }
                            con.Close();
                        }
                    }
                    if (dt != null && dt.Rows.Count > 0 && dt.Rows[0]["Flag"].ToString() == "1")
                    {
                        retValue = true;
                        break;
                    }
                }
            }
            return retValue;
        }

        public static DataTable Service_Toner_GetByCallStatusIds(string callStatusIds, int assignEngineer)
        {
            using (DataTable dt = new DataTable())
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Parameters.AddWithValue("@CallStatusId", callStatusIds);
                        if (assignEngineer == 0)
                            cmd.Parameters.AddWithValue("@AssignEngineer", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@AssignEngineer", assignEngineer);

                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "usp_Service_Docket_GetByCallStatusIds";
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
    }
}
