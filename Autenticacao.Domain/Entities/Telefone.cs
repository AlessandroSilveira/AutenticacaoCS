using System;

namespace Autenticacao.Domain.Entities
{
	public class Telefone
	{
		public Telefone()
		{
			TelefoneId = new Guid();
		}

		public  Guid TelefoneId { get; set; }
		public Guid UsuarioId { get; set; }
		public string Ddd { get; set; }
		public string Numero { get; set; }
		public Usuario Usuario { get; set; }
	}
}
