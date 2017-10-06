using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.Models;
using System.Data.SqlClient;

namespace Capstone.DAL
{
    public class CampgroundDAL
    {
        private string connecetionString;

        public CampgroundDAL(string connectionString)
        {
            this.connecetionString = connectionString;
        }

        public List<Campground> GetCampGroundsList(int park_id)
        {
            List<Campground> output = new List<Campground>();

            try
            {
                using(SqlConnection conn = new SqlConnection(connecetionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("Select * from campground where park_id = @park_id", conn);
                    cmd.Parameters.AddWithValue("@park_id", park_id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Campground c = new Campground();
                        c.campground_id = Convert.ToInt32(reader["campground_id"]);
                        c.name = Convert.ToString(reader["name"]);
                        c.open_from_mm = Convert.ToInt32(reader["open_from_mm"]);
                        c.open_to_mm = Convert.ToInt32(reader["open_to_mm"]);
                        c.daily_fee = Convert.ToDouble(reader["daily_fee"]);
                        output.Add(c);
                    }
                    
                }
            }
            catch(SqlException ex)
            {
                throw;
            }
            return output;
        }
                        
    }
}
