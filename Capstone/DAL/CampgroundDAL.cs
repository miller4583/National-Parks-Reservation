using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.Models;
using System.Data.SqlClient;

namespace Capstone.DAL
{
    //test comment
    public class CampgroundDAL
    {
        private string connecetionString;

        public CampgroundDAL(string connectionString)
        {
            this.connecetionString = connectionString;
        }

        public Campground GetCampgroundById(int campgroundId)
        {
            Campground c = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(connecetionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("Select * from campground where campground_id = @campground_id", conn);
                    cmd.Parameters.AddWithValue("@campground_id", campgroundId);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        c = GetCampgroundFromReader(reader);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw;
            }

            return c;
        }
        
        public List<Campground> GetCampGroundsList(int parkId)
        {
            List<Campground> output = new List<Campground>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connecetionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("Select * from campground where park_id = @park_id", conn);
                    cmd.Parameters.AddWithValue("@park_id", parkId);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Campground c = GetCampgroundFromReader(reader);
                        output.Add(c);                        
                    }
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
            return output;
        }

        private Campground GetCampgroundFromReader(SqlDataReader reader)
        {
            Campground c = new Campground();
            c.CampgroundId = Convert.ToInt32(reader["campground_id"]);
            c.Name = Convert.ToString(reader["name"]);
            c.OpenFromMM = Convert.ToInt32(reader["open_from_mm"]);
            c.OpenToMM = Convert.ToInt32(reader["open_to_mm"]);
            c.DailyFee = Convert.ToDouble(reader["daily_fee"]);
            return c;
        }
    }
}
