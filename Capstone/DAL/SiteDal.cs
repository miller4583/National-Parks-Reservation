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
        private string SQL_List_Top_5 = @"select Top 5 * from site join campground on site.campground_id = campground.campground_id where site.campground_id = @campground_id and site.site_id not in(select reservation.site_id from reservation join site on site.site_id = reservation.site_id where site.campground_id = @campground_id and (from_date >= @startDate or from_date <=@startDate) or (to_date >= @endDate or to_date <= @endDate) or (from_date <= @startDate and to_date >= @endDate))";
        private string SQL_MaxOccupancy = @"Select site.max_occupancy from site Where site.site_id = @site_id";
        private string SQL_Accessible = @"Select site.accessible from site where site.site_id = @site_id and site.accessible = 1";
        private string SQL_MaxRV = @"Select site.max_rv_length from site where site.site_id = @site_id";
        private string SQL_Utilites = @"Select site.utilities from site where site.site_id = @site_id and site.utilities = 1";
        private string SQL_GetSiteById = @"SELECT * FROM site WHERE site.site_id = @site_id";

        public SiteDal(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public bool IsSiteAvailable(int campground_id, DateTime startDate, DateTime endDate)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_Is_Date_Avail, conn);
                    cmd.Parameters.AddWithValue("@campground_id", campground_id);
                    cmd.Parameters.AddWithValue("@startDate", startDate);
                    cmd.Parameters.AddWithValue("@endDate", endDate);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    return (rowsAffected < 7);
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
        }
        public List<Site> GetTop5(int campground_id, DateTime startDate, DateTime endDate)
        {
            List<Site> output = new List<Site>();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(SQL_List_Top_5, con);
                    cmd.Parameters.AddWithValue("@campground_id", campground_id);
                    cmd.Parameters.AddWithValue("@startDate", startDate);
                    cmd.Parameters.AddWithValue("@endDate", endDate);
                    SqlDataReader reader = cmd.ExecuteReader();
                    int totalDays = (endDate - startDate).Days;

                    while (reader.Read())
                    {
                        Site s = new Site();
                        s.site_id = Convert.ToInt32(reader["site_id"]);
                        s.campground_id = Convert.ToInt32(reader["campground_id"]);
                        s.site_number = Convert.ToInt32(reader["site_number"]);
                        s.max_occupancy = Convert.ToInt32(reader["max_occupancy"]);
                        s.totalDays = totalDays;
                        s.totalCost = Convert.ToDecimal(totalDays * Convert.ToInt32(reader["daily_fee"]));
                        output.Add(s);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
            return output;
        }

        public Site GetSite(int siteId)
        {
            Site s = null;

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(SQL_GetSiteById, con);
                    cmd.Parameters.AddWithValue("@site_id", siteId);
                    SqlDataReader reader = cmd.ExecuteReader();
                    
                    if (reader.Read())
                    {
                        s = new Site();
                        s.site_id = Convert.ToInt32(reader["site_id"]);
                        s.campground_id = Convert.ToInt32(reader["campground_id"]);
                        s.site_number = Convert.ToInt32(reader["site_number"]);
                        s.max_occupancy = Convert.ToInt32(reader["max_occupancy"]);
                        s.maxRVLength = Convert.ToInt32(reader["max_rv_length"]);
                        s.utilities = Convert.ToBoolean(reader["utilities"]);
                        s.accesible = Convert.ToBoolean(reader["accessible"]);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw;
            }

            return s;
        }


        public List<Site> ListOfMaxOccupancy(int siteID)
        {
            List<Site> maxOc = new List<Site>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_MaxOccupancy, conn);
                    cmd.Parameters.AddWithValue("@site_id", siteID);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Site s = new Site();

                        s.max_occupancy = Convert.ToInt32(reader["max_occupancy"]);
                        maxOc.Add(s);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
            return maxOc;
        }

        public List<Site> ListIsSiteAccessible(int siteID)
        {
            List<Site> isAccessible = new List<Site>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_Accessible, conn);
                    cmd.Parameters.AddWithValue("@site_id", siteID);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Site s = new Site();
                        s.accesible = Convert.ToBoolean(reader["accessible"]);

                        isAccessible.Add(s);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
            return isAccessible;
        }

        public List<Site> ListOfMaxRVLength(int siteID)
        {
            List<Site> rvLegth = new List<Site>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_MaxRV, conn);
                    cmd.Parameters.AddWithValue("@site_id", siteID);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Site rv = new Site();
                        rv.maxRVLength = Convert.ToInt32(reader["max_rv_length"]);
                        rvLegth.Add(rv);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
            return rvLegth;
        }

        public List<Site> ListAreThereUtilities(int siteID)
        {
            List<Site> areUtilites = new List<Site>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_Utilites, conn);
                    cmd.Parameters.AddWithValue("@site_id", siteID);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Site u = new Site();
                        u.accesible = Convert.ToBoolean(reader["utilities"]);
                        areUtilites.Add(u);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
            return areUtilites;
        }
    }
}

