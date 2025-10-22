using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Hospital_Data
{
    public static class clsDoctorData
    {
        public static int GetDoctorsCount()
        {
            int DoctorsCount = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingData.ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SP_GetDoctorsCount", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        DoctorsCount = Convert.ToInt32(command.ExecuteScalar());
                    }
                }
            }
            catch (SqlException ex)
            {
                clsLoggerData.Log(ex.Message, System.Diagnostics.EventLogEntryType.Error);
            }
            return DoctorsCount;
        }

        public static int AddNewDoctor(int PersonID, byte StartWorkDay,
            byte EndWorkDay, TimeSpan StartWorkHour, TimeSpan EndWorkHour, int ConsultationID)
        {
            int? DoctorID = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingData.ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SP_AddNewDoctor", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@PersonID", PersonID);
                        command.Parameters.AddWithValue("@StartWorkDay", StartWorkDay);
                        command.Parameters.AddWithValue("@EndWorkDay", EndWorkDay);
                        command.Parameters.AddWithValue("@StartWorkHour", StartWorkHour);
                        command.Parameters.AddWithValue("@EndWorkHour", EndWorkHour);
                        command.Parameters.AddWithValue("@ConsultationID", ConsultationID);

                        SqlParameter outputIdParam = new SqlParameter("@DoctorID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(outputIdParam);
                        command.ExecuteNonQuery();
                        DoctorID = (int)(outputIdParam.Value);
                    }
                }
            }
            catch (SqlException ex)
            {
                clsLoggerData.Log(ex.Message, System.Diagnostics.EventLogEntryType.Error);
            }
            return DoctorID ?? -1;
        }

        public static bool UpdateDoctor(int DoctorID, string FirstName, string LastName,
            string Phone, string Email, byte StartWorkDay, byte EndWorkDay,
            TimeSpan StartWorkHour, TimeSpan EndWorkHour, int ConsultationID)
        {
            int rowsEffected = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingData.ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SP_UpdateDoctor", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@DoctorID", DoctorID);
                        command.Parameters.AddWithValue("@StartWorkDay", StartWorkDay);
                        command.Parameters.AddWithValue("@EndWorkDay", EndWorkDay);
                        command.Parameters.AddWithValue("@StartWorkHour", StartWorkHour);
                        command.Parameters.AddWithValue("@EndWorkHour", EndWorkHour);
                        command.Parameters.AddWithValue("@ConsultationID", ConsultationID);

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

        public static bool Find(int DoctorID, ref int PersonID,
            ref byte StartWorkDay, ref byte EndWorkDay, ref TimeSpan StartWorkHour, ref TimeSpan EndWorkHour,
            ref int ConsultationID, ref int UserID)
        {
            bool isFound = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingData.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_GetDoctorByDoctorID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@DoctorID", DoctorID);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                isFound = true;
                                PersonID = (int)reader["PersonID"];
                                StartWorkDay = (byte)reader["StartWorkDay"];
                                EndWorkDay = (byte)reader["EndWorkDay"];
                                StartWorkHour = (TimeSpan)reader["StartWorkHour"];
                                EndWorkHour = (TimeSpan)reader["EndWorkHour"];
                                ConsultationID = (int)reader["ConsultationID"];
                                UserID = (int)reader["UserID"];
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

        public static bool Find(int? UserID, ref int DoctorID, ref int PersonID,
            ref byte StartWorkDay, ref byte EndWorkDay, ref TimeSpan StartWorkHour,
            ref TimeSpan EndWorkHour, ref int ConsultationID)
        {
            bool isFound = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingData.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_GetDoctorByUserID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@UserID", UserID);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                isFound = true;
                                DoctorID = (int)reader["DoctorID"];
                                PersonID = (int)reader["PersonID"];
                                StartWorkDay = (byte)reader["StartWorkDay"];
                                EndWorkDay = (byte)reader["EndWorkDay"];
                                StartWorkHour = (TimeSpan)reader["StartWorkHour"];
                                EndWorkHour = (TimeSpan)reader["EndWorkHour"];
                                ConsultationID = (int)reader["ConsultationID"];
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

        public static bool DeleteDoctor(int DoctorID)
        {
            int Result = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingData.ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SP_DeleteDoctor", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@DoctorID", DoctorID);
                        Result = (int)command.ExecuteScalar();
                    }
                }
            }
            catch (SqlException ex)
            {
                clsLoggerData.Log(ex.Message, EventLogEntryType.Error);
            }
            return Result > 0;
        }

        public async static Task<DataTable> GetAllDoctorsAsync()
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingData.ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SP_GetAllDoctors", connection))
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

        public async static Task<DataTable> GetTodaysAppointmentsForDoctor(int DoctorID)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingData.ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SP_GetTodaysAppointmentsForDoctor", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@DoctorID", DoctorID);
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

        public static int GetPatientsCountForDoctor(int DoctorID)
        {
            int PatientsCount = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingData.ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SP_GetPatientsCountForDoctor", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@DoctorID", DoctorID);
                        PatientsCount = Convert.ToInt32(command.ExecuteScalar());
                    }
                }
            }
            catch (SqlException ex)
            {
                clsLoggerData.Log(ex.Message, System.Diagnostics.EventLogEntryType.Error);
            }
            return PatientsCount;
        }

        public static int GetAppointmentsCountForDoctor(int DoctorID)
        {
            int AppointmentsCount = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingData.ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SP_GetAppointmentsCountForDoctor", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@DoctorID", DoctorID);
                        AppointmentsCount = Convert.ToInt32(command.ExecuteScalar());
                    }
                }
            }
            catch (SqlException ex)
            {
                clsLoggerData.Log(ex.Message, System.Diagnostics.EventLogEntryType.Error);
            }
            return AppointmentsCount;
        }

        public static int GetMedicalRecordsCountForDoctor(int DoctorID)
        {
            int AppointmentsCount = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingData.ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SP_GetMedicalRecordsCountForDoctor", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@DoctorID", DoctorID);
                        AppointmentsCount = Convert.ToInt32(command.ExecuteScalar());
                    }
                }
            }
            catch (SqlException ex)
            {
                clsLoggerData.Log(ex.Message, System.Diagnostics.EventLogEntryType.Error);
            }
            return AppointmentsCount;
        }
    }
}
