using ProdutosMarcas.Dominio;
using ProdutosMarcas.Repositorio.Comum;
using ProdutosMarcas.Repositorio.EF;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProdutosMarcas.Apresentacao
{
    public partial class FrmMarca : Form
    {
        private Marca marcaASerAlterada;
        public FrmMarca(Marca marca = null)
        {
            marcaASerAlterada = marca;
            InitializeComponent();
        }
        private void FrmMarca_Load(object sender, EventArgs e)
        {
            if (marcaASerAlterada != null)
            {
                txbNomeMarca.Text = marcaASerAlterada.Nome;
            }
            else
            {
                txbNomeMarca.Text = string.Empty;
            }
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            IRepositorioGenerico<Marca> repositorioMarcas = new RepositorioMarca();
            if (marcaASerAlterada == null)
            {
                Marca novaMarca = new Marca
                {
                    Nome = txbNomeMarca.Text.Trim()
                };
                repositorioMarcas.Inserir(novaMarca);
            }
            else
            {
                marcaASerAlterada.Nome = txbNomeMarca.Text.Trim();
                repositorioMarcas.Atualiza(marcaASerAlterada);
            }
            Close();
        }
    }
}
