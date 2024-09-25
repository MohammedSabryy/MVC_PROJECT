

using System.Net;

namespace Session03.presentationLayer.Utilities
{
	public class Email
	{
        public string Subject { get; set; }
		public string Body { get; set; }
		public string Recipient {  get; set; }
    }
	public static class MailSettings
	{
		public static void SendEmail(Email email)
		{
			var client = new SmtpClient("smtp.gmail.com ", 587);
			client.EnableSsl = true;
			client.Credentials = new NetworkCredential("mhmdasabry@gmail.com", "qorxoavieraaljkq");
			client.Send("mhmdasabry@gmail.com", email.Recipient, email.Subject, email.Body);
		}
	}
}
