namespace Day1.Messages
{
	// Provides interface for different senders
	public interface IMessageSender
	{
		public void SendMessage(string subject, string body);
	}
}
