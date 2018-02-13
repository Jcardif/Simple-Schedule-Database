using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Simple_Schedule_Database.Models;

namespace Simple_Schedule_Database.Controllers
{
    public class ScheduleController : ApiController
    {
        // GET: api/Schedule
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Schedule/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Schedule
        public HttpResponseMessage Post([FromBody]Schedule schedule)
        {
            SchedulePersistence sp = new SchedulePersistence();
            int id = sp.CreateNewSchedule(schedule);
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.Created);
            response.Headers.Location = new Uri(Request.RequestUri, string.Format($"Schedule/{id}"));
            return response;
        }

        // PUT: api/Schedule/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Schedule/5
        public void Delete(int id)
        {
        }
    }
}
