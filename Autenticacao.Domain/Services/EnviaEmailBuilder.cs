﻿using Autenticacao.Domain.Entities;
using Autenticacao.Domain.Interfaces.Service;

namespace Autenticacao.Domain.Services
{
	public class EnviaEmailBuilder :  IEmailSender
	{
		//private Usuario usuario;

		//public EnviaEmailBuilder(string @from, string to, string cc, string bcc, string subject, string body, string smtpServer, string bodyFormat, string port, Usuario usuario) : base(@from, to, cc, bcc, subject, body, smtpServer, bodyFormat, port)
		//{
		//	this.usuario = usuario;
		//}
		public string From { get; set; }
		public string To { get; set; }
		public string Cc { get; set; }
		public string Bcc { get; set; }
		public string Subject { get; set; }
		public string Body { get; set; }
		public string SmtpServer { get; set; }
		public string BodyFormat { get; set; }
		public string Port { get; set; }

		public void BuildFrom(string @from)
		{
			this.From = from;
		}

		public void BuildTo(string to)
		{
			this.To = to;
		}

		public void BuildCc(string cc)
		{
			this.Cc = cc;
		}

		public void BuildBcc(string bcc)
		{
			this.Bcc = bcc;
		}

		public void BuildSubject(string subject)
		{
			this.Subject = subject;
		}

		public void BuildBody(string Body)
		{
			this.Body = Body;
		}

		public void BuildSmtpServer(string smtpServer)
		{
			this.SmtpServer = smtpServer;
		}

		public void BuildPort(string port)
		{
			this.Port = port;
		}

		public IEmailBuilder GetEmail()
		{
			return new EmailBuilder();

		}
	}
}
