using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Hospital_Data
{
    public class clsConsultationData
    {
        public async static Task<DataTable> GetAllConsultationsAsync()
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingData.ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SP_GetAllConsultations", connection))
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

        public static bool Find(int ConsultationID, ref string ConsultationName, ref decimal ConsultationFee, ref string Specialty)
        {
            bool isFound = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingData.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_GetConsultationByID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ConsultationID", ConsultationID);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                isFound = true;
                                ConsultationName = (string)reader["ConsultationName"];
                                ConsultationFee = (decimal)reader["ConsultationFee"];
                                Specialty = (string)reader["Specialty"];
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

        public async static Task<DataTable> GetAllSpecialitiesAsync()
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingData.ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SP_GetAllSpecialities", connection))
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
    }
}