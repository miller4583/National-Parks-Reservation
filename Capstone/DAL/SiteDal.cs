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
    public class SiteDal
    {
        private string connectionString;
        private string SQL_Is_Date_Avail = @"select * from site join reservation on reservation.site_id = site.site_id where reservation.from_date =  @startDate and reservation.to_date = @endDate and site.campground_id = @campground_id;";
        private string SQL_List_Top_5 = @"select top 5 site.max_occupancy, site.site_id, site.site_number from site where site.campground_id = @campground_id;";
        public SiteDal(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public bool IsSiteAvailable(int campground_id, DateTime startDate, DateTime endDate)
        {
            try
            {
                using(SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_Is_Date_Avail, conn);
                    cmd.Parameters.AddWithValue("@campground_id", campground_id);
                    cmd.Parameters.AddWithValue("@startDate", startDate);
                    cmd.Parameters.AddWithValue("@endDate", endDate);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    return (rowsAffected < 6);

                }

            }
            catch(SqlException ex)
            {
                throw;
            }

        }
        public List<Site> GetTop5(int campground_id)
        {
            List<Site> output = new List<Site>();

            try
            {
                using(SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(SQL_List_Top_5, con);
                    cmd.Parameters.AddWithValue("@campground_id", campground_id);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Site s = new Site();
                        s.site_id = Convert.ToInt32(reader["site_id"]);
                        s.site_number = Convert.ToInt32(reader["site_number"]);
                        s.max_occupancy = Convert.ToInt32(reader["max_occupancy"]);
                        output.Add(s);

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
