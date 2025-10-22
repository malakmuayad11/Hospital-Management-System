using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Hospital_Data
{
    public static class clsUserData
    {
        public async static Task<DataTable> GetAllUsersAsync()
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingData.ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SP_GetAllUsers", connection))
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

        public static int GetUsersCount()
        {
            int UsersCount = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingData.ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SP_GetUsersCount", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        UsersCount = Convert.ToInt32(command.ExecuteScalar());
                    }
                }
            }
            catch (SqlException ex)
            {
                clsLoggerData.Log(ex.Message, System.Diagnostics.EventLogEntryType.Error);
            }
            return UsersCount;
        }

        public static int AddNewUser(string Username, string Password, byte Role, int Permissions)
        {
            int? UserID = null;

            try {
                using (SqlConnection connection = new SqlConnection(clsSettingData.ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SP_AddNewUser", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Username", Username);
                        command.Parameters.AddWithValue("@Password", Password);
                        command.Parameters.AddWithValue("@Role", Role);
                        command.Parameters.AddWithValue("@Permissions", Permissions);

                        SqlParameter outputIdParam = new SqlParameter("@UserID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(outputIdParam);
                        command.ExecuteNonQuery();
                        UserID = (int)(outputIdParam.Value);
                    }
                }
            }
            catch (SqlException ex)
            {
                clsLoggerData.Log(ex.Message, System.Diagnostics.EventLogEntryType.Error);
            }
            return UserID ?? -1;
        }

        public static bool UpdateUser(int UserID, string Username, byte Role, int Permissions)
        {
            int rowsEffected = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingData.ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SP_UpdateUser", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@UserID", UserID);
                        command.Parameters.AddWithValue("@Username", Username);
                        command.Parameters.AddWithValue("@Role", Role);
                        command.Parameters.AddWithValue("@Permissions", Permissions);

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
    
        public static bool ChangePassword(int UserID, string Password)
        {
            int rowsEffected = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingData.ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SP_ChangePassword", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Password", Password);
                        command.Parameters.AddWithValue("@UserID", UserID);

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

        public static bool Find(int UserID, ref string Username, ref string Password,
            ref byte Role, ref DateTime LastLoginDate, ref int Permissions)
        {
            bool isFound = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingData.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_GetUserByID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@UserID", UserID);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                isFound = true;
                                Username = (string)reader["Username"];
                                Password = (string)reader["Password"];
                                Role = (byte)reader["Role"];
                                LastLoginDate = (DateTime)reader["LastLoginDate"];
                                Permissions = (int)reader["Permissions"];
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

        public static bool Find(string Username, string Password, ref int UserID,
            ref byte Role, ref DateTime LastLoginDate, ref int Permissions)
        {
            bool isFound = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingData.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_GetUserByUsernameAndPassword", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Username", Username);
                        command.Parameters.AddWithValue("@Password", Password);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                isFound = true;
                                UserID = (int)reader["UserID"];
                                Role = (byte)reader["Role"];
                                LastLoginDate = (DateTime)reader["LastLoginDate"];
                                Permissions = (int)reader["Permissions"];
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

        public static bool IsUsernameUsed(string Username)
        {
            int result = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingData.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_IsUsernameUsed", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Username", Username);

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

        public static bool DeleteUser(int UserID)
        {
            int Result = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingData.ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SP_DeleteUser", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@UserID", UserID);
                        Result = (int)command.ExecuteScalar();
                    }
                }
            }
            catch(SqlException ex)
            {
                clsLoggerData.Log(ex.Message, EventLogEntryType.Error);
            }
            return Result > 0;
        }

        public static int AddAsCurrentUser(int CurrentUserID)
        {
            int? Result = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingData.ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SP_AddAsCurrentUser", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@UserID", CurrentUserID);

                        SqlParameter outputIdParam = new SqlParameter("@Result", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(outputIdParam);
                        command.ExecuteNonQuery();
                        Result = (int)(outputIdParam.Value);
                    }
                }
            }
            catch (SqlException ex)
            {
                clsLoggerData.Log(ex.Message, System.Diagnostics.EventLogEntryType.Error);
            }
            return Result ?? -1;
        }

        public static bool UpdateUserLastLoginDate(int UserID)
        {
            int rowsEffected = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingData.ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SP_UpdateUserLastLoginDate", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@UserID", UserID);

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
    }
}
