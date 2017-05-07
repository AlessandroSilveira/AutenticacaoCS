using System;
using System.Collections.Generic;

namespace Autenticacao.Domain.Entities
{
	public class Usuario
	{
		private string v1;
		private ICollection<Telefone> telefones;
		private string v2;

		public Usuario()
		{
			UsuarioId = new Guid();
		}

		public Usuario(string nome, string email, string v1, ICollection<Telefone> telefones, string v2)
		{
			Nome = nome;
			Email = email;
			this.v1 = v1;
			this.telefones = telefones;
			this.v2 = v2;
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
