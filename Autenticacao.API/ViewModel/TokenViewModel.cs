namespace Autenticacao.API.ViewModel
{
	public class TokenViewModel
	{
		public string AccessToken { get; set; }
		public string TokenType { get; set; }
		public int ExpiresIn { get; set; }
	}
}