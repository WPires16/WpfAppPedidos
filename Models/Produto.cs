using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppPedidos.Models
{
    public class Produto : INotifyPropertyChanged
    {
        private static int contador = 1;

        public Produto()
        {
            Id = contador++;
        }

        public int Id { get; private set; }

        private string nome;
        public string Nome
        {
            get => nome;
            set { nome = value; OnPropertyChanged(nameof(Nome)); }
        }

        private string codigo;
        public string Codigo
        {
            get => codigo;
            set { codigo = value; OnPropertyChanged(nameof(Codigo)); }
        }

        private decimal valor;
        public decimal Valor
        {
            get => valor;
            set { valor = value; OnPropertyChanged(nameof(Valor)); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string prop) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}