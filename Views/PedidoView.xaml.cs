using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WpfAppPedidos.Dados;
using WpfAppPedidos.Models;

namespace WpfAppPedidos.Views
{
    public partial class PedidoView : Window
    {
        private Pessoa pessoa;
        private List<Produto> produtos;
        private List<Pedido> pedidos;
        private Pedido pedidoAtual;
        private List<ItemPedidoView> itensPedidoView = new List<ItemPedidoView>();

        public PedidoView(Pessoa pessoaSelecionada)
        {
            InitializeComponent();

            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            pessoa = pessoaSelecionada;
            txtPessoa.Text = pessoa.Nome;

            produtos = ArquivoHelper<Produto>.Load();
            pedidos = ArquivoHelper<Pedido>.Load();

            cmbProdutos.ItemsSource = produtos;
            pedidoAtual = new Pedido
            {
                Id = pedidos.Count > 0 ? pedidos.Max(p => p.Id) + 1 : 1,
                Pessoa = pessoa,
                Itens = new List<ItemPedido>(),
                DataVenda = DateTime.Now,
                Status = "Pendente",
                Finalizado = false
            };

            AtualizarGridItens();
        }

        private void BtnAdicionarProduto_Click(object sender, RoutedEventArgs e)
        {
            if (cmbProdutos.SelectedItem == null)
            {
                MessageBox.Show("Selecione um produto.");
                return;
            }

            if (!int.TryParse(txtQuantidade.Text, out int quantidade) || quantidade < 1)
            {
                MessageBox.Show("Quantidade inválida.");
                return;
            }

            var produtoSelecionado = (Produto)cmbProdutos.SelectedItem;

            var itemExistente = pedidoAtual.Itens.FirstOrDefault(i => i.Produto.Id == produtoSelecionado.Id);
            if (itemExistente != null)
            {
                itemExistente.Quantidade += quantidade;
            }
            else
            {
                pedidoAtual.Itens.Add(new ItemPedido
                {
                    Produto = produtoSelecionado,
                    Quantidade = quantidade
                });
            }

            AtualizarGridItens();
            AtualizarValorTotal();
        }

        private void AtualizarGridItens()
        {
            itensPedidoView = pedidoAtual.Itens.Select(i => new ItemPedidoView
            {
                Produto = i.Produto,
                Quantidade = i.Quantidade,
                Subtotal = i.Produto.Valor * i.Quantidade
            }).ToList();

            dgItensPedido.ItemsSource = null;
            dgItensPedido.ItemsSource = itensPedidoView;
        }

        private void AtualizarValorTotal()
        {
            txtValorTotal.Text = pedidoAtual.ValorTotal.ToString("C");
        }

        private void BtnFinalizarPedido_Click(object sender, RoutedEventArgs e)
        {
            if (pedidoAtual.Itens.Count == 0)
            {
                MessageBox.Show("Adicione pelo menos um produto.");
                return;
            }

            if (cmbFormaPagamento.SelectedItem == null)
            {
                MessageBox.Show("Selecione a forma de pagamento.");
                return;
            }

            pedidoAtual.FormaPagamento = ((ComboBoxItem)cmbFormaPagamento.SelectedItem).Content.ToString();
            pedidoAtual.Status = "Pendente";
            pedidoAtual.DataVenda = DateTime.Now;
            pedidoAtual.Finalizado = true;

            pedidos.Add(pedidoAtual);
            ArquivoHelper<Pedido>.Save(pedidos);

            MessageBox.Show("Pedido finalizado com sucesso!");
            this.Close();
        }

        // Classe auxiliar para exibir no DataGrid
        private class ItemPedidoView
        {
            public Produto Produto { get; set; }
            public int Quantidade { get; set; }
            public decimal Subtotal { get; set; }
        }
    }
}