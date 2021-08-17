using Day2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApplication23.Controllers
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
        public Student Post(string name, int age, string college, int year)
        {
            var student = new Student(name, age, college, year);
            Database.Add(student);
            return student;
        }

        // PUT api/student/5
        [HttpPut]
        public void Put([FromUri]int id, string name, int age, string college, int year)
        {
            Database.Update(id, name, age, college, year);
        }

        // DELETE api/student/5
        [HttpDelete]
        public void Delete(int id)
        {
            Database.Remove(id);
        }
    }
}
