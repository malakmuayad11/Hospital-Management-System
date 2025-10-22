using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Hospital_Data
{
    public static class clsMedicalRecordData
    {
        public static int AddNewMedicalRecord(int AppointmentID, string Symptoms,
            string Diagnosis, string MedicalRocordNotes)
        {
            int? MedicalRecordID = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingData.ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SP_AddNewMedicalRecord", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@AppointmentID", AppointmentID);
                        command.Parameters.AddWithValue("@Symptoms", Symptoms);
                        command.Parameters.AddWithValue("@Diagnosis", Diagnosis);
                        command.Parameters.AddWithValue("@MedicalRecordNotes", string.IsNullOrEmpty(MedicalRocordNotes) ? DBNull.Value.ToString() : MedicalRocordNotes);

                        SqlParameter outputIdParam = new SqlParameter("@MedicalRecordID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(outputIdParam);
                        command.ExecuteNonQuery();
                        MedicalRecordID = (int)(outputIdParam.Value);
                    }
                }
            }
            catch (SqlException ex)
            {
                clsLoggerData.Log(ex.Message, System.Diagnostics.EventLogEntryType.Error);
            }
            return MedicalRecordID ?? -1;
        }

        public async static Task<DataTable> GetAllMedicalRecordsAsync()
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingData.ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SP_GetAllMedicalRecords", connection))
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

        public static bool Find(int MedicalRecordID, ref int AppointmentID, ref string Symptoms,
            ref string Diagnosis, ref string MedicalRecordNotes)
        {
            bool isFound = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingData.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_GetMedicalRecordByID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@MedicalRecordID", MedicalRecordID);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                isFound = true;
                                AppointmentID = (int)reader["AppointmentID"];
                                Symptoms = (string)reader["Symptoms"];
                                Diagnosis = (string)reader["Diagnosis"];
                                MedicalRecordNotes = (string)reader["MedicalRecordNotes"];
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

        public static bool FindByAppointmentID(int AppointmentID, ref int MedicalRecordID, ref string Symptoms,
            ref string Diagnosis, ref string MedicalRecordNotes)
        {
            bool isFound = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingData.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_GetMedicalRecordByAppointmentID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@AppointmentID", AppointmentID);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                isFound = true;
                                MedicalRecordID = (int)reader["MedicalRecordID"];
                                Symptoms = (string)reader["Symptoms"];
                                Diagnosis = (string)reader["Diagnosis"];
                                MedicalRecordNotes = (string)reader["MedicalRecordNotes"];
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
