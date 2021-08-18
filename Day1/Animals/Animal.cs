namespace Day1.Animals
{
	// Encapsulation and class which will be inherited
	public class Animal
	{
		protected string Species { get; set; }
		protected string Name;

		public Animal()
		{
			Species = "Animal";
			Name = string.Empty;
		}

		public Animal(string name) : this()
		{
			Name = name;
		}

		public string GetName()
		{
			var ret = Species;
			if (Name != string.Empty) ret += " " + Name;
			return ret;
		}

		public virtual string Move()
		{
			return "moving";
		}
	}
}
