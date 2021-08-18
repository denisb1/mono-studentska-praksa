namespace Day2.Models
{
    public class Student
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string College { get; set; }
        public int Year { get; set; }

        public Student(string name, int age, string college, int year)
        {
            Name = name;
            Age = age;
            College = college;
            Year = year;
        }
    }
}
