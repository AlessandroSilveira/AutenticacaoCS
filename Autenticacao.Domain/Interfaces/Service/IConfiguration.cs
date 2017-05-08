namespace Autenticacao.Domain.Interfaces.Service
{
	public interface IConfiguration
	{
		int ObterTempoLogado();
		string ObterSmtp();
		string ObterEmailFrom();
		string ObterPortaServidorEmail();
		string GetBodyEmailRecuperarSenha(string token);
	}
}