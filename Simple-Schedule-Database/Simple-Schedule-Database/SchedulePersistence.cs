using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using Simple_Schedule_Database.Models;

namespace Simple_Schedule_Database
{
    public class SchedulePersistence
    {
        public int CreateNewSchedule(Schedule schedule)
        {
            MySqlConnection conn;
            string connectionString =
                ConfigurationManager.ConnectionStrings["scheduleDbConnectionString"].ConnectionString;
            conn=new MySqlConnection();
            try
            {
                conn.Open();
                conn.ConnectionString = connectionString;

                string sqlString =
                    $"INSERT INTO scheduletbl (Date, Activity, Locality) VALUES ({schedule.Date:D}, {schedule.Activity}, {schedule.Locality})";
                MySqlCommand cmd = new MySqlCommand(sqlString, conn);
                cmd.ExecuteNonQuery();
                int id = Convert.ToInt32(cmd.LastInsertedId);
                return id;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }
    }
}