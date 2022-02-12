using System;
using System.Diagnostics;
using Xamarin.Forms;
using ClienteAPI.Services;

namespace ClienteAPI.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class CompraViewModel : BaseViewModel
    {
        public Command RemoveItemCommand { get; }
        public CompraViewModel()
        {
            RemoveItemCommand = new Command(OnRemoveItem);
        }
        private int itemId;
        private string nome_produto;
        private double valor_unitario;
        public int Id { get; set; }

        public string Nome_Produto
        {
            get => nome_produto;
            set => SetProperty(ref nome_produto, value);
        }

        public double Valor_Unitario
        {
            get => valor_unitario;
            set => SetProperty(ref valor_unitario, value);
        }

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

        public async void LoadItemId(int itemId)
        {
            try
            {
                var item = await ProdutosService.DetalharProduto(itemId);
                Id = item.produto_id;
                Nome_Produto = item.nome;
                Valor_Unitario = item.valor_unitario;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }

        private async void OnRemoveItem(object obj)
        {
            await ProdutosService.ExcluirProduto(ItemId);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}
