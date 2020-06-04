using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdutosMarcas.Repositorio.Comum
{
    public interface IRepositorioGenerico<TDominio>
    {
       Task<List<TDominio>> SelecionarTodos();
        TDominio SelecionarPorId(int id);
        void Inserir(TDominio entidade);
        void Atualiza(TDominio entidade);
        void Excluir(TDominio entidade);
    }
}
