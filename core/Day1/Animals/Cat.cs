namespace Day1.Animals
{
	// Inheritance
	public class Cat : Animal
	{
		public Cat()
		{
			Species = "Cat";
		}

		public Cat(string name) : this()
		{
			Name = name;
		}

		public override string Move()
		{
			return "running";
		}
	}
}
