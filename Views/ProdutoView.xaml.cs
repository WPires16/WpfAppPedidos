using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WpfAppPedidos.Dados;
using WpfAppPedidos.Models;

namespace WpfAppPedidos.Views
{
    /// <summary>
    /// Lógica interna para ProdutoView.xaml
    /// </summary>
    public partial class ProdutoView : Window
    {
        private List<Produto> produtos;
        private Produto produtoSelecionado;

        public ProdutoView()
        {
            InitializeComponent();
            CarregarDados();
        }

        private void CarregarDados()
        {
            produtos = ArquivoHelper<Produto>.Load();
            dgProdutos.ItemsSource = produtos;
        }

        private void Filtro_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            string nomeFiltro = txtFiltroNome.Text.ToLower();
            string codigoFiltro = txtFiltroCodigo.Text.ToLower();

            decimal valorMin = 0;
            decimal valorMax = decimal.MaxValue;

            decimal.TryParse(txtFiltroValorMin.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out valorMin);
            decimal.TryParse(txtFiltroValorMax.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out valorMax);

            var filtrados = produtos.Where(p =>
                (string.IsNullOrEmpty(nomeFiltro) || p.Nome.ToLower().Contains(nomeFiltro)) &&
                (string.IsNullOrEmpty(codigoFiltro) || p.Codigo.ToLower().Contains(codigoFiltro)) &&
                (p.Valor >= valorMin && p.Valor <= valorMax)
            ).ToList();

            dgProdutos.ItemsSource = filtrados;
        }

        private void BtnNovo_Click(object sender, RoutedEventArgs e)
        {
            produtoSelecionado = null;
            txtNome.Text = "";
            txtNome.Tag = "Enter your username";
            txtCodigo.Text = "";
            txtValor.Text = "";
            dgProdutos.SelectedItem = null;
        }

        private void BtnEditar_Click(object sender, RoutedEventArgs e)
        {
            produtoSelecionado = (Produto)dgProdutos.SelectedItem;
            if (produtoSelecionado != null)
            {
                txtNome.Text = produtoSelecionado.Nome;
                txtCodigo.Text = produtoSelecionado.Codigo;
                txtValor.Text = produtoSelecionado.Valor.ToString(CultureInfo.InvariantCulture);
            }
        }

        private void BtnSalvar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtNome.Text))
            {
                MessageBox.Show("Nome é obrigatório.");
                return;
            }
            if (string.IsNullOrEmpty(txtCodigo.Text))
            {
                MessageBox.Show("Código é obrigatório.");
                return;
            }
            if (!decimal.TryParse(txtValor.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal valor))
            {
                MessageBox.Show("Valor inválido.");
                return;
            }

            if (produtoSelecionado == null)
            {
                produtoSelecionado = new Produto();
                produtoSelecionado.Id = produtos.Count > 0 ? produtos.Max(p => p.Id) + 1 : 1;
                produtos.Add(produtoSelecionado);
            }

            produtoSelecionado.Nome = txtNome.Text;
            produtoSelecionado.Codigo = txtCodigo.Text;
            produtoSelecionado.Valor = valor;

            ArquivoHelper<Produto>.Save(produtos);

            dgProdutos.Items.Refresh();
            MessageBox.Show("Produto salvo com sucesso.");
        }

        private void BtnExcluir_Click(object sender, RoutedEventArgs e)
        {
            var selecionado = (Produto)dgProdutos.SelectedItem;
            if (selecionado != null)
            {
                produtos.Remove(selecionado);
                ArquivoHelper<Produto>.Save(produtos);
                dgProdutos.ItemsSource = null;
                dgProdutos.ItemsSource = produtos;
            }
        }
    }
}