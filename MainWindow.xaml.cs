using System.Windows;
using WpfAppPedidos.Views;

namespace WpfAppPedidos
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void BtnPessoas_Click(object sender, RoutedEventArgs e)
        {
            var janela = new PessoaView();
            janela.ShowDialog();
        }

        private void BtnProdutos_Click(object sender, RoutedEventArgs e)
        {
            var janela = new ProdutoView();
            janela.ShowDialog();
        }

        private void BtnPedidos_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("Use a tela de Pessoas para criar pedidos vinculados.");
            var janela = new PessoaView();
            janela.ShowDialog();
        }
    }
}
