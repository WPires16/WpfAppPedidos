using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace WpfAppPedidos.Views
{
    /// <summary>
    /// Lógica interna para PedidoView.xaml
    /// </summary>
    public partial class PedidoView : Window
    {
        public ObservableCollection<ItemPedidoTemp> Itens { get; set; }

        public PedidoView()
        {
            InitializeComponent();

            Itens = new ObservableCollection<ItemPedidoTemp>();
            dgItens.ItemsSource = Itens;
        }

        private void BtnAdicionar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtProduto.Text) ||
                !int.TryParse(txtQuantidade.Text, out int qtd) ||
                !decimal.TryParse(txtPreco.Text, out decimal preco))
            {
                MessageBox.Show("Preencha todos os campos corretamente.");
                return;
            }

            Itens.Add(new ItemPedidoTemp
            {
                Produto = txtProduto.Text,
                Quantidade = qtd,
                Preco = preco,
                Total = qtd * preco
            });

            txtProduto.Clear();
            txtQuantidade.Clear();
            txtPreco.Clear();

            AtualizarTotal();
        }

        private void AtualizarTotal()
        {
            txtTotalPedido.Text = Itens.Sum(i => i.Total).ToString("C");
        }

        private void BtnFechar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }

    public class ItemPedidoTemp
    {
        public string Produto { get; set; }
        public int Quantidade { get; set; }
        public decimal Preco { get; set; }
        public decimal Total { get; set; }
    }
}