using Autenticacao.Domain.Services;

namespace Autenticacao.Domain.Interfaces.Service
{
	public interface IEmailSender
	{
		void BuildFrom(string from);
		void BuildTo(string to);
		void BuildCc(string cc);
		void BuildBcc(string bcc);
		void BuildSubject(string subject);
		void BuildBody(string Body);
		void BuildSmtpServer(string smtpServer);
		void BuildPort(string port);

		IEmailBuilder GetEmail();

	}
}