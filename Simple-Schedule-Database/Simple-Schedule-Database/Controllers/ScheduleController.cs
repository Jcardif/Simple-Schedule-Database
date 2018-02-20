using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Simple_Schedule_Database.Models;

namespace Simple_Schedule_Database.Controllers
{
    public class ScheduleController : ApiController
    {
        //GET: api/Schedule
        public List<Schedule> Get()
        {
            SchedulePersistence sp = new SchedulePersistence();
            return sp.GetSchedules();
        }

        //GET:api/schedule/date
        public List<Schedule> Get(string date)
        {
            SchedulePersistence sp=new SchedulePersistence();
            return sp.GetSchedules(date);
        }
        // GET: api/Schedule/5
       public Schedule Get(int id)
        {
            SchedulePersistence sp=new SchedulePersistence();
            if (sp.GetSchedule(id) == null)
            {
                throw new  HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
            }
            return sp.GetSchedule(id);
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
        public HttpResponseMessage Put(int id, [FromBody]Schedule schedule)
        {
            SchedulePersistence sp=new SchedulePersistence();
            bool isInSchedules = false;
            HttpResponseMessage response;
            isInSchedules = sp.UpdateSchedule(id, schedule);
            if (isInSchedules)
            {
                response = Request.CreateResponse(HttpStatusCode.NoContent);
            }
            else
            {
                response = Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return response;
        }

        // DELETE: api/Schedule/5
        public HttpResponseMessage Delete(int id)
        {
            SchedulePersistence sp=new SchedulePersistence();
            bool isInSchedule = false;
            HttpResponseMessage response;
            isInSchedule = sp.DeleteSchedule(id);
            if (isInSchedule)
            {
                response = Request.CreateResponse(HttpStatusCode.NoContent);
            }
            else
            {
                response = Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return response;
        }
    }
}
