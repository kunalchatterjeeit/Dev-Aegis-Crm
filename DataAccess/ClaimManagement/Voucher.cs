using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ClaimManagement
{
    public class Voucher
    {
        public static int Voucher_Save(Entity.ClaimManagement.Voucher voucher)
        {
            int rowsAffacted = 0;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "usp_HR_Voucher_Save";
                    cmd.Parameters.AddWithValue("@VoucherId", voucher.VoucherId);
                    cmd.Parameters.AddWithValue("@VoucherJson", voucher.VoucherJson);
                    cmd.Parameters.AddWithValue("@CreatedBy", voucher.CreatedBy);

                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    rowsAffacted = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return rowsAffacted;
        }

        public static DataTable Voucher_GetById(int VoucherId)
        {
            using (DataTable dt = new DataTable())
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "usp_HR_Voucher_GetById";
                        cmd.Parameters.AddWithValue("@VoucherId", VoucherId);
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

        public static DataTable Voucher_GetAll(Entity.ClaimManagement.Voucher voucher)
        {
            using (DataTable dt = new DataTable())
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "usp_HR_Voucher_GetAll";
                        if (string.IsNullOrEmpty(voucher.VoucherNo))
                            cmd.Parameters.AddWithValue("@VoucherNo", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@VoucherNo", voucher.VoucherNo);
                        if (voucher.FromDate == DateTime.MinValue)
                            cmd.Parameters.AddWithValue("@FromDate", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@FromDate", voucher.FromDate);
                        if (voucher.ToDate == DateTime.MinValue)
                            cmd.Parameters.AddWithValue("@ToDate", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@ToDate", voucher.ToDate);
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
