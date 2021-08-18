using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Day3.Models;
using Day3.Database;

namespace Day3.Controllers
{
    public class CourseController : ApiController
    {
        [HttpGet]
        public List<List<string>> Get()
        {
            return CourseDatabase.GetAll();
        }

        [HttpGet]
        public IEnumerable<string> Get(System.Guid id)
        {
            return CourseDatabase.Get(id);
        }

        [HttpPost]
        public HttpResponseMessage Post(string name)
        {
            CourseDatabase.Add(new Course(name));
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpPut]
        public HttpResponseMessage Put([FromUri]System.Guid id, [FromBody]Course course)
        {
            if (course == null) return Request.CreateResponse(HttpStatusCode.BadRequest);
            CourseDatabase.Update(id, course);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpDelete]
        public HttpResponseMessage Delete(System.Guid id)
        {
            CourseDatabase.Remove(id);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
