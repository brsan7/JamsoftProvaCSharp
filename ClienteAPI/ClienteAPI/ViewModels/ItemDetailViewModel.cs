using System;
using System.Diagnostics;
using Xamarin.Forms;
using ClienteAPI.Services;
using System.Globalization;

namespace ClienteAPI.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class ItemDetailViewModel : BaseViewModel
    {
        public ItemDetailViewModel()
        {
            RemoveItemCommand = new Command(OnRemoveItem);
        }

        public Command RemoveItemCommand { get; }

        private int itemId;
        private string produto_id;
        private string nome_produto;
        private string valor_unitario;
        private string qtde_estoque;
        private string data_ultima_compra;
        private string valor_ultima_compra;

        public int ItemId
        {
            get
            {
                return itemId;
            }
            set
            {
                itemId = value;
                LoadItemId(value);
            }
        }
        public string Produto_Id
        {
            get => produto_id;
            set => SetProperty(ref produto_id, value);
        }
        public string Nome_Produto
        {
            get => nome_produto;
            set => SetProperty(ref nome_produto, value);
        }

        public string Valor_Unitario
        {
            get => valor_unitario;
            set => SetProperty(ref valor_unitario, value);
        }

        public string Qtde_Estoque
        {
            get => qtde_estoque;
            set => SetProperty(ref qtde_estoque, value);
        }

        public string Data_Ultima_Compra
        {
            get => data_ultima_compra;
            set => SetProperty(ref data_ultima_compra, value);
        }

        public string Valor_Ultima_Compra
        {
            get => valor_ultima_compra;
            set => SetProperty(ref valor_ultima_compra, value);
        }

        public async void LoadItemId(int itemId)
        {
            try
            {
                var item = await ProdutosService.DetalharProduto(itemId);
                string _valor_ultima_compra = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", item.valor_ultima_compra);
                string _valor_unitario = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", item.valor_unitario);

                Produto_Id = $"ID: {item.produto_id}";
                Nome_Produto = $"Nome_Produto:{Environment.NewLine}{item.nome}";
                Valor_Unitario = $"Valor_Unitario:{Environment.NewLine} {_valor_unitario}";
                Qtde_Estoque = $"Qtde_Estoque: {item.qtde_estoque}";
                Data_Ultima_Compra = $"Data_Ultima_Compra:{Environment.NewLine}{item.data_ultima_compra}";
                Valor_Ultima_Compra = $"Valor_Ultima_Compra:{Environment.NewLine} {_valor_ultima_compra}";
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
                await App.Current.MainPage.DisplayAlert($"Status Carregamento", "Falha ao acessar o Produto", "OK");
            }
        }

        private async void OnRemoveItem(object obj)
        {
            var resposta = await ProdutosService.ExcluirProduto(ItemId);
            await App.Current.MainPage.DisplayAlert($"Status da Remoção", ServiceMsg.FormatarResposta(resposta), "OK");

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}
