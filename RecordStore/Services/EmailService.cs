using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace TicTacToe.Services
{
	public class EmailService : IEmailService
	{
		private readonly EmailServiceOptions options;

        public EmailService(IOptions<EmailServiceOptions> options)
		{
			this.options = options.Value;
        }

		public Task SendEmail(string emailTo, string subject, string message)
		{
			using(var client = new SmtpClient(options.MailServer, int.Parse(options.MailPort)))
			{
				if(bool.Parse(options.UseSSL) == true)
					client.EnableSsl = true;

				if(!string.IsNullOrEmpty(options.UserId))
				{
					client.UseDefaultCredentials = false;
					client.Credentials = new NetworkCredential(options.UserId, options.Password);
				}

				client.Send(new MailMessage(options.UserId, emailTo, subject, message){IsBodyHtml = true});
			}
			return Task.CompletedTask;
		}
	}

	public class EmailServiceOptions
	{
		public string MailType { get; set; }
		public string MailServer { get; set; }
		public string MailPort { get; set; }
		public string UseSSL { get; set; }
		public string UserId { get; set; }
		public string Password { get; set; }
		public string RemoteServerAPI { get; set; }
		public string RemoteServerKey { get; set; }

		public EmailServiceOptions()
		{
		}

		public EmailServiceOptions(string mailType, string mailServer, string mailPort, string useSSL,
			string userId, string password, string remoteServerAPI,	string remoteServerKey)
		{
			MailType = mailType;
			MailServer = mailServer;
			MailPort = mailPort;
			UseSSL = useSSL;
			UserId = userId;
			Password = password;
			RemoteServerAPI = remoteServerAPI;
			RemoteServerKey = remoteServerKey;
		}
	}
}
