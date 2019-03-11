using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Entity.Service;
using DataAccess.Common;

namespace DataAccess.Service
{
    public class ServiceBook
    {
        public ServiceBook()
        { }

        public static long Service_ServiceBook_Save(Entity.Service.ServiceBook serviceBook)
        {
            long rowsAffacted = 0;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    //using (DataSet ds = new DataSet())
                    //{
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "usp_Service_ServiceBook_Save";

                    cmd.Parameters.AddWithValue("@ServiceBookId", serviceBook.ServiceBookId).Direction = ParameterDirection.InputOutput;
                    cmd.Parameters.AddWithValue("@CallId", serviceBook.CallId);
                    cmd.Parameters.AddWithValue("@CallType", serviceBook.CallType);
                    cmd.Parameters.AddWithValue("@EmployeeId_FK", serviceBook.EmployeeId_FK);
                    cmd.Parameters.AddWithValue("@Remarks", serviceBook.Remarks);
                    //cmd.Parameters.AddWithValue("@InTime", serviceBook.InTime);
                    //if (serviceBook.OutTime == DateTime.MinValue)
                    //    cmd.Parameters.AddWithValue("@OutTime", DBNull.Value);
                    //else
                    //    cmd.Parameters.AddWithValue("@OutTime", serviceBook.OutTime);
                    cmd.Parameters.AddWithValue("@Diagnosis", serviceBook.Diagnosis);
                    cmd.Parameters.AddWithValue("@ActionTaken", serviceBook.ActionTaken);
                    cmd.Parameters.AddWithValue("@CallStatusId", serviceBook.CallStatusId);
                    cmd.Parameters.AddWithValue("@CustomerFeedBack", serviceBook.CustomerFeedback);
                    cmd.Parameters.AddWithValue("@CreatedBy", serviceBook.CreatedBy);
                    if (serviceBook.ProblemObserved == "")
                        cmd.Parameters.AddWithValue("@ProblemObserved", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@ProblemObserved", serviceBook.ProblemObserved);

                    //ds.Tables.Add(serviceBook.ServiceBookDetails);
                    //cmd.Parameters.AddWithValue("@ServiceBookDetails", ds.GetXml());
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
                    //}
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    cmd.ExecuteNonQuery();
                    rowsAffacted = Convert.ToInt64(cmd.Parameters["@ServiceBookId"].Value);
                    con.Close();
                }
            }
            return rowsAffacted;
        }

        public static int Service_ServiceBookDetails_Save(Entity.Service.ServiceBook serviceBook)
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
                        cmd.CommandText = "usp_Service_ServiceBookDetails_Save";

                        cmd.Parameters.AddWithValue("@ServiceBookId", serviceBook.ServiceBookId);
                        cmd.Parameters.AddWithValue("@CallId", serviceBook.CallId);
                        cmd.Parameters.AddWithValue("@CallType", serviceBook.CallType);
                        ds.Tables.Add(serviceBook.ServiceBookDetails);
                        cmd.Parameters.AddWithValue("@ServiceBookDetails", ds.GetXml());
                        cmd.Parameters.AddWithValue("@CreatedBy", serviceBook.CreatedBy);
                    }
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    cmd.ExecuteNonQuery();
                    rowsAffacted = Convert.ToInt32(cmd.Parameters["@ServiceBookId"].Value);
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
                            cmd.Parameters.AddWithValue("@AssetId", item["AssetId"].ToString());
                            cmd.Parameters.AddWithValue("@AssetLocationId", item["AssetLocationId"].ToString());
                            cmd.Parameters.AddWithValue("@CustomerId", serviceBook.CustomerId);
                            cmd.Parameters.AddWithValue("@CreatedBy", serviceBook.CreatedBy);

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

        public static DataTable ServiceBookMasterHistory_GetAllByCallId(long callId, int callType)
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

                        if (string.IsNullOrEmpty(serviceBook.CustomerName))
                            cmd.Parameters.AddWithValue("@CustomerName", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@CustomerName", serviceBook.CustomerName);

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

        //public static DataTable GetLastMeterReadingByCustomerPurchaseIdAndItemId(int customerPurchaseId, int spareId)
        //{
        //    using (DataTable dt = new DataTable())
        //    {
        //        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
        //        {
        //            using (SqlCommand cmd = new SqlCommand())
        //            {
        //                cmd.Connection = con;
        //                cmd.CommandType = CommandType.StoredProcedure;
        //                cmd.CommandText = "usp_Service_GetLastMeterReadingByCustomerPurchaseIdAndItemId";

        //                cmd.Parameters.AddWithValue("@CustomerPurchaseId", customerPurchaseId);
        //                cmd.Parameters.AddWithValue("@SpareId", spareId);

        //                if (con.State == ConnectionState.Closed)
        //                    con.Open();
        //                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
        //                {
        //                    da.Fill(dt);
        //                }
        //                con.Close();
        //            }
        //        }
        //        return dt;
        //    }
        //}

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

        public static DataSet Service_CSR_GetByDocketNo(string docketNo)
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
                        cmd.CommandText = "usp_Service_CSR_GetByDocketNo";
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

        public static DataSet Service_GetLastMeterReadingByCustomerPurchaseId(long customerPurchaseId)
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

        public static DataTable GetSpareInventory_ByProductId(long productId, int assetLocationId)
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
                        cmd.Parameters.AddWithValue("@Comment", drItem["Comment"]);
                        if (string.IsNullOrEmpty(drItem["RespondBy"].ToString()))
                        {
                            cmd.Parameters.AddWithValue("@RespondBy", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@RespondBy", drItem["RespondBy"]);
                        }
                        if (drItem["IsLowYield"] == null)
                        {
                            cmd.Parameters.AddWithValue("@IsLowYield", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@IsLowYield", drItem["IsLowYield"]);
                        }
                        if (drItem["CallStatus"] == null)
                        {
                            cmd.Parameters.AddWithValue("@CallStatus", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@CallStatus", drItem["CallStatus"]);
                        }
                        if (drItem["RequisiteQty"] == null)
                        {
                            cmd.Parameters.AddWithValue("@RequisiteQty", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@RequisiteQty", drItem["RequisiteQty"]);
                        }
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
                        cmd.Parameters.AddWithValue("@CallType", serviceBook.CallType);
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

        public static int Service_CallTransfer_Save(Entity.Service.ServiceBook serviceBook)
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
                        cmd.CommandText = "usp_Service_CallTransfer_Save";

                        cmd.Parameters.AddWithValue("@CallTransferId", serviceBook.CallTransferId);
                        cmd.Parameters.AddWithValue("@CallId", serviceBook.CallId);
                        cmd.Parameters.AddWithValue("@CallType", serviceBook.CallType);
                        cmd.Parameters.AddWithValue("@NewEngineerId", serviceBook.EmployeeId_FK);
                        cmd.Parameters.AddWithValue("@TransferReason", serviceBook.Remarks);
                        cmd.Parameters.AddWithValue("@CreatedBy", serviceBook.CreatedBy);
                    }
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    rowsAffacted = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return rowsAffacted;
        }

        public static bool Service_ServiceSpareApprovalCheck(Entity.Service.ServiceBook serviceBook)
        {
            bool retValue = false;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    using (DataSet ds = new DataSet())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "usp_Service_ServiceSpareApprovalCheck";
                        cmd.Parameters.AddWithValue("@LowYieldFound", false).Direction = ParameterDirection.InputOutput;
                        cmd.Parameters.AddWithValue("@ServiceBookId", serviceBook.ServiceBookId);
                        cmd.Parameters.AddWithValue("@CallId", serviceBook.CallId);
                        using (DataSet dsItems = new DataSet())
                        {
                            dsItems.Tables.Add(serviceBook.ApprovalItems);
                            cmd.Parameters.AddWithValue("@SpareXml", dsItems.GetXml());
                        }
                    }
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    cmd.ExecuteNonQuery();
                    retValue = Convert.ToBoolean(cmd.Parameters["@LowYieldFound"].Value);
                    con.Close();
                }
            }
            return retValue;
        }

        public static DataSet Service_ServiceBookMaster_GetByCallId(long callId, CallType callType)
        {
            using (DataSet ds = new DataSet())
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Parameters.AddWithValue("@CallId", callId);
                        cmd.Parameters.AddWithValue("@CallType", (int)callType);

                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "usp_Service_ServiceBookMaster_GetByCallId";
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

        public static DataSet Service_AssociatedEngineers_GetByCallId(long callId, CallType callType)
        {
            using (DataSet ds = new DataSet())
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Parameters.AddWithValue("@CallId", callId);
                        cmd.Parameters.AddWithValue("@CallType", (int)callType);

                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "usp_Service_AssociatedEngineers_GetByCallId";
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

        public static ApprovalStatus Service_GetServiceBookDetailsApprovalStatus(long serviceBookId, long itemId)
        {
            ApprovalStatus approvalStatus = ApprovalStatus.None;
            using (DataTable ds = new DataTable())
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Parameters.AddWithValue("@ServiceBookId", serviceBookId);
                        cmd.Parameters.AddWithValue("@ItemId", itemId);

                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "usp_Service_GetServiceBookDetailsApprovalStatus";
                        if (con.State == ConnectionState.Closed)
                            con.Open();
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(ds);
                        }
                        con.Close();

                        if (ds != null && ds.Rows.Count > 0)
                        {
                            Enum.TryParse(ds.Rows[0][0].ToString(), out approvalStatus);
                        }
                    }
                }
            }
            return approvalStatus;
        }

        public static DataSet Service_ServiceBookDetailsApproval_GetById(long serviceBookId, long itemId)
        {
            using (DataSet ds = new DataSet())
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Parameters.AddWithValue("@ServiceBookId", serviceBookId);
                        cmd.Parameters.AddWithValue("@ItemId", itemId);

                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "usp_Service_ServiceBookDetailsApproval_GetById";
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

        public static DataSet Service_ServiceCallAttendance_GetAll(Entity.Service.ServiceCallAttendance serviceCallAttendance)
        {
            using (DataSet ds = new DataSet())
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        if (string.IsNullOrEmpty(serviceCallAttendance.RequestNo))
                            cmd.Parameters.AddWithValue("@DocketNo", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@DocketNo", serviceCallAttendance.RequestNo);
                        if (string.IsNullOrEmpty(serviceCallAttendance.MachineId))
                            cmd.Parameters.AddWithValue("@MachineId", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@MachineId", serviceCallAttendance.MachineId);
                        if (serviceCallAttendance.EmployeeId == 0)
                            cmd.Parameters.AddWithValue("@EngineerId", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@EngineerId", serviceCallAttendance.EmployeeId);
                        if (serviceCallAttendance.FromDate == DateTime.MinValue)
                            cmd.Parameters.AddWithValue("@FromDate", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@FromDate", serviceCallAttendance.FromDate);
                        if (serviceCallAttendance.ToDate == DateTime.MinValue)
                            cmd.Parameters.AddWithValue("@ToDate", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@ToDate", serviceCallAttendance.ToDate);

                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "usp_Service_ServiceCallAttendance_GetAll";
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

        public static int Service_CallAttendance_Save(ServiceCallAttendance serviceCallAttendance)
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
                        cmd.CommandText = "usp_Service_ServiceCallAttendance_Save";

                        cmd.Parameters.AddWithValue("@ServiceCallAttendanceId", serviceCallAttendance.ServiceCallAttendanceId);
                        cmd.Parameters.AddWithValue("@ServiceBookId", serviceCallAttendance.ServiceBookId);
                        if (serviceCallAttendance.InTime != DateTime.MinValue)
                            cmd.Parameters.AddWithValue("@InTime", serviceCallAttendance.InTime);
                        else
                            cmd.Parameters.AddWithValue("@InTime", DBNull.Value);
                        if (serviceCallAttendance.OutTime != DateTime.MinValue)
                            cmd.Parameters.AddWithValue("@OutTime", serviceCallAttendance.OutTime);
                        else
                            cmd.Parameters.AddWithValue("@OutTime", DBNull.Value);
                        cmd.Parameters.AddWithValue("@EmployeeId", serviceCallAttendance.EmployeeId);
                        cmd.Parameters.AddWithValue("@Status", serviceCallAttendance.Status);
                    }
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    rowsAffacted = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return rowsAffacted;
        }

        public static int Service_CallAttendance_Edit(ServiceCallAttendance serviceCallAttendance)
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
                        cmd.CommandText = "usp_Service_ServiceCallAttendance_Edit";

                        cmd.Parameters.AddWithValue("@ServiceCallAttendanceId", serviceCallAttendance.ServiceCallAttendanceId);
                        if (serviceCallAttendance.InTime != DateTime.MinValue)
                            cmd.Parameters.AddWithValue("@InTime", serviceCallAttendance.InTime);
                        else
                            cmd.Parameters.AddWithValue("@InTime", DBNull.Value);
                        if (serviceCallAttendance.OutTime != DateTime.MinValue)
                            cmd.Parameters.AddWithValue("@OutTime", serviceCallAttendance.OutTime);
                        else
                            cmd.Parameters.AddWithValue("@OutTime", DBNull.Value);
                        if (serviceCallAttendance.CallStatusId != 0)
                            cmd.Parameters.AddWithValue("@CallStatusId", serviceCallAttendance.CallStatusId);
                        else
                            cmd.Parameters.AddWithValue("@CallStatusId", DBNull.Value);
                    }
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    rowsAffacted = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return rowsAffacted;
        }

        public static int Service_CallAttendance_Delete(long serviceCallAttendanceId)
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
                        cmd.CommandText = "usp_Service_ServiceCallAttendance_Delete";

                        cmd.Parameters.AddWithValue("@ServiceCallAttendanceId", serviceCallAttendanceId);
                    }
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    rowsAffacted = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return rowsAffacted;
        }

        public static DataTable Service_ServiceCallAttendanceByServiceBookId(long serviceBookId)
        {
            using (DataTable dt = new DataTable())
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        using (DataSet ds = new DataSet())
                        {
                            cmd.Connection = con;
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = "usp_Service_ServiceCallAttendanceByServiceBookId";

                            cmd.Parameters.AddWithValue("@ServiceBookId", serviceBookId);
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

        public static DataSet Service_SpareUsage(Entity.Service.ServiceBook serviceBook)
        {
            using (DataSet ds = new DataSet())
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "usp_Service_SpareUsage";

                        if (string.IsNullOrEmpty(serviceBook.CustomerName))
                            cmd.Parameters.AddWithValue("@CustomerName", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@CustomerName", serviceBook.CustomerName);
                        if (string.IsNullOrEmpty(serviceBook.RequestNo))
                            cmd.Parameters.AddWithValue("@CallNo", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@CallNo", serviceBook.RequestNo);
                        if (serviceBook.EmployeeId_FK == 0)
                            cmd.Parameters.AddWithValue("@EmployeeId", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@EmployeeId", serviceBook.EmployeeId_FK);
                        if (serviceBook.ItemId == 0)
                            cmd.Parameters.AddWithValue("@ItemId", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@ItemId", serviceBook.ItemId);
                        if (serviceBook.FromDate == DateTime.MinValue)
                            cmd.Parameters.AddWithValue("@FromDate", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@FromDate", serviceBook.FromDate);
                        if (serviceBook.ToDate == DateTime.MinValue)
                            cmd.Parameters.AddWithValue("@ToDate", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@ToDate", serviceBook.ToDate);
                        cmd.InsertPaging(serviceBook, serviceBook.ServiceBookId);

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

        public static DataTable Service_GetLastMeterReadingOfSpare(long callId, CallType callType, int itemId)
        {
            using (DataTable dt = new DataTable())
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        using (DataSet ds = new DataSet())
                        {
                            cmd.Connection = con;
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = "usp_Service_GetLastMeterReadingOfSpare";

                            cmd.Parameters.AddWithValue("@CallId", callId);
                            cmd.Parameters.AddWithValue("@CallType", (int)callType);
                            cmd.Parameters.AddWithValue("@ItemId", itemId);
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
    }
}
