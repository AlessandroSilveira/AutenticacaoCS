namespace Autenticacao.Domain.Interfaces.Service
{
	public interface IJwt
	{
		string GenerateToken(string username);
	}
}
