using System.ComponentModel;

namespace WpfAppPedidos.Models
{
    public class Pessoa : INotifyPropertyChanged
    {
        //private static int contador = 1;
        //private int Id;

        public Pessoa()
        {
            //Id = contador++;
        }

        public int Id { get; set; }

        //private int Id;
        //public int Id
        //{
        //    get => Id;
        //    set { Id = value; OnPropertyChanged(nameof(Id)); }
        //}

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