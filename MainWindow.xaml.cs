using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
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
            MessageBox.Show("Use a tela de Pessoas para criar pedidos vinculados.");
        }
    }
}
