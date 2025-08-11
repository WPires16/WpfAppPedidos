# WpfAppPedidos

Projeto WPF (.NET Framework 4.6) em padrão MVVM com persistência JSON usando **Newtonsoft.Json**.

## Estrutura
- Models/: modelos Pessoa, Produto, Pedido, ItemPedido
- ViewModels/: ViewModels para cada tela + infraestrutura (BaseViewModel, RelayCommand)
- Views/: MainWindow + UserControls para Pessoa, Produto e Pedido
- Services/: JsonFileService para ler/gravar arquivos JSON

## Requisitos
- Visual Studio 2019/2022
- .NET Framework 4.6
- NuGet package: **Newtonsoft.Json**

## Como abrir
1. Abra `WpfAppPedidos.csproj` no Visual Studio.
2. Restaure pacotes NuGet.
3. Compile e rode (F5).

## Observações
- Arquivos JSON gerados no diretório do executável: `Pessoas.json`, `Produtos.json`, `Pedidos.json`.
- Tela Pedidos usa listas carregadas de Pessoas e Produtos; finalize um pedido para continuar.
