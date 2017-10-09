using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.DAL;
using Capstone.Models;
using System.Data.SqlClient;


namespace Capstone.DAL
{
    public class ParkDAL
    {
        private string connectionString;

        public ParkDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Parks> GetListOfParks()
        {
            List<Parks> output = new List<Parks>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT * FROM park", conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Parks p = new Parks();
                        p.Id = Convert.ToInt32(reader["park_id"]);
                        p.Name = Convert.ToString(reader["name"]);
                        p.Location = Convert.ToString(reader["location"]);
                        p.EstablishedDate = Convert.ToDateTime(reader["establish_date"]);
                        p.Area = Convert.ToInt32(reader["area"]);
                        p.Visitors = Convert.ToInt32(reader["visitors"]);
                        p.Description = Convert.ToString(reader["description"]);

                        output.Add(p);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw;
            }

            return output;
        }
    }
}
