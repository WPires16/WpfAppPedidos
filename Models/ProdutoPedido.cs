using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppPedidos.Models
{
    public class ProdutoPedido : INotifyPropertyChanged
    {
        private Produto produto;
        public Produto Produto
        {
            get => produto;
            set { produto = value; OnPropertyChanged(nameof(Produto)); OnPropertyChanged(nameof(ValorTotal)); }
        }

        private int quantidade;
        public int Quantidade
        {
            get => quantidade;
            set { quantidade = value; OnPropertyChanged(nameof(Quantidade)); OnPropertyChanged(nameof(ValorTotal)); }
        }

        public decimal ValorTotal => Produto != null ? Produto.Valor * Quantidade : 0;

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string prop) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}