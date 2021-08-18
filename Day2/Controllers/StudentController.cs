using Day2.Models;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Day2.Controllers
{
    public class StudentController : ApiController
    {
        // GET api/student
        [HttpGet]
        public List<List<string>> Get()
        {
            return Database.GetAll();
        }

        // GET api/student/5
        [HttpGet]
        public IEnumerable<string> Get(int id)
        {
            return Database.Get(id);
        }

        // POST api/student
        [HttpPost]
        public HttpResponseMessage Post(string name, int age, string college, int year)
        {
            Database.Add(new Student(name, age, college, year));
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // PUT api/student/5
        [HttpPut]
        public HttpResponseMessage Put([FromUri]int id, string name, int age, string college, int year)
        {
            Database.Update(id, name, age, college, year);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // DELETE api/student/5
        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            Database.Remove(id);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
