using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.DAL;
using Capstone.Models;

namespace Capstone
{
    public class ParkCLI
    {
        private string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=Park Capstone;User ID=te_student;Password=sqlserver1";

        public void run()
        {

            Console.WriteLine(@" _   _       _   _                   _   _____           _          _____");
            Console.WriteLine(@"| \ | |     | | (_)                 | | |  __ \         | |        |  __ \           ");
            Console.WriteLine(@"|  \| | __ _| |_ _  ___  _ __   __ _| | | |__) |_ _ _ __| | _____  | |__) |___  ___  ");
            Console.WriteLine(@"| . ` |/ _` | __| |/ _ \| '_ \ / _` | | |  ___/ _` | '__| |/ / __| |  _  // _ \/ __| ");
            Console.WriteLine(@"| |\  | (_| | |_| | (_) | | | | (_| | | | |  | (_| | |  |   <\__ \ | | \ \  __/\__ \ ");
            Console.WriteLine(@"|_| \_|\__,_|\__|_|\___/|_| |_|\__,_|_| |_|   \__,_|_|  |_|\_\___/ |_|  \_\___||___/ ");
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("1 - Show all Parks");
                Console.WriteLine("2 - Show all Campgrounds");
                Console.WriteLine("3 - Find avaiable camp site");
                Console.WriteLine("4 - Make Reservation");

                string input = CLIHelper.GetString("Make your choice: ");

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
                Console.WriteLine("Park ID Park Name Location Date Established Area Annual Vistors");
                Console.WriteLine($"{p.Id} ) {p.Name} {p.Location} {p.EstablishedDate} {p.Area} {p.Visitors} ");
                Console.WriteLine();
                Console.WriteLine("Park Description");
                Console.WriteLine(p.Description);
                Console.WriteLine();
            }

        }


        private void ShowAllCampgrounds()
        {
            int park_id = CLIHelper.GetInteger("Please Select Park ID ");
            Console.WriteLine("Showing All Campgrounds in Selected Park:");
            Console.WriteLine();
            CampgroundDAL dal = new CampgroundDAL(connectionString);
            List<Campground> listOfCamps = dal.GetCampGroundsList(park_id);

            foreach (Campground c in listOfCamps)
            {
                Console.WriteLine();
                Console.WriteLine("Campground ID Campground Name Month Opens Month Closes Daily Fee");
                Console.WriteLine($"{c.campground_id})      {c.name}    {c.open_from_mm}    {c.open_to_mm}      ${c.daily_fee}  ");
                Console.WriteLine();

            }

        }


        private void FindAvailableCampSites()
        {

            int campground_id = CLIHelper.GetInteger("Please Select Campground ID");
            DateTime startDate = CLIHelper.GetDateTime("Please Select Start of Stay");
            DateTime endDate = CLIHelper.GetDateTime("Please Select Date of Depature");

            SiteDal dal = new SiteDal(connectionString);
            List<Site> sites;

            bool avail = dal.IsSiteAvailable(campground_id, startDate, endDate);
            if (!avail)
            {
                Console.WriteLine("No availablity please try different dates or Campground");
            }
            else if (avail)
            {
                sites = dal.GetTop5(campground_id);
                foreach (Site s in sites)
                {
                    Console.WriteLine();
                    Console.WriteLine($"National Site ID {s.site_id}  Campground Site Number {s.site_number} Max Occupancy {s.max_occupancy} ");
                }
            }            
        }


        private void MakeReservation()
        {
            throw new NotImplementedException();
        }
    }
}
