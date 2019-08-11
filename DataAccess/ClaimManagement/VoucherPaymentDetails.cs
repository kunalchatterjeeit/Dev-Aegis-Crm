using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess.ClaimManagement
{
    public class VoucherPaymentDetails
    {
        public static int VoucherPaymentDetails_Save(Entity.ClaimManagement.VoucherPaymentDetails voucherPaymentDetails)
        {
            int rowsAffacted = 0;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "usp_HR_VoucherPaymentDetails_Save";
                    cmd.Parameters.AddWithValue("@VoucherPaymentDetailsId", voucherPaymentDetails.VoucherPaymentDetailsId);
                    cmd.Parameters.AddWithValue("@VoucherPaymentId", voucherPaymentDetails.VoucherPaymentId);
                    cmd.Parameters.AddWithValue("@PaymentModeId", voucherPaymentDetails.PaymentModeId);
                    cmd.Parameters.AddWithValue("@PaymentAmount", voucherPaymentDetails.PaymentAmount);
                    cmd.Parameters.AddWithValue("@Description", voucherPaymentDetails.Description);
                    cmd.Parameters.AddWithValue("@CreatedBy", voucherPaymentDetails.CreatedBy);

                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    rowsAffacted = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return rowsAffacted;
        }

        public static DataTable VoucherPaymentDetails_GetById(int voucherPaymentDetailsId)
        {
            using (DataTable dt = new DataTable())
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "usp_HR_VoucherPaymentDetails_GetById";
                        cmd.Parameters.AddWithValue("@VoucherPaymentDetailsId", voucherPaymentDetailsId);
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

        public static DataTable VoucherPaymentDetails_GetAll(Entity.ClaimManagement.VoucherPaymentDetails voucherPaymentDetails)
        {
            using (DataTable dt = new DataTable())
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "usp_HR_VoucherPaymentDetails_GetAll";
                        if (voucherPaymentDetails.VoucherPaymentId == 0)
                            cmd.Parameters.AddWithValue("@VoucherPaymentId", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@VoucherPaymentId", voucherPaymentDetails.VoucherPaymentId);
                        if (voucherPaymentDetails.PaymentModeId == 0)
                            cmd.Parameters.AddWithValue("@PaymentModeId", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@PaymentModeId", voucherPaymentDetails.PaymentModeId);
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
