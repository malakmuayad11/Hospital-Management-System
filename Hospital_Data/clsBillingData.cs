using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Hospital_Data
{
    public static class clsBillingData
    {
        public static bool UpdateBillingCharges(int BillingID, decimal? AdditionalCharges)
        {
            int rowsEffected = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingData.ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SP_UpdateBillingCharges", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@BillingID", BillingID);
                        command.Parameters.AddWithValue("@AdditionalCharges", AdditionalCharges ?? (object)DBNull.Value);
                        rowsEffected = command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                clsLoggerData.Log(ex.Message, EventLogEntryType.Error);
            }
            return rowsEffected > 0;
        }

        public static bool UpdateBillingPaymentStatus(int BillingID, bool IsPaid, byte? PaymentMethod)
        {
            int rowsEffected = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingData.ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SP_UpdateBillingPaymentStatus", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@BillingID", BillingID);
                        command.Parameters.AddWithValue("@IsPaid", IsPaid);
                        command.Parameters.AddWithValue("@PaymentMethod", PaymentMethod ?? (object)DBNull.Value);
                        rowsEffected = command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                clsLoggerData.Log(ex.Message, EventLogEntryType.Error);
            }
            return rowsEffected > 0;
        }

        public async static Task<DataTable> GetAllBillingsAsync()
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingData.ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SP_GetAllBillings", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (reader.HasRows)
                                dataTable.Load(reader);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                clsLoggerData.Log(ex.Message, System.Diagnostics.EventLogEntryType.Error);
            }
            return dataTable;
        }

        public static bool Find(int BillingID, ref int AppointmentID, ref decimal ConsultationFee,
            ref decimal? AdditionalCharges, ref bool IsPaid, ref byte? PaymentMethod)
        {
            bool isFound = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingData.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_GetBillingByID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@BillingID", BillingID);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                isFound = true;
                                AppointmentID = (int)reader["AppointmentID"];
                                ConsultationFee = (decimal)reader["ConsultationFee"];
                                AdditionalCharges = reader["AdditionalCharges"] == DBNull.Value ? null : (decimal?)reader["AdditionalCharges"];
                                IsPaid = (bool)reader["IsPaid"];
                                PaymentMethod = reader["PaymentMethod"] == DBNull.Value ? null : (byte?)reader["PaymentMethod"];
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                clsLoggerData.Log(ex.Message, EventLogEntryType.Error);
            }
            return isFound;
        }
    }
}
