using System;

namespace Day1.Messages
{
	public class GenericSender : IMessageSender
	{
		public void SendMessage(string subject, string body)
		{
			Console.WriteLine($"{subject}\n{body}\n");
		}
	}

	public class EmailSender : IMessageSender
	{
		public void SendMessage(string subject, string body)
		{
			Console.WriteLine($"Email\nSubject: {subject}\nMessage: {body}\n");
		}
	}
}
