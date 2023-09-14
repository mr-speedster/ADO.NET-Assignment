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
        private static SqlConnection GetConnection()
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultDBConnection"].ConnectionString);
            connection.Open();
            return connection;
        }
        private static int ExecuteStoredProcedure(string storedProcedureName, List<SqlParameter> parameters)
        {
            using (SqlConnection connection = GetConnection())
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = storedProcedureName;
                    command.Parameters.AddRange(parameters.ToArray());
                    try
                    {
                        return command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                        return -1;
                    }
                }
            }
        }

        public static bool BookTicket(string customerName, string mobileNo, string emailId, int airTripsId)
        {
            List<SqlParameter> parameters = new List<SqlParameter>
        {
            new SqlParameter("@CustomerName", customerName),
            new SqlParameter("@MobileNo", mobileNo),
            new SqlParameter("@EmailId", emailId),
            new SqlParameter("@AirTripsId", airTripsId)
        };

            int result = ExecuteStoredProcedure("sp_Insert_Customers", parameters);

            if (result > 0)
            {
                Console.WriteLine("Record Inserted successfully!");
                return true;
            }

            return false;
        }

        public static int UpdateRating(int airTripId, int customerId, int rating)
        {
            List<SqlParameter> parameters = new List<SqlParameter>
        {
            new SqlParameter("@AirTripId", airTripId),
            new SqlParameter("@CustomerId", customerId),
            new SqlParameter("@Rating", rating)
        };

            int result = ExecuteStoredProcedure("sp_Update_Rating", parameters);
            if (result > 0)
            {
                Console.WriteLine("Record updated successfully!");
                return 1;
            }
            return -1;
        }

        public static int CheckAvailability(string from, string to, DateTime departureDateTime)
        {
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@From", from),
                new SqlParameter("@To", to),
                new SqlParameter("@DepartureDateTime", departureDateTime)
            };

            SqlParameter rowCount = new SqlParameter("@rowCount", SqlDbType.Int);
            rowCount.Direction = ParameterDirection.Output;
            parameters.Add(rowCount);

            ExecuteStoredProcedure("sp_Check_Availability", parameters);
            if ((int)rowCount.Value > 0)
            {
                return (int)rowCount.Value;
            }
            return -1;
        }

        public static int? FetchAirlinesRating(int airTripId)
        {
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@AirTripId", airTripId)
            };

            SqlParameter rating = new SqlParameter("@Rating", SqlDbType.Int);
            rating.Direction = ParameterDirection.Output;
            parameters.Add(rating);

            ExecuteStoredProcedure("sp_Get_Rating", parameters);

            if ((int)rating.Value > 0)
            {
                if (DBNull.Value.Equals(rating.Value))
                {
                    return null;
                }
                else
                {
                    return (int)rating.Value;
                }
            }

            return null;
        }
    }

}
