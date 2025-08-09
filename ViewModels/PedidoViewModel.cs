using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using WpfAppPedidos.Models;

namespace WpfAppPedidos.ViewModels
{
    public class PedidoViewModel : BaseViewModel
    {
        private ObservableCollection<Pessoa> pessoas;
        private ObservableCollection<Produto> produtos;
        private ObservableCollection<ProdutoPedido> produtosDoPedido;

        private Pessoa pessoaSelecionada;
        private Produto produtoSelecionado;
        private ProdutoPedido produtoPedidoSelecionado;

        private FormaPagamento formaPagamentoSelecionada;
        private StatusPedido statusPedido;

        private DateTime dataVenda;
        private decimal valorTotal;

        private bool pedidoFinalizado;

        public PedidoViewModel(Pessoa pessoa)
        {
            //pessoas = new ObservableCollection<Pessoa>(JsonRepository<Pessoa>.Load());
            //produtos = new ObservableCollection<Produto>(JsonRepository<Produto>.Load());
            //produtosDoPedido = new ObservableCollection<ProdutoPedido>();

            //PessoaSelecionada = pessoa ?? pessoas.FirstOrDefault();

            //DataVenda = DateTime.Now;
            //FormaPagamentoSelecionada = FormaPagamento.Dinheiro;
            //StatusPedido = StatusPedido.Pendente;

            //IncluirProdutoCommand = new RelayCommand(o => IncluirProdutoNoPedido(), o => ProdutoSelecionado != null && QuantidadeProduto > 0 && !pedidoFinalizado);
            //RemoverProdutoCommand = new RelayCommand(o => RemoverProdutoDoPedido(), o => ProdutoPedidoSelecionado != null && !pedidoFinalizado);
            //FinalizarPedidoCommand = new RelayCommand(o => FinalizarPedido(), o => produtosDoPedido.Any() && !pedidoFinalizado);

            //QuantidadeProduto = 1;
        }

        public ObservableCollection<Pessoa> Pessoas => pessoas;
        public ObservableCollection<Produto> Produtos => produtos;
        public ObservableCollection<ProdutoPedido> ProdutosDoPedido
        {
            get => produtosDoPedido;
            set { produtosDoPedido = value; OnPropertyChanged(nameof(ProdutosDoPedido)); AtualizarValorTotal(); }
        }

        public Pessoa PessoaSelecionada
        {
            get => pessoaSelecionada;
            set { pessoaSelecionada = value; OnPropertyChanged(nameof(PessoaSelecionada)); }
        }

        public Produto ProdutoSelecionado
        {
            get => produtoSelecionado;
            set { produtoSelecionado = value; OnPropertyChanged(nameof(ProdutoSelecionado)); }
        }

        public ProdutoPedido ProdutoPedidoSelecionado
        {
            get => produtoPedidoSelecionado;
            set { produtoPedidoSelecionado = value; OnPropertyChanged(nameof(ProdutoPedidoSelecionado)); }
        }

        private int quantidadeProduto;
        public int QuantidadeProduto
        {
            get => quantidadeProduto;
            set { quantidadeProduto = value; OnPropertyChanged(nameof(QuantidadeProduto)); }
        }

        public FormaPagamento FormaPagamentoSelecionada
        {
            get => formaPagamentoSelecionada;
            set { formaPagamentoSelecionada = value; OnPropertyChanged(nameof(FormaPagamentoSelecionada)); }
        }

        public StatusPedido StatusPedido
        {
            get => statusPedido;
            set { statusPedido = value; OnPropertyChanged(nameof(StatusPedido)); }
        }

        public DateTime DataVenda
        {
            get => dataVenda;
            set { dataVenda = value; OnPropertyChanged(nameof(DataVenda)); }
        }

        public decimal ValorTotal
        {
            get => valorTotal;
            set { valorTotal = value; OnPropertyChanged(nameof(ValorTotal)); }
        }

        public bool PedidoFinalizado
        {
            get => pedidoFinalizado;
            set
            {
                pedidoFinalizado = value;
                OnPropertyChanged(nameof(PedidoFinalizado));
                CommandManager.InvalidateRequerySuggested();
            }
        }

        public ICommand IncluirProdutoCommand { get; }
        public ICommand RemoverProdutoCommand { get; }
        public ICommand FinalizarPedidoCommand { get; }

        private void IncluirProdutoNoPedido()
        {
            if (ProdutoSelecionado == null)
            {
                MessageBox.Show("Selecione um produto.");
                return;
            }

            if (QuantidadeProduto <= 0)
            {
                MessageBox.Show("Quantidade deve ser maior que zero.");
                return;
            }

            // Verifica se produto já está na lista
            var existente = produtosDoPedido.FirstOrDefault(pp => pp.Produto.Id == ProdutoSelecionado.Id);
            if (existente != null)
            {
                existente.Quantidade += QuantidadeProduto;
            }
            else
            {
                produtosDoPedido.Add(new ProdutoPedido
                {
                    Produto = ProdutoSelecionado,
                    Quantidade = QuantidadeProduto
                });
            }

            QuantidadeProduto = 1;
            AtualizarValorTotal();
        }

        private void RemoverProdutoDoPedido()
        {
            if (ProdutoPedidoSelecionado != null)
            {
                produtosDoPedido.Remove(ProdutoPedidoSelecionado);
                ProdutoPedidoSelecionado = null;
                AtualizarValorTotal();
            }
        }

        private void AtualizarValorTotal()
        {
            ValorTotal = produtosDoPedido.Sum(pp => pp.ValorTotal);
        }

        private void FinalizarPedido()
        {
            if (PessoaSelecionada == null)
            {
                MessageBox.Show("Selecione uma pessoa.");
                return;
            }

            if (produtosDoPedido.Count == 0)
            {
                MessageBox.Show("Adicione pelo menos um produto.");
                return;
            }

            ////var pedidos = JsonRepository<Pedido>.Load();

            //var pedido = new Pedido
            //{
            //    Pessoa = PessoaSelecionada,
            //    Produtos = produtosDoPedido.ToList(),
            //    DataVenda = DateTime.Now,
            //    FormaPagamento = FormaPagamentoSelecionada,
            //    Status = StatusPedido.Pendente
            //};

            //pedidos.Add(pedido);
            //JsonRepository<Pedido>.Save(pedidos);

            PedidoFinalizado = true;
            MessageBox.Show("Pedido finalizado com sucesso!");
        }
    }
}