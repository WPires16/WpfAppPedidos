using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppPedidos.Models
{
    public enum FormaPagamento
    {
        Dinheiro,
        Cartao,
        Boleto
    }

    public enum StatusPedido
    {
        Pendente,
        Pago,
        Enviado,
        Recebido
    }

    public class Pedido : INotifyPropertyChanged
    {
        private static int contador = 1;

        public Pedido()
        {
            Id = contador++;
            DataVenda = DateTime.Now;
            Status = StatusPedido.Pendente;
            Produtos = new List<ProdutoPedido>();
        }

        public int Id { get; private set; }

        private Pessoa pessoa;
        public Pessoa Pessoa
        {
            get => pessoa;
            set { pessoa = value; OnPropertyChanged(nameof(Pessoa)); }
        }

        private List<ProdutoPedido> produtos;
        public List<ProdutoPedido> Produtos
        {
            get => produtos;
            set { produtos = value; OnPropertyChanged(nameof(Produtos)); OnPropertyChanged(nameof(ValorTotal)); }
        }

        public decimal ValorTotal
        {
            get
            {
                decimal total = 0;
                if (Produtos != null)
                {
                    foreach (var pp in Produtos)
                        total += pp.ValorTotal;
                }
                return total;
            }
        }

        public DateTime DataVenda { get; set; }

        private FormaPagamento formaPagamento;
        public FormaPagamento FormaPagamento
        {
            get => formaPagamento;
            set { formaPagamento = value; OnPropertyChanged(nameof(FormaPagamento)); }
        }

        private StatusPedido status;
        public StatusPedido Status
        {
            get => status;
            set { status = value; OnPropertyChanged(nameof(Status)); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string prop) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}