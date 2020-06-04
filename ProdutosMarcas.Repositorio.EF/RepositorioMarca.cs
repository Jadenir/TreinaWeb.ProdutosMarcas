using ProdutosMarcas.Dominio;
using ProdutosMarcas.Persistencia.EF.Context;
using ProdutosMarcas.Repositorio.Comum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdutosMarcas.Repositorio.EF
{
    public class RepositorioMarca : IRepositorioGenerico<Marca>
    {
        public void Atualiza(Marca entidade)
        {
            using (ProdutosMarcasDbContext contexto = new ProdutosMarcasDbContext())
            {
                contexto.Marcas.Attach(entidade);
                contexto.Entry(entidade).State = System.Data.Entity.EntityState.Modified;
                contexto.SaveChanges();
            }
        }

        public void Excluir(Marca entidade)
        {
            using (ProdutosMarcasDbContext contexto = new ProdutosMarcasDbContext())
            {
                contexto.Marcas.Attach(entidade);
                contexto.Entry(entidade).State = System.Data.Entity.EntityState.Deleted;
                contexto.SaveChanges();
            }
        }

        public void Inserir(Marca entidade)
        {
            using (ProdutosMarcasDbContext contexto = new ProdutosMarcasDbContext())
            {
                contexto.Marcas.Add(entidade);
                contexto.SaveChanges();
            }
        }

        public Marca SelecionarPorId(int id)
        {
            using (ProdutosMarcasDbContext contexto = new ProdutosMarcasDbContext())
            {
                return contexto.Marcas.Find(id);
            }
        }

        public Task<List<Marca>> SelecionarTodos()
        {
            return Task.Run(() =>
            {
                using (ProdutosMarcasDbContext contexto = new ProdutosMarcasDbContext())
                {
                    return contexto.Marcas.ToList();
                }
            });
        }
    }
}
