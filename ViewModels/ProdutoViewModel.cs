using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using WpfAppPedidos.Models;

namespace WpfAppPedidos.ViewModels
{
    public class ProdutoViewModel : BaseViewModel
    {
        private ObservableCollection<Produto> produtos;
        private ObservableCollection<Produto> produtosOriginais;

        private Produto produtoSelecionado;

        private string filtroNome;
        private string filtroCodigo;
        private decimal? filtroValorMin;
        private decimal? filtroValorMax;

        public ProdutoViewModel()
        {
            //produtosOriginais = new ObservableCollection<Produto>(JsonRepository<Produto>.Load());
            //Produtos = new ObservableCollection<Produto>(produtosOriginais);

            //IncluirCommand = new RelayCommand(o => IncluirProduto());
            //ExcluirCommand = new RelayCommand(o => ExcluirProduto(), o => ProdutoSelecionado != null);
            //SalvarCommand = new RelayCommand(o => SalvarProdutos());
        }

        public ObservableCollection<Produto> Produtos
        {
            get => produtos;
            set { produtos = value; OnPropertyChanged(nameof(Produtos)); }
        }

        public Produto ProdutoSelecionado
        {
            get => produtoSelecionado;
            set { produtoSelecionado = value; OnPropertyChanged(nameof(ProdutoSelecionado)); }
        }

        public string FiltroNome
        {
            get => filtroNome;
            set { filtroNome = value; OnPropertyChanged(nameof(FiltroNome)); Filtrar(); }
        }

        public string FiltroCodigo
        {
            get => filtroCodigo;
            set { filtroCodigo = value; OnPropertyChanged(nameof(FiltroCodigo)); Filtrar(); }
        }

        public decimal? FiltroValorMin
        {
            get => filtroValorMin;
            set { filtroValorMin = value; OnPropertyChanged(nameof(FiltroValorMin)); Filtrar(); }
        }

        public decimal? FiltroValorMax
        {
            get => filtroValorMax;
            set { filtroValorMax = value; OnPropertyChanged(nameof(FiltroValorMax)); Filtrar(); }
        }

        public ICommand IncluirCommand { get; }
        public ICommand ExcluirCommand { get; }
        public ICommand SalvarCommand { get; }

        private void Filtrar()
        {
            var lista = produtosOriginais.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(FiltroNome))
                lista = lista.Where(p => p.Nome.ToLower().Contains(FiltroNome.ToLower()));

            if (!string.IsNullOrWhiteSpace(FiltroCodigo))
                lista = lista.Where(p => p.Codigo.ToLower().Contains(FiltroCodigo.ToLower()));

            if (FiltroValorMin.HasValue)
                lista = lista.Where(p => p.Valor >= FiltroValorMin.Value);

            if (FiltroValorMax.HasValue)
                lista = lista.Where(p => p.Valor <= FiltroValorMax.Value);

            Produtos = new ObservableCollection<Produto>(lista);
        }

        private void IncluirProduto()
        {
            var novoProduto = new Produto();
            Produtos.Add(novoProduto);
            ProdutoSelecionado = novoProduto;
        }

        private void ExcluirProduto()
        {
            if (ProdutoSelecionado == null) return;

            if (MessageBox.Show("Confirma exclusão?", "Excluir", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Produtos.Remove(ProdutoSelecionado);
                produtosOriginais.Remove(ProdutoSelecionado);
                ProdutoSelecionado = null;
                //JsonRepository<Produto>.Save(produtosOriginais.ToList());
            }
        }

        private bool ValidarProduto(Produto p, out string erro)
        {
            erro = "";

            if (string.IsNullOrWhiteSpace(p.Nome))
            {
                erro = "Nome é obrigatório.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(p.Codigo))
            {
                erro = "Código é obrigatório.";
                return false;
            }

            if (p.Valor <= 0)
            {
                erro = "Valor deve ser maior que zero.";
                return false;
            }

            return true;
        }

        private void SalvarProdutos()
        {
            foreach (var p in Produtos)
            {
                if (!ValidarProduto(p, out var erro))
                {
                    MessageBox.Show($"Erro no produto Id {p.Id}: {erro}");
                    return;
                }
            }

            produtosOriginais = new ObservableCollection<Produto>(Produtos);
            //JsonRepository<Produto>.Save(produtosOriginais.ToList());
            MessageBox.Show("Produtos salvos com sucesso!");
        }
    }
}