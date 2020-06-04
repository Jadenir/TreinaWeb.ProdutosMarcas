using ProdutosMarcas.Apresentacao.ViewModels;
using ProdutosMarcas.Dominio;
using ProdutosMarcas.Repositorio.Comum;
using ProdutosMarcas.Repositorio.EF;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProdutosMarcas.Apresentacao
{
    public partial class FrmProduto : Form
    {
        private Produto produtoASerAlterado;
        public FrmProduto(Produto produto = null)
        {
            produtoASerAlterado = produto;
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private async void FrmProduto_Load(object sender, EventArgs e)
        {
            IRepositorioGenerico<Marca> repositorioMarcas = new RepositorioMarca();
            List<Marca> marcas = await repositorioMarcas.SelecionarTodos();
            List<MarcaViewModel> viewModels = new List<MarcaViewModel>();
            foreach (Marca marca in marcas)
            {
                MarcaViewModel viewModel = new MarcaViewModel
                {
                    Id = marca.Id,
                    Nome = marca.Nome
                };
                viewModels.Add(viewModel);
            }
            cmbMarcas.DataSource = viewModels;
            cmbMarcas.DisplayMember = "Nome";
            cmbMarcas.ValueMember = "Id";
            cmbMarcas.Refresh();
            if (produtoASerAlterado != null)
            {
                txbNomeProduto.Text = produtoASerAlterado.Nome;
                cmbMarcas.SelectedValue = produtoASerAlterado.MarcaId;
            }
            else
            {
                txbNomeProduto.Text = string.Empty;
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            IRepositorioGenerico<Produto> repositorioProdutos = new RepositorioProduto();
            if (produtoASerAlterado == null)
            {
                Produto novoProduto = new Produto
                {
                    Nome = txbNomeProduto.Text.Trim(),
                    MarcaId = Convert.ToInt32(cmbMarcas.SelectedValue)
                };
                repositorioProdutos.Inserir(novoProduto);
            }
            else
            {
                produtoASerAlterado.Nome = txbNomeProduto.Text.Trim();
                produtoASerAlterado.MarcaId = Convert.ToInt32(cmbMarcas.SelectedValue);
                repositorioProdutos.Atualiza(produtoASerAlterado);
            }
            Close();
        }
    }
}
