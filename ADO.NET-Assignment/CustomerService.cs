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
        public static int UpdateRating(int airTripId, int customerId, int rating)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultDBConnection"].ConnectionString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_Update_Rating";
                command.Parameters.AddWithValue("@AirTripId", airTripId);
                command.Parameters.AddWithValue("@CustomerId", customerId);
                command.Parameters.AddWithValue("@Rating", rating);
                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    Console.WriteLine("Record updated successfully!");
                    return 1;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    return -1;
                }
            }
        }
        public static int CheckAvailability(string from, string to, DateTime departureDateTime)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultDBConnection"].ConnectionString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_Check_Availability";
                command.Parameters.AddWithValue("@From", from);
                command.Parameters.AddWithValue("@To", to);
                command.Parameters.AddWithValue("@DepartureDateTime", departureDateTime);
                SqlParameter rowCount = new SqlParameter("@rowCount", SqlDbType.Int);
                rowCount.Direction = ParameterDirection.Output;
                command.Parameters.Add(rowCount);
                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    if ((int)rowCount.Value > 0)
                        return (int)rowCount.Value;
                    else
                        return -1;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    return -1;
                }
            }
        }
        public static int? FetchAirlinesRating(int airTripId)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultDBConnection"].ConnectionString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_Get_Rating";
                command.Parameters.AddWithValue("@AirTripId", airTripId);
                SqlParameter rating = new SqlParameter("@Rating", SqlDbType.Int);
                rating.Direction = ParameterDirection.Output;
                command.Parameters.Add(rating);
                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    if (DBNull.Value.Equals(rating.Value))
                    {
                        return null;
                    }
                    else
                    {
                        return (int)rating.Value;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    return null;
                }
            }
        }
    }
}
