namespace Day1.Animals
{
	// Inheritance
	public class Bird : Animal
	{
		public Bird()
		{
			Species = "Bird";
		}

		public Bird(string name) : this()
		{
			Name = name;
		}

		public override string Move()
		{
			return "flying";
		}
	}
}
