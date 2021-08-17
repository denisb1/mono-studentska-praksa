using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Day2.Models
{
    public class Database
    {
        private static List<Student> students = new List<Student>();
        public static IEnumerable<Student> AllStudents => students;

        public static void Add(Student student)
        {
            students.Add(student);
        }

        public static void Remove(int id)
        {
            if (id >= 0 && id < students.Count)
                students.RemoveAt(id);
        }

        public static void Update(int id, string name, int age, string college, int year)
        {
            if (id < 0 || id >= students.Count) return;

            students[id].Name = name;
            students[id].College = college;
            students[id].Age = age;
            students[id].Year = year;
        }

        public static IEnumerable<string> Get(int id)
        {
            if (id >= 0 && id < students.Count)
                return new string[] { students[id].Name, students[id].Age.ToString(), students[id].College, students[id].Year.ToString() };
            return new string[] { "empty" };
        }

        public static List<List<string>> GetAll()
        {
            var count = students.Count;
            var studentData = new List<List<string>>();

            if (count == 0)
            {
                studentData.Add(new List<string> { "empty" });
            }
            else
            {
                var i = 0;
                foreach (Student s in students)
                {
                    studentData.Add(new List<string>(5) { i.ToString(), s.Name, s.Age.ToString(), s.College, s.Year.ToString() });
                    ++i;
                }
            }            
            return studentData;
        }
    }
}
