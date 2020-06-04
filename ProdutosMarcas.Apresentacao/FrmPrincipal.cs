using ProdutosMarcas.Apresentacao.ViewModels;
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
    public partial class FrmPrincipal : Form
    {
        public FrmPrincipal()
        {
            InitializeComponent();
        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
            PreenhcerDgvMarcasAsync();
            PreenhcerDgvProdutosAsync();
        }

        private async void PreenhcerDgvMarcasAsync()
        {
            IRepositorioGenerico<Marca> repositorioMarcas = new RepositorioMarca();
            List<Marca> marcas = await repositorioMarcas.SelecionarTodos();
            List<MarcaViewModel> marcaViewModels = new List<MarcaViewModel>();
            foreach (Marca marca in marcas)
            {
                MarcaViewModel viewModel = new MarcaViewModel
                {
                    Id = marca.Id,
                    Nome = marca.Nome
                };
                marcaViewModels.Add(viewModel);
            }
            dgvMarcas.Invoke((MethodInvoker)delegate
            {
                dgvMarcas.DataSource = marcaViewModels;
                dgvMarcas.Refresh();
            });
        }
        private async void PreenhcerDgvProdutosAsync()
        {
            IRepositorioGenerico<Produto> repositorioProdutos = new RepositorioProduto();
            List<Produto> produtos = await repositorioProdutos.SelecionarTodos();
            List<ProdutoViewModel> produtoViewModels = new List<ProdutoViewModel>();
            foreach (Produto produto in produtos)
            {
                ProdutoViewModel viewModel = new ProdutoViewModel
                {
                    Id = produto.Id,
                    Marca = produto.Marca.Nome,
                    MarcaId = produto.MarcaId,
                    Nome = produto.Nome
                };
                produtoViewModels.Add(viewModel);
            }
            dgvProdutos.Invoke((MethodInvoker)delegate
            {
                dgvProdutos.DataSource = produtoViewModels;
                dgvProdutos.Refresh();
            });
        }

        private void btnCadastrarMarca_Click(object sender, EventArgs e)
        {
            FrmMarca frmMarca = new FrmMarca();
            frmMarca.ShowDialog();
            PreenhcerDgvMarcasAsync();
        }

        private void btnCadastrarProduto_Click(object sender, EventArgs e)
        {
            FrmProduto frmProduto = new FrmProduto();
            frmProduto.ShowDialog();
            PreenhcerDgvProdutosAsync();
        }

        private void btnAlterarMarca_Click(object sender, EventArgs e)
        {
            if (dgvMarcas.SelectedRows.Count > 0)
            {
                int idMarcaSelecionada = Convert.ToInt32(dgvMarcas.SelectedRows[0].Cells[0].Value);
                IRepositorioGenerico<Marca> repositorioMarcas = new RepositorioMarca();
                Marca marcaSelecionada = repositorioMarcas.SelecionarPorId(idMarcaSelecionada);
                FrmMarca frmMarca = new FrmMarca(marcaSelecionada);
                frmMarca.ShowDialog();
                PreenhcerDgvMarcasAsync();
                PreenhcerDgvProdutosAsync();
            }
            else
            {
                MessageBox.Show("Selecione uma marca antes.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnAlterarProduto_Click(object sender, EventArgs e)
        {
            if (dgvProdutos.SelectedRows.Count > 0)
            {
                int idProdutSelecionado = Convert.ToInt32(dgvProdutos.SelectedRows[0].Cells[0].Value);
                IRepositorioGenerico<Produto> repositorioProdutos = new RepositorioProduto();
                Produto produtoSelecionado = repositorioProdutos.SelecionarPorId(idProdutSelecionado);
                FrmProduto frmProduto = new FrmProduto(produtoSelecionado);
                frmProduto.ShowDialog();
                PreenhcerDgvProdutosAsync();
            }
            else
            {
                MessageBox.Show("Selecione um produto antes.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnExcluirMarca_Click(object sender, EventArgs e)
        {
            if (dgvMarcas.SelectedRows.Count > 0)
            {
                int idMarcaSelecionada = Convert.ToInt32(dgvMarcas.SelectedRows[0].Cells[0].Value);
                IRepositorioGenerico<Marca> repositorioMarcas = new RepositorioMarca();
                Marca marcaASerExcluida = repositorioMarcas.SelecionarPorId(idMarcaSelecionada);
                repositorioMarcas.Excluir(marcaASerExcluida);
                PreenhcerDgvMarcasAsync();
                PreenhcerDgvProdutosAsync();
            }
            else
            {
                MessageBox.Show("Selecione uma marca antes.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnExcluirProduto_Click(object sender, EventArgs e)
        {
            if (dgvProdutos.SelectedRows.Count > 0)
            {
                int idProdutSelecionado = Convert.ToInt32(dgvProdutos.SelectedRows[0].Cells[0].Value);
                IRepositorioGenerico<Produto> repositorioProdutos = new RepositorioProduto();
                Produto produtoASerExcluido = repositorioProdutos.SelecionarPorId(idProdutSelecionado);
                repositorioProdutos.Excluir(produtoASerExcluido);
                PreenhcerDgvProdutosAsync();
            }
            else
            {
                MessageBox.Show("Selecione um produto antes.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
