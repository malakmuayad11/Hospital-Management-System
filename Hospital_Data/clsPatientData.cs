using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Hospital_Data
{
    public class clsPatientData
    {
        public async static Task<DataTable> GetAllPatientsAsync()
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingData.ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SP_GetAllPatients", connection))
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

        public static int AddNewPatient(int PersonID, string NationalNo, DateTime DateOfBirth,
            string Address, string EmergencyContact)
        {
            int? PatientID = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingData.ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SP_AddNewPatient", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@PersonID", PersonID);
                        command.Parameters.AddWithValue("@NationalNo", NationalNo);
                        command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
                        command.Parameters.AddWithValue("@Address", Address);
                        command.Parameters.AddWithValue("@EmergencyContact", EmergencyContact);

                        SqlParameter outputIdParam = new SqlParameter("@PatientID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(outputIdParam);
                        command.ExecuteNonQuery();
                        PatientID = (int)(outputIdParam.Value);
                    }
                }
            }
            catch (SqlException ex)
            {
                clsLoggerData.Log(ex.Message, System.Diagnostics.EventLogEntryType.Error);
            }
            return PatientID ?? -1;
        }

        public static bool UpdatePatient(int PatientID, string NationalNo, DateTime DateOfBirth,
            string Address, string EmergencyContact)
        {
            int rowsEffected = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingData.ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SP_UpdatePatient", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@PatientID", PatientID);
                        command.Parameters.AddWithValue("@NationalNo", NationalNo);
                        command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
                        command.Parameters.AddWithValue("@Address", Address);
                        command.Parameters.AddWithValue("@EmergencyContact", EmergencyContact);

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


        public static bool Find(int PatientID, ref int PersonID, ref string NationalNo,
            ref DateTime DateOfBirth, ref string Address, ref string EmergencyContact)
        {
            bool isFound = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingData.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_GetPatientByID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@PatientID", PatientID);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                isFound = true;
                                PersonID = (int)reader["PersonID"];
                                NationalNo = (string)reader["NationalNo"];
                                DateOfBirth = (DateTime)reader["DateOfBirth"];
                                Address = (string)reader["Address"];
                                EmergencyContact = (string)reader["EmergencyContact"];
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

        public static bool Find(string NationalNo, ref int PatientID, ref int PersonID,
            ref DateTime DateOfBirth, ref string Address, ref string EmergencyContact)
        {
            bool isFound = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingData.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_GetPatientByNationalNo", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@NationalNo", NationalNo);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                isFound = true;
                                PersonID = (int)reader["PersonID"];
                                PatientID = (int)reader["PatientID"];
                                DateOfBirth = (DateTime)reader["DateOfBirth"];
                                Address = (string)reader["Address"];
                                EmergencyContact = (string)reader["EmergencyContact"];
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

        public static bool DeletePatient(int PatientID)
        {
            int Result = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingData.ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SP_DeletePatient", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@PatientID", PatientID);
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

        public static bool DoesNationalNoExist(string NationalNo)
        {
            byte result = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingData.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_DoesNationalNoExist", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@NationalNo", NationalNo);

                        SqlParameter outputResult = new SqlParameter("@Result", SqlDbType.TinyInt)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(outputResult);
                        command.ExecuteNonQuery();
                        result = (byte)outputResult.Value;
                    }
                }
            }
            catch (SqlException ex)
            {
                clsLoggerData.Log(ex.Message, EventLogEntryType.Error);
            }
            return result > 0;
        }

        public static async Task<DataTable> GetPatientMedicalRecordsHistoryAsync(int PatientID, bool IsMedicalRecord)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingData.ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SP_GetPatientHistory", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@PatientID", PatientID);
                        command.Parameters.AddWithValue("@IsMedicalRecord", IsMedicalRecord);
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

        public static bool HasPatientMedicalRecords(int PatientID)
        {
            int result = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingData.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_HasPatientMedicalRecords", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@PatientID", PatientID);

                        result = Convert.ToInt32(command.ExecuteScalar());
                    }
                }
            }
            catch (SqlException ex)
            {
                clsLoggerData.Log(ex.Message, EventLogEntryType.Error);
            }
            return result > 0;
        }

        public static bool HasPatientPrescriptions(int PatientID)
        {
            int result = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingData.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_HasPatientPrescriptions", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@PatientID", PatientID);

                        result = Convert.ToInt32(command.ExecuteScalar());
                    }
                }
            }
            catch (SqlException ex)
            {
                clsLoggerData.Log(ex.Message, EventLogEntryType.Error);
            }
            return result > 0;
        }

        public static bool HasPatientAppointmentAt(int PatientID, DateTime AppointmentDate,
            TimeSpan AppointmentTime)
        {
            int result = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingData.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_HasPatientAppointmentAt", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@PatientID", PatientID);
                        command.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
                        command.Parameters.AddWithValue("@AppointmentTime", AppointmentTime);

                        result = Convert.ToInt32(command.ExecuteScalar());
                    }
                }
            }
            catch (SqlException ex)
            {
                clsLoggerData.Log(ex.Message, EventLogEntryType.Error);
            }
            return result > 0;
        }
    }
}
