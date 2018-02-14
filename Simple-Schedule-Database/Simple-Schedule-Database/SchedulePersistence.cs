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

        public List<Schedule> GetSchedule(string date)
        {
            MySqlConnection conn=new MySqlConnection();
            try
            {
                conn.Open();
                conn.ConnectionString = connectionString;
                List<Schedule> scheduleList=new List<Schedule>();
                string sqlQuery = $"SELECT * FROM scheduletbl WHERE Date = {date} ";
                MySqlCommand cmd = new MySqlCommand(sqlQuery, conn);
                MySqlDataReader reader = null;
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {

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

        public bool UpdateSchedule(int id, Schedule schedule)
        {
            MySqlConnection conn=new MySqlConnection();
            try
            {
                conn.Open();
                conn.ConnectionString = connectionString;
                string sqlString = $"SELECT * FROM scheduletbl WHERE ID = {id}";
                MySqlDataReader reader = null;
                MySqlCommand cmd = new MySqlCommand(sqlString, conn);
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    reader.Close();
                    sqlString =
                        $"UPDATE scheduletbl SET Date = {schedule.Date}, Activity = {schedule.Activity}, Locality = {schedule.Locality} WHERE ID = {Id}";
                    cmd=new MySqlCommand(sqlString, conn);
                    cmd.ExecuteNonQuery();
                    return true;
                }
                else
                {
                    return false;
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