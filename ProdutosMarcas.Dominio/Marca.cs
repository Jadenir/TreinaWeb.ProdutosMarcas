using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ProdutosMarcas.Dominio
{
    public class Marca
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public List<Produto> Produtos { get; set; }
    }
}
