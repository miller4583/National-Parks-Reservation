using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.DAL;
using Capstone.Models;

namespace Capstone
{
    public class AdvancedSearchMenu
    {
        private string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=Park Capstone;User ID=te_student;Password=sqlserver1";

        public void Display()
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("1 - Max Occupancy");
                Console.WriteLine("2 - Accessible");
                Console.WriteLine("3 - Max RV Length");
                Console.WriteLine("4 - Are utilities provided");
                Console.WriteLine("5 - Return to Main Menu");
                Console.WriteLine();

                string input = CLIHelper.GetString("Make your choice:");
                Console.WriteLine();

                switch (input.ToLower())
                {
                    case "1":
                        int site_id = CLIHelper.GetInteger("Please Select Site ID ");
                        SiteDal maxGet = new SiteDal(connectionString);
                        ShowMaxOccupancy(maxGet.ListOfMaxOccupancy(site_id), site_id);
                        break;

                    case "2":
                        int site_idONE = CLIHelper.GetInteger("Please Select Site ID ");
                        SiteDal accessible = new SiteDal(connectionString);
                        IsItAccessible(accessible.ListIsSiteAccessible(site_idONE), site_idONE);
                        break;

                    case "3":
                        int maxRVId = CLIHelper.GetInteger("Please Select Site ID ");
                        SiteDal maxRV = new SiteDal(connectionString);
                        MaxRvLength(maxRV.ListOfMaxRVLength(maxRVId), maxRVId);
                        break;

                    case "4":
                        int utilityID = CLIHelper.GetInteger("Please Select Site ID ");
                        SiteDal utility = new SiteDal(connectionString);
                        AreThereUtilities(utility.ListAreThereUtilities(utilityID), utilityID);
                        break;

                    case "5":
                        ParkCLI mainmenu = new ParkCLI();
                        mainmenu.run();
                        break;
                }

            }
        }

        private void ShowMaxOccupancy(List<Site> maxes, int site)
        {
            SiteDal dal = new SiteDal(connectionString);

            if (maxes.Count > 0)
            {
                foreach (Site occupancy in maxes)
                {
                    Tools.ColorfulWriteLine($"The max occupancy of site {site} is {occupancy.max_occupancy}", ConsoleColor.Green);
                }
            }

        }

        private void IsItAccessible(List<Site> accessible, int site)
        {
            SiteDal dal = new SiteDal(connectionString);

            if (accessible.Count > 0)
            {
                Tools.ColorfulWriteLine($"{site} is accessible", ConsoleColor.Green);
            }
            else
            {
                Tools.ColorfulWriteLine($"Site {site} is not accessible", ConsoleColor.Red);
            }
        }

        private void MaxRvLength(List<Site> rvLength, int site)
        {
            SiteDal dal = new SiteDal(connectionString);

            if (rvLength.Count > 0)
            {
                foreach (Site rv in rvLength)
                {
                    Tools.ColorfulWriteLine($"The max rv length of site {site} is {rv.maxRVLength} feet", ConsoleColor.Green);
                }
            }
        }

        private void AreThereUtilities(List<Site> utilities, int siteID)
        {
            SiteDal dal = new SiteDal(connectionString);

            if (utilities.Count > 0)
            {
                Tools.ColorfulWriteLine($"{siteID} has utilities", ConsoleColor.Green);
            }
            else
            {
                Tools.ColorfulWriteLine($"Site {siteID} does not have utilities", ConsoleColor.Red);
            }
        }
    }
}
