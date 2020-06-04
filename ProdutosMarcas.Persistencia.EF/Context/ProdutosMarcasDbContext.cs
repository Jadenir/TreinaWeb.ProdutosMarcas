using ProdutosMarcas.Dominio;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdutosMarcas.Persistencia.EF.Context
{
    public class ProdutosMarcasDbContext : DbContext
    {
        public DbSet<Marca> Marcas { get; set; }
        public DbSet<Produto> Produtos { get; set; }

        public ProdutosMarcasDbContext()
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Produto>()
                .HasRequired(p => p.Marca)
                .WithMany(p => p.Produtos)
                .HasForeignKey(fk => fk.MarcaId)
                .WillCascadeOnDelete(false);
        }
    }
}
