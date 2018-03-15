﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace DataAccess.Service
{
    public class ServiceBook
    {
        public ServiceBook()
        { }

        public static int Service_ServiceBook_Save(Entity.Service.ServiceBook serviceBook)
        {
            int rowsAffacted = 0;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    using (DataSet ds = new DataSet())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "usp_Service_ServiceBook_Save";

                        cmd.Parameters.AddWithValue("@ServiceBookId", serviceBook.ServiceBookId);
                        cmd.Parameters.AddWithValue("@CallId", serviceBook.CallId);
                        cmd.Parameters.AddWithValue("@CallType", serviceBook.CallType);
                        cmd.Parameters.AddWithValue("@EmployeeId_FK", serviceBook.EmployeeId_FK);
                        cmd.Parameters.AddWithValue("@Remarks", serviceBook.Remarks);
                        cmd.Parameters.AddWithValue("@InTime", serviceBook.InTime);
                        cmd.Parameters.AddWithValue("@OutTime", serviceBook.OutTime);
                        cmd.Parameters.AddWithValue("@Diagnosis", serviceBook.Diagnosis);
                        cmd.Parameters.AddWithValue("@ActionTaken", serviceBook.ActionTaken);
                        cmd.Parameters.AddWithValue("@CallStatusId", serviceBook.CallStatusId);
                        cmd.Parameters.AddWithValue("@CustomerFeedBack", serviceBook.CustomerFeedback);
                        cmd.Parameters.AddWithValue("@CreatedBy", serviceBook.CreatedBy);
                        if (serviceBook.ProblemObserved == "")
                            cmd.Parameters.AddWithValue("@ProblemObserved", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@ProblemObserved", serviceBook.ProblemObserved);

                        ds.Tables.Add(serviceBook.ServiceBookDetails);
                        cmd.Parameters.AddWithValue("@ServiceBookDetails", ds.GetXml());
                        cmd.Parameters.AddWithValue("@Signature", serviceBook.Signature);
                        cmd.Parameters.AddWithValue("@A3BWMeterReading", serviceBook.A3BWMeterReading);
                        cmd.Parameters.AddWithValue("@A3CLMeterReading", serviceBook.A3CLMeterReading);
                        cmd.Parameters.AddWithValue("@A4BWMeterReading", serviceBook.A4BWMeterReading);
                        cmd.Parameters.AddWithValue("@A4CLMeterReading", serviceBook.A4CLMeterReading);
                        using (DataSet dsAssociate = new DataSet())
                        {
                            dsAssociate.Tables.Add(serviceBook.AssociatedEngineers);
                            cmd.Parameters.AddWithValue("@AssociatedEngineers", dsAssociate.GetXml());
                        }
                    }
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    rowsAffacted = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return rowsAffacted;
        }

        public static int Service_TonerRequest_Approve(Entity.Service.ServiceBook serviceBook)
        {
            int rowsAffacted = 0;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "usp_Service_TonnerRequest_Approve";

                    cmd.Parameters.AddWithValue("@TonnerRequestId", serviceBook.TonnerRequestId);
                    cmd.Parameters.AddWithValue("@CallStatus", serviceBook.CallStatusId);
                    cmd.Parameters.AddWithValue("@Diagnosis", serviceBook.Diagnosis);
                    cmd.Parameters.AddWithValue("@ActionTaken", serviceBook.ActionTaken);
                    cmd.Parameters.AddWithValue("@ServiceEngineer", serviceBook.EmployeeId_FK);

                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    rowsAffacted = cmd.ExecuteNonQuery();

                    if (rowsAffacted > 0)
                    {
                        foreach (DataRow item in serviceBook.ServiceBookDetails.Rows)
                        {
                            cmd.Parameters.Clear();
                            cmd.Connection = con;
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = "usp_Service_ServiceBookTonerDetails_Approve";

                            cmd.Parameters.AddWithValue("@TonnerRequestId", serviceBook.TonnerRequestId);
                            cmd.Parameters.AddWithValue("@TonerId", Convert.ToInt64(item["TonerId"].ToString()));
                            cmd.Parameters.AddWithValue("@ServiceEngineer", serviceBook.EmployeeId_FK);

                            if (con.State == ConnectionState.Closed)
                                con.Open();
                            rowsAffacted = cmd.ExecuteNonQuery();
                        }
                    }
                    con.Close();
                }
            }
            return rowsAffacted;
        }

        public static DataSet Service_Tonner_GetByTonnerRequestId(Int64 tonnerRequestId)
        {
            using (DataSet ds = new DataSet())
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Parameters.AddWithValue("@TonnerRequestId", tonnerRequestId);

                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "usp_Service_Tonner_GetByTonnerRequestId";
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

        public static DataTable ServiceBookMasterHistory_GetAllByCallId(Int64 callId, int callType)
        {
            using (DataTable dt = new DataTable())
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "usp_Service_ServiceBookMasterHistory_GetAllByCallId";

                        cmd.Parameters.AddWithValue("@CallId", callId);
                        cmd.Parameters.AddWithValue("@CallType", callType);

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

        public static DataTable Service_ServiceBook_GetAll(int calltype, Entity.Service.ServiceBook serviceBook)
        {
            using (DataTable dt = new DataTable())
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "USP_Service_ServiceBook_GetAll";
                        cmd.Parameters.AddWithValue("@CallType", calltype);

                        if (serviceBook.CustomerId == 0)
                            cmd.Parameters.AddWithValue("@CustomerId", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@CustomerId", serviceBook.CustomerId);

                        if (serviceBook.ModelId == 0)
                            cmd.Parameters.AddWithValue("@ModelId", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@ModelId", serviceBook.ModelId);

                        if (serviceBook.MachineId == string.Empty)
                            cmd.Parameters.AddWithValue("@MachineId", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@MachineId", serviceBook.MachineId);

                        if (serviceBook.FromDate == DateTime.MinValue)
                            cmd.Parameters.AddWithValue("@FromDate", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@FromDate", serviceBook.FromDate);

                        if (serviceBook.ToDate == DateTime.MinValue)
                            cmd.Parameters.AddWithValue("@ToDate", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@ToDate", serviceBook.ToDate);

                        if (serviceBook.CallStatusId == 0)
                            cmd.Parameters.AddWithValue("@CallStatus", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@CallStatus", serviceBook.CallStatusId);

                        if (serviceBook.DocketType == string.Empty)
                            cmd.Parameters.AddWithValue("@DocketType", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@DocketType", serviceBook.DocketType);

                        if (serviceBook.EmployeeId_FK == 0)
                            cmd.Parameters.AddWithValue("@ServiceEngineerId", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@ServiceEngineerId", serviceBook.EmployeeId_FK);

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

        public static DataTable GetLastMeterReadingByCustomerPurchaseIdAndItemId(int customerPurchaseId, int spareId)
        {
            using (DataTable dt = new DataTable())
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "usp_Service_GetLastMeterReadingByCustomerPurchaseIdAndItemId";

                        cmd.Parameters.AddWithValue("@CustomerPurchaseId", customerPurchaseId);
                        cmd.Parameters.AddWithValue("@SpareId", spareId);

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

        public static DataTable Service_CheckIfAnyOpenTonnerRequest(int customerPurchaseId)
        {
            using (DataTable dt = new DataTable())
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "usp_Service_CheckIfAnyOpenTonnerRequest";

                        cmd.Parameters.AddWithValue("@CustomerPurchaseId", customerPurchaseId);

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

        public static DataTable Service_CheckIfAnyOpenDocket(int customerPurchaseId)
        {
            using (DataTable dt = new DataTable())
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "usp_Service_CheckIfAnyOpenDocket";

                        cmd.Parameters.AddWithValue("@CustomerPurchaseId", customerPurchaseId);

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

        public static int Service_MeterReading_Update(Entity.Service.ServiceBook serviceBook)
        {
            int rowsAffacted = 0;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "usp_Service_MeterReading_Update";

                    cmd.Parameters.AddWithValue("@CustomerPurchaseId", serviceBook.CustomerPurchaseId);
                    if (serviceBook.A3BWMeterReading == null && serviceBook.A3BWMeterReading == 0)
                        cmd.Parameters.AddWithValue("@A3BWLastMeterReading", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@A3BWLastMeterReading", serviceBook.A3BWMeterReading);
                    if (serviceBook.A4BWMeterReading == null && serviceBook.A4BWMeterReading == 0)
                        cmd.Parameters.AddWithValue("@A4BWLastMeterReading", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@A4BWLastMeterReading", serviceBook.A4BWMeterReading);
                    if (serviceBook.A3CLMeterReading == null && serviceBook.A3CLMeterReading == 0)
                        cmd.Parameters.AddWithValue("@A3CLLastMeterReading", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@A3CLLastMeterReading", serviceBook.A3CLMeterReading);
                    if (serviceBook.A4CLMeterReading == null && serviceBook.A4CLMeterReading == 0)
                        cmd.Parameters.AddWithValue("@A4CLLastMeterReading", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@A4CLLastMeterReading", serviceBook.A4CLMeterReading);

                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    rowsAffacted = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return rowsAffacted;
        }

        public static DataSet Service_CSR_GetByDocketId(Int64 docketNo)
        {
            using (DataSet ds = new DataSet())
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Parameters.AddWithValue("@DocketNo", docketNo);

                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "usp_Service_CSR_GetByDocketId";
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

        public static DataSet Service_Challan_GetByTonerRequestNo(string requestNo)
        {
            using (DataSet ds = new DataSet())
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Parameters.AddWithValue("@RequestNo", requestNo);

                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "usp_Service_Challan_GetByTonerRequestNo";
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

        public static DataSet Service_GetLastMeterReadingByCustomerPurchaseId(Int64 customerPurchaseId)
        {
            using (DataSet ds = new DataSet())
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Parameters.AddWithValue("@CustomerPurchaseId", customerPurchaseId);

                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "usp_Service_GetLastMeterReadingByCustomerPurchaseId";
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

        public static DataTable GetSpareInventory_ByProductId(Int64 productId, int assetLocationId)
        {
            using (DataTable dt = new DataTable())
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "usp_GetSpareInventory_ByProductId";

                        cmd.Parameters.AddWithValue("@ProductId", productId);
                        if (assetLocationId == 0)
                        {
                            cmd.Parameters.AddWithValue("@AssetLocationId", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@AssetLocationId", assetLocationId);
                        }

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

        public static int Service_ServiceBookDetailsApproval_Save(Entity.Service.ServiceBook serviceBook)
        {
            int rowsAffacted = 0;
            foreach (DataRow drItem in serviceBook.ApprovalItems.Rows)
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "usp_Service_ServiceBookDetailsApproval_Save";

                        cmd.Parameters.AddWithValue("@ApprovalId", drItem["ApprovalId"]);
                        cmd.Parameters.AddWithValue("@ServiceBookId", drItem["ServiceBookId"]);
                        cmd.Parameters.AddWithValue("@ItemId", drItem["ItemId"]);
                        cmd.Parameters.AddWithValue("@ApprovalStatus", drItem["ApprovalStatus"]);
                        if (string.IsNullOrEmpty(drItem["RespondBy"].ToString()))
                        {
                            cmd.Parameters.AddWithValue("@RespondBy", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@RespondBy", drItem["RespondBy"]);
                        }
                        cmd.Parameters.AddWithValue("@Comment", drItem["Comment"]);
                        if (con.State == ConnectionState.Closed)
                            con.Open();
                        rowsAffacted += cmd.ExecuteNonQuery();

                        con.Close();
                    }
                }
            }
            return rowsAffacted;
        }

        public static DataTable Service_ServiceBookDetailsApproval_GetAll(Entity.Service.ServiceBook serviceBook)
        {
            using (DataTable dt = new DataTable())
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "usp_Service_ServiceBookDetailsApproval_GetAll";

                        if (string.IsNullOrEmpty(serviceBook.MachineId))
                            cmd.Parameters.AddWithValue("@MachineId", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@MachineId", serviceBook.MachineId);
                        if (string.IsNullOrEmpty(serviceBook.RequestNo))
                            cmd.Parameters.AddWithValue("@RequestNo", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@RequestNo", serviceBook.RequestNo);
                        if (serviceBook.RequestFromDate == DateTime.MinValue)
                            cmd.Parameters.AddWithValue("@RequestFromDate", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@RequestFromDate", serviceBook.RequestFromDate);
                        if (serviceBook.RequestToDate == DateTime.MinValue)
                            cmd.Parameters.AddWithValue("@RequestToDate", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@RequestToDate", serviceBook.RequestToDate);
                        cmd.Parameters.AddWithValue("@ApprovalStatus", serviceBook.ApprovalStatus);
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
