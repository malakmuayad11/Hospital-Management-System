using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
namespace Hospital_Data
{
    public static class clsPrescriptionData
    {
        public static int AddNewPrescription(int AppointmentID, string MedicationName,
            string Dosage, byte DurationDays, byte? DurationMonths)
        {
            int? PrescriptionID = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingData.ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SP_AddNewPrescription", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@AppointmentID", AppointmentID);
                        command.Parameters.AddWithValue("@MedicationName", MedicationName);
                        command.Parameters.AddWithValue("@Dosage", Dosage);
                        command.Parameters.AddWithValue("@DurationDays", DurationDays);
                        command.Parameters.AddWithValue("@DurationMonths", DurationMonths ?? (object)DBNull.Value);

                        SqlParameter outputIdParam = new SqlParameter("@PrescriptionID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(outputIdParam);
                        command.ExecuteNonQuery();
                        PrescriptionID = (int)(outputIdParam.Value);
                    }
                }
            }
            catch (SqlException ex)
            {
                clsLoggerData.Log(ex.Message, System.Diagnostics.EventLogEntryType.Error);
            }
            return PrescriptionID ?? -1;
        }

        public static bool FindByPrescriptionID(int PrescriptionID, ref int AppointmentID, ref string MedicationName,
           ref string Dosage, ref byte DurationDays, ref byte? DurationMonths)
        {
            bool isFound = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingData.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_GetPrescriptionByID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@PrescriptionID", PrescriptionID);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                isFound = true;
                                AppointmentID = (int)reader["AppointmentID"];
                                MedicationName = (string)reader["MedicationName"];
                                Dosage = (string)reader["Dosage"];
                                DurationDays = (byte)reader["DurationDays"];
                                DurationMonths = (byte?)reader["DurationMonths"] == Convert.ToByte(DBNull.Value) ? null
                                    : (byte?)reader["DurationMonths"];
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

        public static bool FindByAppointmentID(int AppointmentID, ref int PrescriptionID, ref string MedicationName,
           ref string Dosage, ref byte DurationDays, ref byte? DurationMonths)
        {
            bool isFound = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingData.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_GetPrescriptionByAppointmentID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@AppointmentID", AppointmentID);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                isFound = true;
                                PrescriptionID = (int)reader["PrescriptionID"];
                                MedicationName = (string)reader["MedicationName"];
                                Dosage = (string)reader["Dosage"];
                                DurationDays = (byte)reader["DurationDays"];
                                DurationMonths = (byte?)reader["DurationMonths"];
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
