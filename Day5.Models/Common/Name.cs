namespace Day5.Models.Common
{
	public readonly struct Name
	{
		public string First { get; }
		public string Last { get; }

		public Name(string firstName, string lastName)
		{
			First = firstName;
			Last = lastName;
		}

		public override string ToString() => $"{Last}, {First}";
	}
}
