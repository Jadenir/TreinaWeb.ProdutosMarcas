namespace ProdutosMarcas.Persistencia.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration_Inicial_Criacao_Produto_Marca : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Marcas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Produtoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        MarcaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Marcas", t => t.MarcaId)
                .Index(t => t.MarcaId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Produtoes", "MarcaId", "dbo.Marcas");
            DropIndex("dbo.Produtoes", new[] { "MarcaId" });
            DropTable("dbo.Produtoes");
            DropTable("dbo.Marcas");
        }
    }
}
