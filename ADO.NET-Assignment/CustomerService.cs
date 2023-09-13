using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO.NET_Assignment
{
    internal class CustomerService
    {
        public static bool BookTicket(string customerName, string mobileNo, string emailId, int airTripsId)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultDBConnection"].ConnectionString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_Insert_Customers";
                command.Parameters.AddWithValue("@CustomerName", customerName);
                command.Parameters.AddWithValue("@MobileNo", mobileNo);
                command.Parameters.AddWithValue("@EmailId", emailId);
                command.Parameters.AddWithValue("@AirTripsId", airTripsId);
                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    Console.WriteLine("Record Inserted successfully!");
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    return false;
                }
            }
        }
        public static int UpdateRating(int airTripId, int rating)
        {
            return 0;
        }
        public static int CheckAvailability(string from, string to, DateTime departureDateTime)
        {
            return 0;
        }
        public static int FetchAirlinesRating(int airTripId)
        {
            return 0;
        }
    }
}
