namespace Day1.Animals
{
	// Inheritance
	public class Fish : Animal
	{
		public Fish()
		{
			Species = "Fish";
		}

		public Fish(string name) : this()
		{
			Name = name;
		}

		public override string Move()
		{
			return "swimming";
		}
	}
}
