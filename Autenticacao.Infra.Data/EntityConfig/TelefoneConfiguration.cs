using System.Data.Entity.ModelConfiguration;
using Autenticacao.Domain.Entities;

namespace Autenticacao.Infra.Data.EntityConfig
{
	public class TelefoneConfiguration : EntityTypeConfiguration<Telefone>
	{
		public TelefoneConfiguration()
		{
			HasKey(c => c.TelefoneId);

			Property(c => c.Ddd)
			   .HasMaxLength(2);

			Property(c => c.Numero)
			   .HasMaxLength(10);

			HasRequired((t => t.Usuario))
				.WithMany(c => c.Telefones)
				.HasForeignKey(c => c.UsuarioId);

			ToTable("Telefones");
		} 
	}
}