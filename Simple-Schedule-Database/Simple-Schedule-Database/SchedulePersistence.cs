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
            string connectionString = ConfigurationManager.ConnectionStrings["scheduleDbConnectionString"].ConnectionString;
            conn = new MySqlConnection();
            try
            {
                conn.ConnectionString = connectionString;
                conn.Open();

                string sqlString =
                    $"INSERT INTO scheduletbl (Date, Activity, Locality) VALUES ('{schedule.Date:d}', '{schedule.Activity}', '{schedule.Locality}')";
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
            string connectionString = ConfigurationManager.ConnectionStrings["scheduleDbConnectionString"].ConnectionString;
            try
            {

                List<Schedule> scheduleList = new List<Schedule>();
                conn.ConnectionString = connectionString;
                conn.Open();
                string sqlQuery = "SELECT * FROM scheduletbl";
                MySqlCommand cmd=new MySqlCommand(sqlQuery, conn);
                MySqlDataReader reader = null;
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Schedule schedule = new Schedule()
                    {
                        ID = reader.GetInt32(0),
                        Date = Convert.ToDateTime(reader.GetString(1)),
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
            //string datePass = Convert.ToDateTime(date).ToString("D");
            MySqlConnection conn=new MySqlConnection();
            string connectionString = ConfigurationManager.ConnectionStrings["scheduleDbConnectionString"].ConnectionString;
            try
            {
                conn.ConnectionString = connectionString;
                conn.Open();
                string sqlQuery = $"SELECT * FROM scheduletbl WHERE ID = {id} ";
                MySqlCommand cmd = new MySqlCommand(sqlQuery, conn);
                MySqlDataReader reader = null;
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                        Schedule schedule = new Schedule()
                        {
                            ID = reader.GetInt32(0),
                            Date = Convert.ToDateTime(reader.GetString(1)),
                            Activity = reader.GetString(2),
                            Locality = reader.GetString(3)
                        };
                        return schedule;
                }
                else
                {
                    return null;
                }
                //return scheduleList;
          
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

        public List<Schedule> GetSchedules(string date)
        {
            MySqlConnection conn=new MySqlConnection();
            string connectionString = ConfigurationManager.ConnectionStrings["scheduleDbConnectionString"].ConnectionString;
            try
            {
                var scheduleList = new List<Schedule>();
                conn.ConnectionString = connectionString;
                conn.Open();
                string sqlQuery = $"SELECT * FROM scheduletbl WHERE Date = '{Convert.ToDateTime(date):D}'";
                MySqlCommand cmd = new MySqlCommand(sqlQuery, conn);
                MySqlDataReader reader = null;
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Schedule schedule = new Schedule()
                    {
                        ID = reader.GetInt32(0),
                        Date = Convert.ToDateTime(reader.GetInt32(1)),
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

        public bool UpdateSchedule(int id, Schedule schedule)
        {
            MySqlConnection conn=new MySqlConnection();
            string connectionString = ConfigurationManager.ConnectionStrings["scheduleDbConnectionString"].ConnectionString;
            try
            {
                conn.ConnectionString = connectionString;
                conn.Open();
                string sqlString = $"SELECT * FROM scheduletbl WHERE ID = {id}";
                MySqlDataReader reader = null;
                MySqlCommand cmd = new MySqlCommand(sqlString, conn);
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    reader.Close();
                    sqlString =
                        $"UPDATE scheduletbl SET Date = '{schedule.Date:D}', Activity = '{schedule.Activity}', Locality = '{schedule.Locality}' WHERE ID = {id}";
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

        public bool DeleteSchedule(int id)
        {
            MySqlConnection conn;
            string connectionString = ConfigurationManager.ConnectionStrings["scheduleDbConnectionString"].ConnectionString;
            conn=new MySqlConnection();
            try
            {
                conn.ConnectionString = connectionString;
                conn.Open();
                MySqlDataReader reader = null;
                string sqlQuery = $"SELECT * FROM scheduletbl WHERE ID = {id}";
                MySqlCommand cmd = new MySqlCommand(sqlQuery, conn);
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    reader.Close();
                    sqlQuery = $"DELETE FROM scheduletbl WHERE ID = {id}";
                    cmd = new MySqlCommand(sqlQuery, conn);
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