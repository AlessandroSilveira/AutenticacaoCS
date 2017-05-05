using System;
using System.Collections.Generic;

namespace Autenticacao.Domain.Entities
{
	public class Usuario
	{
		public Usuario()
		{
			UsuarioId = new Guid();
		}

		public Guid UsuarioId { get;  set; }
		public string Nome { get;  set; }
		public string Email { get;  set; }
		public string Senha { get;  set; }
		public DateTime DataCriacao { get;  set; }
		public DateTime DataAtualizacao { get;  set; }
		public DateTime DataUltimoLogin { get;  set; }
		public string Token { get;  set; }

		public virtual ICollection<Telefone> Telefones { get; set; } = new List<Telefone>();
	}
}
