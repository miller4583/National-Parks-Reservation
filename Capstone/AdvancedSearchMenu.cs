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
               // int siteID = CLIHelper.GetInteger("Select a site id");

                Console.WriteLine();
                Console.WriteLine("1 - Max Occupancy");
                Console.WriteLine("2 - Accessible");
                Console.WriteLine("3 - Max RV Length");
                Console.WriteLine("4 - Are utilities provided");

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
                        IsItAccessible();
                        break;

                    case "3":
                        MaxRvLength();
                        break;

                    case "4":
                        AreThereUtilities();
                        break;


                }


            }
        }

        private void ShowMaxOccupancy(List<Site> maxes, int site)
        {


            
            SiteDal dal = new SiteDal(connectionString);
            //List<Site> site = dal.ShowMaxOccupancy(site_id);

            if(maxes.Count > 0)
            {
                foreach (Site occupancy in maxes)
                {
                    Console.WriteLine($"The max occupancy of site {site} is {occupancy.max_occupancy}");

                }
            }
                                
        }

        private void IsItAccessible()
        {
            throw new NotImplementedException();
        }

        private void MaxRvLength()
        {
            throw new NotImplementedException();
        }

        private void AreThereUtilities()
        {
            throw new NotImplementedException();
        }
    }
}
