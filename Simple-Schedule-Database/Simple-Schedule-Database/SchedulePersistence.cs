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
        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["scheduleDbConnectionString"].ConnectionString;

        public int CreateNewSchedule(Schedule schedule)
        {
            MySqlConnection conn;
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

        public List<Schedule> GetSchedules()
        {
            MySqlConnection conn=new MySqlConnection();
            try
            {

                List<Schedule> scheduleList = new List<Schedule>();

                conn.Open();
                conn.ConnectionString = connectionString;
                string sqlQuery = "SELECT * FROM scheduletbl";
                MySqlCommand cmd=new MySqlCommand(sqlQuery, conn);
                MySqlDataReader reader = null;
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Schedule schedule = new Schedule()
                    {
                        ID = reader.GetInt32(0),
                        Date = reader.GetDateTime(1),
                        Activity = reader.GetString(2),
                        Locality = reader.GetString(3)
                    };
                    scheduleList.Add(schedule);
                }
                return scheduleList;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        public Schedule GetSchedule(int id)
        {
            MySqlConnection conn=new MySqlConnection();
            try
            {
                conn.Open();
                conn.ConnectionString = connectionString;
                Schedule schedule;
                string sqlQuery = $"SELECT * FROM scheduletbl WHERE ID = {id} ";
                MySqlCommand cmd = new MySqlCommand(sqlQuery, conn);
                MySqlDataReader reader = null;
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    schedule = new Schedule()
                    {
                        ID = reader.GetInt32(0),
                        Date = reader.GetDateTime(1),
                        Activity = reader.GetString(2),
                        Locality = reader.GetString(3)
                    };
                    return schedule;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
            finally
            {
                conn.Close();
            }
        }
    }
}