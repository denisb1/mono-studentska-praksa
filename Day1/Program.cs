using System.Collections.Generic;
using Day1.Animals;
using Day1.Messages;

namespace Day1
{
	// Collection of different classes showing fundamentals of OOP
	internal static class Program
	{
		private static void ClassTest()
		{
			var lightbulb = new Lightbulb();
			lightbulb.PressSwitch();
			lightbulb.PressSwitch();
			lightbulb.PressSwitch();
			System.Console.WriteLine();
		}

		private static void PolymorphismTest()
		{
			var genericAnimal1 = new Animal();
			var genericAnimal2 = new Animal("John");
			var animals = new List<Animal>
			{
				genericAnimal1,
				genericAnimal2,
				new Cat("Thomas"),
				new Fish("Nemo"),
				new Fish(),
				new Bird()
			};

			foreach (var animal in animals)
				System.Console.WriteLine($"{animal.GetName()} is {animal.Move()}.");
			System.Console.WriteLine();
		}

		private static void InterfaceTest()
		{
			var senders = new List<IMessageSender>()
			{
				new GenericSender(),
				new EmailSender()
			};

			Message message = new SystemMessage();
			message.Subject = "Test?";
			message.Body = "Message received.";

			foreach (var sender in senders)
			{
				message.MessageSender = sender;
				message.Send();
			}
		}

		public static void Main(string[] args)
		{
			System.Console.Clear();

			ClassTest();
			PolymorphismTest();
			InterfaceTest();
		}
	}
}
