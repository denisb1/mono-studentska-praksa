namespace Day1.Messages
{
	public class SystemMessage : Message
	{
		public override void Send()
		{
			MessageSender.SendMessage(Subject, Body);
		}
	}
}
