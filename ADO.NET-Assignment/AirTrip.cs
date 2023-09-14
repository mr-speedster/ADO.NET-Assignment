using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ADO.NET_Assignment
{
    internal class AirTrip
    {
        private static SqlConnection GetConnection()
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultDBConnection"].ConnectionString);
            connection.Open();
            return connection;
        }
        private static bool ExecuteStoredProcedure(string storedProcedureName, params SqlParameter[] parameters)
        {
            using (SqlConnection connection = GetConnection())
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = storedProcedureName;
                    command.Parameters.AddRange(parameters);
                    try
                    {
                        command.ExecuteNonQuery();
                        Console.WriteLine("Record operation successful!");
                        return true;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                        return false;
                    }
                }
            }
        }
        public static bool AddNewAirTrip(string airLines, int availability, DateTime departureDateTime, string from, string to)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@Airlines", airLines),
                new SqlParameter("@Availability", availability),
                new SqlParameter("@DepartureDateTime", departureDateTime),
                new SqlParameter("@FromLocation", from),
                new SqlParameter("@ToLocation", to)
            };

            return ExecuteStoredProcedure("Insert_AirTrip", parameters);
        }
        public static bool UpdateAvailability(int airTripId, int availability)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@airTripId", airTripId),
                new SqlParameter("@availability", availability)
            };

            return ExecuteStoredProcedure("sp_Update_Availability", parameters);
        }
    }
}
