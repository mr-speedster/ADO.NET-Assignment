using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Net;
using System.Configuration;
using System.Data;

namespace ADO.NET_Assignment
{
    internal class AirTrip
    {
        public static bool AddNewAirTrip(string airLines, int availability, DateTime departureDateTime, string from, string to)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultDBConnection"].ConnectionString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "Insert_AirTrip";
                command.Parameters.AddWithValue("@Airlines", airLines);
                command.Parameters.AddWithValue("@Availability", availability);
                command.Parameters.AddWithValue("@DepartureDateTime", departureDateTime);
                command.Parameters.AddWithValue("@FromLocation", from);
                command.Parameters.AddWithValue("@ToLocation", to);
                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    Console.WriteLine("Record inserted successfully!");
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    return false;
                }
            }
        }
        
        public static bool UpdateAvailability(int airTripId, int availability)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultDBConnection"].ConnectionString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_Update_Availability";
                command.Parameters.AddWithValue("@airTripId", airTripId);
                command.Parameters.AddWithValue("@availability", availability);
                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    Console.WriteLine("Record updated successfully!");
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
}
