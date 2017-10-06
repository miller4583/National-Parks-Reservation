using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.DAL;
using Capstone.Models;
using System.Globalization;

namespace Capstone
{
    public class ParkCLI
    {
        private string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=Park Capstone;User ID=te_student;Password=sqlserver1";

        public void run()
        {

            Tools.ColorfulWriteLine(@" _   _       _   _                   _   _____           _          _____",ConsoleColor.Green); 
            Tools.ColorfulWriteLine(@"| \ | |     | | (_)                 | | |  __ \         | |        |  __ \           ",ConsoleColor.Green);
            Tools.ColorfulWriteLine(@"|  \| | __ _| |_ _  ___  _ __   __ _| | | |__) |_ _ _ __| | _____  | |__) |___  ___  ",ConsoleColor.Green);
            Tools.ColorfulWriteLine(@"| . ` |/ _` | __| |/ _ \| '_ \ / _` | | |  ___/ _` | '__| |/ / __| |  _  // _ \/ __| ",ConsoleColor.Green);
            Tools.ColorfulWriteLine(@"| |\  | (_| | |_| | (_) | | | | (_| | | | |  | (_| | |  |   <\__ \ | | \ \  __/\__ \ ",ConsoleColor.Green);
            Tools.ColorfulWriteLine(@"|_| \_|\__,_|\__|_|\___/|_| |_|\__,_|_| |_|   \__,_|_|  |_|\_\___/ |_|  \_\___||___/ ",ConsoleColor.Green);
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("1 - Show all Parks");
                Console.WriteLine("2 - Show all Campgrounds");
                Console.WriteLine("3 - See if reservation is possible at your desired campground");
                Console.WriteLine("4 - Make Reservation");

                string input = CLIHelper.GetString("Make your choice: ");
                Console.WriteLine();

                switch (input.ToLower())
                {
                    case "1":
                        ShowAllParks();
                        break;

                    case "2":
                        ShowAllCampgrounds();
                        break;

                    case "3":
                        FindAvailableCampSites();
                        break;

                    case "4":
                        MakeReservation();
                        break;

                }

            }

        }

        private void ShowAllParks()
        {
            Console.WriteLine("SHOWING ALL PARKS");

            ParkDAL dal = new ParkDAL(connectionString);

            List<Parks> parksList = dal.GetListOfParks();

            foreach (Parks p in parksList)
            {
                Tools.ColorfulWriteLine("Park ID".PadRight(10) + "Park Name".PadRight(20) + "Location".PadRight(10) + "Date Established".PadRight(30) + "Area".PadRight(10) + "Annual Vistors".PadRight(10), ConsoleColor.Green);
                Console.WriteLine($"{p.Id} )".PadRight(10) + $"{p.Name}".PadRight(20) + $"{p.Location}".PadRight(10) + $"{p.EstablishedDate}".PadRight(30) + $"{p.Area}".PadRight(10) + $"{p.Visitors}");
                Console.WriteLine();
                Tools.ColorfulWriteLine("Park Description", ConsoleColor.Yellow);
                Console.WriteLine(p.Description);
                Console.WriteLine();
            }

        }

        private void ShowAllCampgrounds()
        {
            int park_id = CLIHelper.GetInteger("Please Select Park ID ");
            Console.WriteLine();
            Console.WriteLine("Showing All Campgrounds in Selected Park:");
            Console.WriteLine();
            CampgroundDAL dal = new CampgroundDAL(connectionString);
            List<Campground> listOfCamps = dal.GetCampGroundsList(park_id);

            foreach (Campground c in listOfCamps)
            {
                System.Globalization.DateTimeFormatInfo gmn = new
                System.Globalization.DateTimeFormatInfo();
                string strMonthName = gmn.GetMonthName(c.open_from_mm).ToString();
                string endMonthName = gmn.GetMonthName(c.open_to_mm).ToString();

                Console.WriteLine();
                Tools.ColorfulWriteLine("Campground ID".PadRight(20) + "Name".PadRight(35) + "Opens".PadRight(10) + "Closes".PadRight(15) + "Daily Fee", ConsoleColor.Green);
                Console.WriteLine($"{c.campground_id})".PadRight(20) + $"{c.name}".PadRight(35) + $"{strMonthName}".PadRight(10) + $"{endMonthName}".PadRight(15) + $"{c.daily_fee}");
                Console.WriteLine();
            }

        }
        
        private void FindAvailableCampSites()
        {

            int campground_id = CLIHelper.GetInteger("Please Select Campground ID");
            Console.WriteLine();
            DateTime startDate = CLIHelper.GetDateTime("Please Select Start of Stay (yyyy/mm/dd)");
            Console.WriteLine();
            DateTime endDate = CLIHelper.GetDateTime("Please Select Date of Depature (yyyy/mm/dd)");

            SiteDal dal = new SiteDal(connectionString);
            List<Site> sites;

            bool avail = dal.IsSiteAvailable(campground_id, startDate, endDate);
            if (!avail)
            {
                Tools.ColorfulWriteLine("No availablity please try different dates or Campground", ConsoleColor.Red);
            }
            else if (avail)
            {
                sites = dal.GetTop5(campground_id, startDate, endDate);
                foreach (Site s in sites)
                {
                    Console.WriteLine();
                    Console.WriteLine($"National Site ID {s.site_id}  Campground Site Number {s.site_number} Max Occupancy {s.max_occupancy} Total Days {s.totalDays} Total Cost ${s.totalCost}");
                }
            }
        }
        
        private void MakeReservation()
        {
            throw new NotImplementedException();
        }
             
    }
}
