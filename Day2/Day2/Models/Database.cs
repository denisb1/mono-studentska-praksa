using System.Collections.Generic;

namespace Day2.Models
{
	public class Database
	{
		private static readonly List<Student> students = new();

		public static void Add(Student student)
		{
			students.Add(student);
		}

		public static void Remove(int id)
		{
			if (id >= 0 && id < students.Count) students.RemoveAt(id);
		}

		public static void Update(int id, Student student)
		{
			if (id < 0 || id >= students.Count) return;

			students[id].Name = student.Name;
			students[id].College = student.College;
			students[id].Age = student.Age;
			students[id].Year = student.Year;
		}

		public static Student Get(int id)
		{
			if (id >= 0 && id < students.Count) return students[id];
			return null;
		}

		public static List<Student> GetAll()
		{
			return students;
		}
	}
}
