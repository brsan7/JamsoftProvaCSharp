using ClienteAPI.Models;
using System;
using System.Diagnostics;
using Xamarin.Forms;
using ClienteAPI.Services;

namespace ClienteAPI.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class ComprarViewModel : BaseViewModel
    {
        private int itemId;
        private string nomeProduto;
        private string valorUnitario;
        private string quantidade;
        private string total;
        Produto produto = new Produto();

        public ComprarViewModel()
        {
            ComprarCommand = new Command(OnComprar, ValidarCompra);
            CancelarCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => ComprarCommand.ChangeCanExecute();
        }

        public Command ComprarCommand { get; }
        public Command CancelarCommand { get; }
        

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
        public string NomeProduto
        {
            get => nomeProduto;
            set => SetProperty(ref nomeProduto, value);
        }
        public string ValorUnitario
        {
            get => valorUnitario;
            set => SetProperty(ref valorUnitario, value);
        }
        public string Quantidade
        {
            get => quantidade;
            set => SetProperty(ref quantidade, value);
        }
        public string Total
        {
            get => total;
            set => SetProperty(ref total, value);
        }

        public async void LoadItemId(int itemId)
        {
            try
            {
                produto = await ProdutosService.DetalharProduto(itemId);
                NomeProduto = produto.nome;
                ValorUnitario = $"Total: R${produto.valor_unitario}";
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async void OnComprar()
        {
            Compra compra = new Compra()
            {
                produto_id = produto.produto_id,
                qtde_comprada = Convert.ToInt32(Quantidade),
                Cartao = new Cartao()
            };
            await ComprasService.ComprarProduto(compra);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private bool ValidarCompra()
        {
            try
            {
                Total = $"Total: R${produto.valor_unitario * Convert.ToInt32(Quantidade)}";
            }
            catch (Exception)
            {
                Total = "Total: R$0";
            }

            return !String.IsNullOrWhiteSpace(Quantidade)
                && Convert.ToInt32(Quantidade) > 0;
        }
    }
}
