namespace Autenticacao.Infra.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class inicial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Telefones",
                c => new
                    {
                        TelefoneId = c.Guid(nullable: false, identity: true),
                        UsuarioId = c.Guid(nullable: false),
                        Ddd = c.String(maxLength: 2, unicode: false),
                        Numero = c.String(maxLength: 10, unicode: false),
                    })
                .PrimaryKey(t => t.TelefoneId)
                .ForeignKey("dbo.Usuarios", t => t.UsuarioId)
                .Index(t => t.UsuarioId);
            
            CreateTable(
                "dbo.Usuarios",
                c => new
                    {
                        UsuarioId = c.Guid(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 150, unicode: false),
                        Email = c.String(nullable: false, maxLength: 150, unicode: false),
                        Senha = c.String(nullable: false, maxLength: 250, unicode: false),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAtualizacao = c.DateTime(nullable: false),
                        DataUltimoLogin = c.DateTime(nullable: false),
                        Token = c.String(nullable: false, maxLength: 250, unicode: false),
                    })
                .PrimaryKey(t => t.UsuarioId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Telefones", "UsuarioId", "dbo.Usuarios");
            DropIndex("dbo.Telefones", new[] { "UsuarioId" });
            DropTable("dbo.Usuarios");
            DropTable("dbo.Telefones");
        }
    }
}
