namespace Day1
{
	// Simple class with private variable and public method
	public class Lightbulb
	{
		private bool IsPressed { get; set; }

		public Lightbulb()
		{
			IsPressed = false;
		}

		public void PressSwitch()
		{
			IsPressed = !IsPressed;
			System.Console.WriteLine("Light is {0}", IsPressed ? "on!" : "off!");
		}
	}
}
