using DataAccess.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DataAccess.Service
{
    public class Contract
    {
        public static int Save(Entity.Service.Contract contract)
        {
            int rowsAffacted = 0;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "usp_Service_Contract_Save";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@ContractId", SqlDbType.Int);
                    cmd.Parameters["@ContractId"].Direction = ParameterDirection.InputOutput;
                    cmd.Parameters["@ContractId"].Value = contract.ContractId;
                    cmd.Parameters.AddWithValue("@CustomerId", contract.CustomerId);
                    cmd.Parameters.AddWithValue("@ContractTypeId", contract.ContractTypeId);
                    if (contract.ContractStartDate == DateTime.MinValue)
                        cmd.Parameters.AddWithValue("@ContractStartDate", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@ContractStartDate", contract.ContractStartDate);
                    if (contract.ContractEndDate == DateTime.MinValue)
                        cmd.Parameters.AddWithValue("@ContractEndDate", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@ContractEndDate", contract.ContractEndDate);
                    cmd.Parameters.AddWithValue("@CreatedBy", contract.CreatedBy);

                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    rowsAffacted = cmd.ExecuteNonQuery();
                    int contractid = int.Parse(cmd.Parameters["@ContractId"].Value.ToString());
                    con.Close();

                    if (rowsAffacted > 0)
                    {
                        foreach (DataRow dr in contract.ContractDetails.Rows)
                        {
                            using (SqlCommand cmdInner = new SqlCommand())
                            {
                                cmdInner.Connection = con;
                                cmdInner.CommandText = "usp_Service_ContractDetails_Save";
                                cmdInner.CommandType = CommandType.StoredProcedure;

                                cmdInner.Parameters.AddWithValue("@ContractId", contractid);
                                cmdInner.Parameters.AddWithValue("@CustomerPurchaseId", dr["CustomerPurchaseId"].ToString());
                                if (!String.IsNullOrEmpty(dr["A3BWStartMeter"].ToString()))
                                    cmdInner.Parameters.AddWithValue("@A3BWStartMeter", dr["A3BWStartMeter"].ToString());
                                else
                                    cmdInner.Parameters.AddWithValue("@A3BWStartMeter", DBNull.Value);
                                if (!String.IsNullOrEmpty(dr["A4BWStartMeter"].ToString()))
                                    cmdInner.Parameters.AddWithValue("@A4BWStartMeter", dr["A4BWStartMeter"].ToString());
                                else
                                    cmdInner.Parameters.AddWithValue("@A4BWStartMeter", DBNull.Value);
                                if (!String.IsNullOrEmpty(dr["A3CLStartMeter"].ToString()))
                                    cmdInner.Parameters.AddWithValue("@A3CLStartMeter", dr["A3CLStartMeter"].ToString());
                                else
                                    cmdInner.Parameters.AddWithValue("@A3CLStartMeter", DBNull.Value);
                                if (!String.IsNullOrEmpty(dr["A4CLStartMeter"].ToString()))
                                    cmdInner.Parameters.AddWithValue("@A4CLStartMeter", dr["A4CLStartMeter"].ToString());
                                else
                                    cmdInner.Parameters.AddWithValue("@A4CLStartMeter", DBNull.Value);
                                if (!String.IsNullOrEmpty(dr["A3BWPages"].ToString()))
                                    cmdInner.Parameters.AddWithValue("@A3BWPages", dr["A3BWPages"].ToString());
                                else
                                    cmdInner.Parameters.AddWithValue("@A3BWPages", DBNull.Value);
                                if (!String.IsNullOrEmpty(dr["A4BWPages"].ToString()))
                                    cmdInner.Parameters.AddWithValue("@A4BWPages", dr["A4BWPages"].ToString());
                                else
                                    cmdInner.Parameters.AddWithValue("@A4BWPages", DBNull.Value);
                                if (!String.IsNullOrEmpty(dr["A3CLPages"].ToString()))
                                    cmdInner.Parameters.AddWithValue("@A3CLPages", dr["A3CLPages"].ToString());
                                else
                                    cmdInner.Parameters.AddWithValue("@A3CLPages", DBNull.Value);
                                if (!String.IsNullOrEmpty(dr["A4CLPages"].ToString()))
                                    cmdInner.Parameters.AddWithValue("@A4CLPages", dr["A4CLPages"].ToString());
                                else
                                    cmdInner.Parameters.AddWithValue("@A4CLPages", DBNull.Value);
                                if (!String.IsNullOrEmpty(dr["A3BWRate"].ToString()))
                                    cmdInner.Parameters.AddWithValue("@A3BWRate", dr["A3BWRate"].ToString());
                                else
                                    cmdInner.Parameters.AddWithValue("@A3BWRate", DBNull.Value);
                                if (!String.IsNullOrEmpty(dr["A4BWRate"].ToString()))
                                    cmdInner.Parameters.AddWithValue("@A4BWRate", dr["A4BWRate"].ToString());
                                else
                                    cmdInner.Parameters.AddWithValue("@A4BWRate", DBNull.Value);
                                if (!String.IsNullOrEmpty(dr["A3CLRate"].ToString()))
                                    cmdInner.Parameters.AddWithValue("@A3CLRate", dr["A3CLRate"].ToString());
                                else
                                    cmdInner.Parameters.AddWithValue("@A3CLRate", DBNull.Value);
                                if (!String.IsNullOrEmpty(dr["A4CLRate"].ToString()))
                                    cmdInner.Parameters.AddWithValue("@A4CLRate", dr["A4CLRate"].ToString());
                                else
                                    cmdInner.Parameters.AddWithValue("@A4CLRate", DBNull.Value);

                                if (con.State == ConnectionState.Closed)
                                    con.Open();
                                cmdInner.ExecuteNonQuery();
                            }
                            con.Close();
                        }
                    }
                }
            }
            return rowsAffacted;
        }

        public static DataTable GetAll(int customerId)
        {
            using (DataTable dt = new DataTable())
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "usp_Service_Contract_GetAll";

                        cmd.Parameters.AddWithValue("@CustomerId", customerId);

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

        public static DataSet GetById(int contractId)
        {
            using (DataSet dt = new DataSet())
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Parameters.AddWithValue("@ContractId", contractId);

                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "usp_Service_Contract_GetById";
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

        public static int Delete(int contractId)
        {
            int i = 0;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Parameters.AddWithValue("@ContractId", contractId);

                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "usp_Service_Contract_Delete";
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    i = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return i;
        }


        public static bool Service_MachineIsInContractCheck(int customerPurchaseId)
        {
            using (DataTable dt = new DataTable())
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "usp_Service_MachineIsInContractCheck";
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
                return (dt != null && dt.Rows.Count > 0 && dt.Rows[0]["Flag"].ToString() == "1") ? false : true;
            }
        }

        public static DataTable Services_ContractStatus(int assignEngineer)
        {
            using (DataTable dt = new DataTable())
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "usp_Services_ContractStatus";
                        if (assignEngineer == 0)
                            cmd.Parameters.AddWithValue("@AssignEngineer", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@AssignEngineer", assignEngineer);
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

        public static DataSet Service_ContractStatusList(Entity.Service.Contract contract)
        {
            using (DataSet dt = new DataSet())
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "usp_Service_ContractStatusList";

                        if (contract.MachineId == "")
                            cmd.Parameters.AddWithValue("@MachineId", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@MachineId", contract.MachineId);
                        if (contract.FromDate == DateTime.MinValue)
                            cmd.Parameters.AddWithValue("@FromDate", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@FromDate", contract.FromDate);
                        if (contract.ToDate == DateTime.MinValue)
                            cmd.Parameters.AddWithValue("@ToDate", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@ToDate", contract.ToDate);
                        if (contract.AssignEngineer == 0)
                            cmd.Parameters.AddWithValue("@AssignEngineer", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@AssignEngineer", contract.AssignEngineer);
                        cmd.InsertPaging(contract, contract.ContractId);

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

        public static DataSet Service_ContractExpiringList(Entity.Service.Contract contract)
        {
            using (DataSet dt = new DataSet())
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "usp_Service_ContractExpiringList";

                        if (contract.MachineId == "")
                            cmd.Parameters.AddWithValue("@MachineId", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@MachineId", contract.MachineId);
                        if (contract.FromDate == DateTime.MinValue)
                            cmd.Parameters.AddWithValue("@FromDate", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@FromDate", contract.FromDate);
                        if (contract.ToDate == DateTime.MinValue)
                            cmd.Parameters.AddWithValue("@ToDate", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@ToDate", contract.ToDate);
                        if (contract.AssignEngineer == 0)
                            cmd.Parameters.AddWithValue("@AssignEngineer", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@AssignEngineer", contract.AssignEngineer);
                        cmd.InsertPaging(contract, contract.ContractId);

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

        public static DataSet Service_ContractExpiredList(Entity.Service.Contract contract)
        {
            using (DataSet dt = new DataSet())
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "usp_Service_ContractExpiredList";

                        if (contract.MachineId == "")
                            cmd.Parameters.AddWithValue("@MachineId", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@MachineId", contract.MachineId);
                        if (contract.FromDate == DateTime.MinValue)
                            cmd.Parameters.AddWithValue("@FromDate", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@FromDate", contract.FromDate);
                        if (contract.ToDate == DateTime.MinValue)
                            cmd.Parameters.AddWithValue("@ToDate", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@ToDate", contract.ToDate);
                        if (contract.AssignEngineer == 0)
                            cmd.Parameters.AddWithValue("@AssignEngineer", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@AssignEngineer", contract.AssignEngineer);
                        cmd.InsertPaging(contract, contract.ContractId);

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
