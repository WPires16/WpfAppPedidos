using System.ComponentModel;

namespace WpfAppPedidos.Models
{
    //    public class Produto : INotifyPropertyChanged
    //    {
    //        private static int contador = 1;

    //        public Produto()
    //        {
    //            Id = contador++;
    //        }

    //        public int Id { get; private set; }

    //        private string nome;
    //        public string Nome
    //        {
    //            get => nome;
    //            set { nome = value; OnPropertyChanged(nameof(Nome)); }
    //        }

    //        private string codigo;
    //        public string Codigo
    //        {
    //            get => codigo;
    //            set { codigo = value; OnPropertyChanged(nameof(Codigo)); }
    //        }

    //        private decimal valor;
    //        public decimal Valor
    //        {
    //            get => valor;
    //            set { valor = value; OnPropertyChanged(nameof(Valor)); }
    //        }

    //        public event PropertyChangedEventHandler PropertyChanged;
    //        protected void OnPropertyChanged(string prop) =>
    //            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    //    }

    public class Produto : INotifyPropertyChanged
    {
        private int id;
        private string nome;
        private string codigo;
        private decimal valor;

        public int Id
        {
            get => id;
            set { id = value; OnPropertyChanged(nameof(Id)); }
        }

        public string Nome
        {
            get => nome;
            set { nome = value; OnPropertyChanged(nameof(Nome)); }
        }

        public string Codigo
        {
            get => codigo;
            set { codigo = value; OnPropertyChanged(nameof(Codigo)); }
        }

        public decimal Valor
        {
            get => valor;
            set { valor = value; OnPropertyChanged(nameof(Valor)); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string nomeProp)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nomeProp));
        }
    }
}