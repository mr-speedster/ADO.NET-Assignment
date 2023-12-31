﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO.NET_Assignment
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            AirTrip.AddNewAirTrip("airLines2", 50, DateTime.Now, "Delhi", "Ernakulam");
            AirTrip.UpdateAvailability(10001, 60);
            CustomerService.BookTicket("customerNamettt", "1636567890", "dadds@mail.com", 10002);
            CustomerService.UpdateRating(10001, 29, 10);
            int availability = CustomerService.CheckAvailability("TVM", "Ernakulam", DateTime.Parse("2023-09-14 00:53:18.620"));
            Console.WriteLine(availability);
            var rating = CustomerService.FetchAirlinesRating(10001);
            Console.WriteLine(rating);
        }
    }
}