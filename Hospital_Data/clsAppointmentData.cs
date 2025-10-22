using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Hospital_Data
{
    public class clsAppointmentData
    {
        public static int GetAppointmentsCount()
        {
            int AppointmentsCount = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingData.ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SP_GetAppointmentsCount", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
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

        public async static Task<DataTable> GetTodaysAppointmentsAsync()
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingData.ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SP_GetTodaysAppointments", connection))
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

        public async static Task<DataTable> GetAllAppointments()
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingData.ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SP_GetAllAppointments", connection))
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

        public static int AddNewAppointment(int DoctorID, int PatientID, DateTime AppointmentDate,
            TimeSpan AppointmentTime, string ReasonForVisit)
        {
            int? AppointmentID = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingData.ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SP_AddNewAppointment", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@DoctorID", DoctorID);
                        command.Parameters.AddWithValue("@PatientID", PatientID);
                        command.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
                        command.Parameters.AddWithValue("@AppointmentTime", AppointmentTime);
                        command.Parameters.AddWithValue("@ReasonForVisit", ReasonForVisit);

                        SqlParameter outputIdParam = new SqlParameter("@AppointmentID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(outputIdParam);
                        command.ExecuteNonQuery();
                        AppointmentID = (int)(outputIdParam.Value);
                    }
                }
            }
            catch (SqlException ex)
            {
                clsLoggerData.Log(ex.Message, System.Diagnostics.EventLogEntryType.Error);
            }
            return AppointmentID ?? -1;
        }

        public static bool UpdateAppointmentDate(int AppointmentID, DateTime AppointmentDate, TimeSpan AppointmentTime)
        {
            int rowsEffected = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingData.ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SP_UpdateAppointmentDate", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@AppointmentID", AppointmentID);
                        command.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
                        command.Parameters.AddWithValue("@AppointmentTime", AppointmentTime);

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

        public static bool UpdateAppointmentStatus(int AppointmentID, byte Status)
        {
            int rowsEffected = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingData.ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SP_UpdateAppointmentStatus", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@AppointmentID", AppointmentID);
                        command.Parameters.AddWithValue("@Status", Status);

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

        public static bool Find(int AppointmentID, ref int DoctorID, ref int PatientID,
            ref DateTime AppointmentDate, ref TimeSpan AppointmentTime, ref string ReasonForVisit, ref byte Status)
        {
            bool isFound = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingData.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_GetAppointmentByID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@AppointmentID", AppointmentID);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                isFound = true;
                                DoctorID = (int)reader["DoctorID"];
                                PatientID = (int)reader["PatientID"];
                                AppointmentDate = (DateTime)reader["AppointmentDate"];
                                AppointmentTime = (TimeSpan)reader["AppointmentTime"];
                                ReasonForVisit = (string)reader["ReasonForVisit"];
                                Status = (byte)reader["Status"];
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

        public static bool CancelAppointment(int AppointmentID)
        {
            int rowsEffected = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingData.ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SP_CancelAppointment", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@AppointmentID", AppointmentID);

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

        public static bool UpdateAppointment(int AppointmentID, DateTime AppointmentDate, TimeSpan AppointmentTime)
        {
            int rowsEffected = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingData.ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SP_RescheduleAppointment", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@AppointmentID", AppointmentID);
                        command.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
                        command.Parameters.AddWithValue("@AppointmentTime", AppointmentTime);

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

        public static byte HasAppointmentMedicalRecord(int AppointmentID)
        {
            byte Result = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingData.ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SP_HasAppointmentMedicalRecord", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@AppointmentID", AppointmentID);
                        Result = Convert.ToByte(command.ExecuteScalar());
                    }
                }
            }
            catch (SqlException ex)
            {
                clsLoggerData.Log(ex.Message, System.Diagnostics.EventLogEntryType.Error);
            }
            return Result;
        }
    }
}
