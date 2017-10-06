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
    public class ReservationDAL
    {
        private string connectionString;
        private string SQL_Make_Res = @"insert into reservation values(@siteId, @name, @fromDate, @toDate, @createDate);";
        private string SQL_Get_Res_Num = @" select * from reservation where reservation.site_id = @siteId and reservation.name = @name and reservation.from_date = @fromDate and reservation.to_date = @toDate;";
        public ReservationDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public bool MakeReservation(int siteId, string name, DateTime fromDate, DateTime toDate, DateTime createDate)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(this.connectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(SQL_Make_Res, con);
                    cmd.Parameters.AddWithValue("@siteId", siteId);
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@fromDate", fromDate);
                    cmd.Parameters.AddWithValue("@toDate", toDate);
                    cmd.Parameters.AddWithValue("@createDate", createDate);
                    int rowsaffected = cmd.ExecuteNonQuery();

                    return (rowsaffected > 0);
                }
            }
            catch(SqlException ex)
            {
                throw;
            }
        }

        public Reservation GetReservationNumber(int siteId, string name, DateTime fromDate, DateTime toDate)
        {
            //List<Reservation> list = new List<Reservation>();
            Reservation r = new Reservation();
            try
            {
                using(SqlConnection conn= new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_Get_Res_Num, conn);
                    cmd.Parameters.AddWithValue("@siteId", siteId);
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@fromDate", fromDate);
                    cmd.Parameters.AddWithValue("@toDate", toDate);
                    SqlDataReader readerTwo = cmd.ExecuteReader();
                    while (readerTwo.Read())
                    {
                        
                        r.createDate = Convert.ToDateTime(readerTwo["create_date"]);
                        r.siteId = Convert.ToInt32(readerTwo["site_id"]);
                        r.name = Convert.ToString(readerTwo["name"]);
                        r.fromDate = Convert.ToDateTime(readerTwo["from_date"]);
                        r.toDate = Convert.ToDateTime(readerTwo["to_date"]);
                        r.reservationId = Convert.ToInt32(readerTwo["reservation_id"]);
                        //list.Add(r);
                    }

                }
            }
            catch(SqlException ex)
            {
                throw;
            }
            return r;
        }
    }
}
