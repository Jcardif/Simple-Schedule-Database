using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Simple_Schedule_Database.Models
{
    public class Schedule
    {
        public DateTime Date { get; set; }
        public string Activity { get; set; }
        public string Locality { get; set; }
        public int ID { get; set; } 
    }
}