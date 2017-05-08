using Autenticacao.Domain.Interfaces.Service;

namespace Autenticacao.Domain.Services
{
	public class EmailBuilder : IEmailBuilder
	{ 
		public string From { get; set; }
		public string To { get; set; }
		public string Cc { get; set; }
		public string Bcc { get; set; }
		public string Subject { get; set; }
		public string Body { get; set; }
		public string SmtpServer { get; set; }
		public string BodyFormat { get; set; }
		public string Port { get; set; }

		public string GetFrom()
		{
			return From;
		}

		public string GetTo()
		{
			return To;
		}

		public string GetCc()
		{
			return Cc;
		}

		public string GetBcc()
		{
			return Bcc;
		}

		public string GetSubject()
		{
			return Subject;
		}

		public string GetBody()
		{
			return Body;
		}

		public string GetSmtpServer()
		{
			return SmtpServer;
		}

		public string GetBodyFormat()
		{
			return BodyFormat;
		}

		public string GetPort()
		{
			return Port;
		}
	}
}
