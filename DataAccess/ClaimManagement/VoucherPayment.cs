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
    public class VoucherPayment
    {
        public static int VoucherPayment_Save(Entity.ClaimManagement.VoucherPayment VoucherPayment)
        {
            int rowsAffacted = 0;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "usp_HR_VoucherPayment_Save";
                    cmd.Parameters.AddWithValue("@VoucherPaymentId", VoucherPayment.VoucherPaymentId);
                    cmd.Parameters.AddWithValue("@VoucherId", VoucherPayment.VoucherId);
                    cmd.Parameters.AddWithValue("@TotalAmount", VoucherPayment.TotalAmount);
                    cmd.Parameters.AddWithValue("@CreatedBy", VoucherPayment.CreatedBy);

                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    rowsAffacted = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return rowsAffacted;
        }

        public static DataTable VoucherPayment_GetById(int VoucherPaymentId)
        {
            using (DataTable dt = new DataTable())
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "usp_HR_VoucherPayment_GetById";
                        cmd.Parameters.AddWithValue("@VoucherPaymentId", VoucherPaymentId);
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

        public static DataTable VoucherPayment_GetAll(Entity.ClaimManagement.VoucherPayment VoucherPayment)
        {
            using (DataTable dt = new DataTable())
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "usp_HR_VoucherPayment_GetAll";
                        if (string.IsNullOrEmpty(VoucherPayment.VoucherNo))
                            cmd.Parameters.AddWithValue("@VoucherNo", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@VoucherNo", VoucherPayment.VoucherNo);
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
