using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Day3.Models;
using Day3.Database;

namespace Day3.Controllers
{
    public class StudentController : ApiController
    {
        [HttpGet]
        public List<List<string>> Get()
        {
            return StudentDatabase.GetAll();
        }

        [HttpGet]
        public IEnumerable<string> Get(System.Guid id)
        {
            return StudentDatabase.Get(id);
        }

        [HttpPost]
        public HttpResponseMessage Post(string fname, string lname, string college, int age, int cyear)
        {
            StudentDatabase.Add(new Student(fname, lname, college, age, cyear));
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpPut]
        public HttpResponseMessage Put([FromUri]System.Guid id, [FromBody]Student student)
        {
            if (student == null) return Request.CreateResponse(HttpStatusCode.BadRequest);
            StudentDatabase.Update(id, student);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpDelete]
        public HttpResponseMessage Delete(System.Guid id)
        {
            StudentDatabase.Remove(id);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
