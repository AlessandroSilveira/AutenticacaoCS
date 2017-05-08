namespace Autenticacao.Domain.Interfaces.Service
{
	public interface ICriptografia
	{
		 string Hash(string senha);
	}
}
