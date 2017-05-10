using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Autenticacao.Domain.Entities;
using Autenticacao.Infra.Data.EntityConfig;

namespace Autenticacao.Infra.Data.Context
{
	
	public class AutenticacaoContext : DbContext
	{
		public AutenticacaoContext()
			: base("AutenticacaoContext")
		{
		}

		public virtual IDbSet<Usuario> Usuario { get; set; }
		public virtual IDbSet<Telefone> Telefone { get; set; }


		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
			modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
			modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

			modelBuilder.Properties().Where(p => p.Name == p.ReflectedType.Name + "Id").Configure(p => p.IsKey().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity));
			modelBuilder.Properties<string>().Configure(p => p.HasColumnType("varchar"));

			
			modelBuilder.Configurations.Add(new UsuarioConfiguration());
			modelBuilder.Configurations.Add(new TelefoneConfiguration());

			base.OnModelCreating(modelBuilder);
		}
	}
}
