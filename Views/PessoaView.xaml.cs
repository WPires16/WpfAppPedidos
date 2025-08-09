using System.Collections.Generic;
using System.Linq;
using System.Windows;
using WpfAppPedidos.Dados;
using WpfAppPedidos.Models;
using WpfAppPedidos.Utils;

namespace WpfAppPedidos.Views
{
    public partial class PessoaView : Window
    {
        private List<Pessoa> pessoas;
        private List<Pedido> pedidos;
        private Pessoa pessoaSelecionada;

        public PessoaView()
        {
            InitializeComponent();
            CarregarDados();
        }

        private void CarregarDados()
        {
            pessoas = ArquivoHelper<Pessoa>.Load();
            pedidos = ArquivoHelper<Pedido>.Load();

            dgPessoas.ItemsSource = pessoas;
        }

        private void Filtro_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            string nomeFiltro = txtFiltroNome.Text.ToLower();
            string cpfFiltro = new string(txtFiltroCPF.Text.Where(char.IsDigit).ToArray());

            var filtradas = pessoas.Where(p =>
                (string.IsNullOrEmpty(nomeFiltro) || p.Nome.ToLower().Contains(nomeFiltro)) &&
                (string.IsNullOrEmpty(cpfFiltro) || p.CPF.Contains(cpfFiltro))
            ).ToList();

            dgPessoas.ItemsSource = filtradas;
        }

        private void BtnNovo_Click(object sender, RoutedEventArgs e)
        {
            pessoaSelecionada = new Pessoa();
            txtNome.Text = "";
            txtCPF.Text = "";
            txtEndereco.Text = "";
            dgPessoas.SelectedItem = null;
        }

        private void BtnEditar_Click(object sender, RoutedEventArgs e)
        {
            pessoaSelecionada = (Pessoa)dgPessoas.SelectedItem;
            if (pessoaSelecionada != null)
            {
                txtNome.Text = pessoaSelecionada.Nome;
                txtCPF.Text = pessoaSelecionada.CPF;
                txtEndereco.Text = pessoaSelecionada.Endereco;
            }
        }

        private void BtnSalvar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtNome.Text))
            {
                MessageBox.Show("Nome é obrigatório.");
                return;
            }

            if (!ValidadorCPF.Validar(txtCPF.Text))
            {
                MessageBox.Show("CPF inválido.");
                return;
            }

            if (pessoaSelecionada == null)
            {
                pessoaSelecionada = new Pessoa();
                //pessoaSelecionada.Id = pessoas.Count > 0 ? pessoas.Max(p => p.Id) + 1 : 1;
                pessoas.Add(pessoaSelecionada);
            }

            pessoaSelecionada.Nome = txtNome.Text;
            pessoaSelecionada.CPF = txtCPF.Text;
            pessoaSelecionada.Endereco = txtEndereco.Text;

            ArquivoHelper<Pessoa>.Save(pessoas);

            dgPessoas.Items.Refresh();
            MessageBox.Show("Pessoa salva com sucesso.");
        }

        private void BtnExcluir_Click(object sender, RoutedEventArgs e)
        {
            var selecionado = (Pessoa)dgPessoas.SelectedItem;
            if (selecionado != null)
            {
                pessoas.Remove(selecionado);
                ArquivoHelper<Pessoa>.Save(pessoas);
                dgPessoas.ItemsSource = null;
                dgPessoas.ItemsSource = pessoas;
            }
        }

        private void BtnIncluirPedido_Click(object sender, RoutedEventArgs e)
        {
            pessoaSelecionada = (Pessoa)dgPessoas.SelectedItem;

            if (pessoaSelecionada == null)
            {
                MessageBox.Show("Selecione uma pessoa.");
                return;
            }

            var pedidoWindow = new PedidoView(pessoaSelecionada);
            pedidoWindow.ShowDialog();
            AtualizarPedidosPessoa();
        }

        private void AtualizarPedidosPessoa()
        {
            pessoaSelecionada = (Pessoa)dgPessoas.SelectedItem;

            if (pessoaSelecionada == null) return;

            var pedidosPessoa = pedidos.Where(p => p.Pessoa.Id == pessoaSelecionada.Id).ToList();
            //var pedidosPessoa = pedidos.Where(p => p.Id == pessoaSelecionada.Id).ToList();

            // Aplicar filtros de status conforme checkboxes
            var statusFiltros = new List<string>();
            if (chkPendentes.IsChecked == true) statusFiltros.Add("Pendente");
            if (chkPagos.IsChecked == true) statusFiltros.Add("Pago");
            if (chkEntregues.IsChecked == true)
            {
                statusFiltros.Add("Enviado");
                statusFiltros.Add("Recebido");
            }

            pedidosPessoa = pedidosPessoa.Where(p => statusFiltros.Contains(p.Status)).ToList();

            dgPedidos.ItemsSource = pedidosPessoa;
        }

        private void FiltroPedidos_Checked(object sender, RoutedEventArgs e)
        {
            AtualizarPedidosPessoa();
        }

        // Eventos para marcar status do pedido:
        private void BtnMarcarPago_Click(object sender, RoutedEventArgs e)
        {
            AlterarStatusPedido("Pago");
        }

        private void BtnMarcarEnviado_Click(object sender, RoutedEventArgs e)
        {
            AlterarStatusPedido("Enviado");
        }

        private void BtnMarcarRecebido_Click(object sender, RoutedEventArgs e)
        {
            AlterarStatusPedido("Recebido");
        }

        private void AlterarStatusPedido(string novoStatus)
        {
            var pedido = (Pedido)dgPedidos.SelectedItem;
            if (pedido == null) return;

            if (pedido.Status == novoStatus)
            {
                MessageBox.Show($"Pedido já está marcado como {novoStatus}.");
                return;
            }

            pedido.Status = novoStatus;
            ArquivoHelper<Pedido>.Save(pedidos);
            AtualizarPedidosPessoa();
        }

        private void BtnbtnConsulta_Click(object sender, RoutedEventArgs e)
        {
            pedidos = ArquivoHelper<Pedido>.Load();

            this.AtualizarPedidosPessoa();
        }
    }
}