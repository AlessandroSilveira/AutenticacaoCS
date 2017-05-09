namespace Autenticacao.Domain.Services
{
	public class TokenData
	{
		public string AccessToken { get; set; }
		public string TokenType { get; set; }
		public int ExpiresIn { get; set; }
	}
}