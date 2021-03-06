﻿using System;
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
            catch (SqlException ex)
            {
                throw;
            }
        }

        public Reservation GetReservationNumber(int siteId, string name, DateTime fromDate, DateTime toDate)
        {
            Reservation r = null;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_Get_Res_Num, conn);
                    cmd.Parameters.AddWithValue("@siteId", siteId);
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@fromDate", fromDate);
                    cmd.Parameters.AddWithValue("@toDate", toDate);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        r = new Reservation();
                        r.createDate = Convert.ToDateTime(reader["create_date"]);
                        r.siteId = Convert.ToInt32(reader["site_id"]);
                        r.name = Convert.ToString(reader["name"]);
                        r.fromDate = Convert.ToDateTime(reader["from_date"]);
                        r.toDate = Convert.ToDateTime(reader["to_date"]);
                        r.reservationId = Convert.ToInt32(reader["reservation_id"]);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
            return r;
        }
    }
}
