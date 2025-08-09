using System.Collections.ObjectModel;
using System.Linq;
using WpfAppPedidos.Models;
using WpfAppPedidos.Dados;
using System.Collections.Generic;

namespace WpfCrudPedidos.ViewModels
{
    public class PessoaViewModel
    {
        public ObservableCollection<Pessoa> Pessoas { get; set; } = new ObservableCollection<Pessoa>();

        private List<Pessoa> pessoasOriginais;

        public PessoaViewModel()
        {
            pessoasOriginais = ArquivoHelper<Pessoa>.Load();
            Pessoas = new ObservableCollection<Pessoa>(pessoasOriginais);
        }

        public void Filtrar(string nomeFiltro, string cpfFiltro)
        {
            var filtradas = pessoasOriginais.Where(p =>
                (string.IsNullOrEmpty(nomeFiltro) || p.Nome.ToLower().Contains(nomeFiltro.ToLower())) &&
                (string.IsNullOrEmpty(cpfFiltro) || p.CPF.Contains(cpfFiltro))
            ).ToList();

            Pessoas.Clear();
            foreach (var p in filtradas)
                Pessoas.Add(p);
        }

        public void Salvar()
        {
            ArquivoHelper<Pessoa>.Save(pessoasOriginais);
        }

        public void Incluir(Pessoa pessoa)
        {
            //pessoa.Id = pessoasOriginais.Count > 0 ? pessoasOriginais.Max(p => p.Id) + 1 : 1;
            pessoasOriginais.Add(pessoa);
            Pessoas.Add(pessoa);
        }

        public void Excluir(Pessoa pessoa)
        {
            pessoasOriginais.Remove(pessoa);
            Pessoas.Remove(pessoa);
        }
    }
}