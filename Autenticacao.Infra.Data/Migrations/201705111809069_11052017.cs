namespace Autenticacao.Infra.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _11052017 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Usuarios", "Token", c => c.String(nullable: false, maxLength: 500, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Usuarios", "Token", c => c.String(nullable: false, maxLength: 250, unicode: false));
        }
    }
}
