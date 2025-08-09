using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppPedidos.Models
{
    public class Pessoa : INotifyPropertyChanged
    {
        private static int contador = 1;

        public Pessoa()
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

        private string cpf;
        public string CPF
        {
            get => cpf;
            set { cpf = value; OnPropertyChanged(nameof(CPF)); }
        }

        private string endereco;
        public string Endereco
        {
            get => endereco;
            set { endereco = value; OnPropertyChanged(nameof(Endereco)); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string prop) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}