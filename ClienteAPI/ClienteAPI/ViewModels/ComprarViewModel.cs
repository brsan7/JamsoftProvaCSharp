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
        private bool forma_pagamento;
        private string _titular;
        private string _numero_cartao;
        private string _data_expiracao;
        private string _bandeira;
        private string _cvv;
        Produto produto = new Produto();

        public Command ComprarCommand { get; }
        public Command CancelarCommand { get; }

        public ComprarViewModel()
        {
            MockCartao();
            ComprarCommand = new Command(OnComprar, ValidarCompra);
            CancelarCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => ComprarCommand.ChangeCanExecute();
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
        public bool Forma_Pagamento_Selecionada
        {
            get => forma_pagamento;
            set => SetProperty(ref forma_pagamento, value);
        }
        public string Titular
        {
            get => _titular;
            set => SetProperty(ref _titular, value);
        }
        public string Numero_Cartao
        {
            get => _numero_cartao;
            set => SetProperty(ref _numero_cartao, value);
        }
        public string Data_Expiracao
        {
            get => _data_expiracao;
            set => SetProperty(ref _data_expiracao, value);
        }
        public string Bandeira
        {
            get => _bandeira;
            set => SetProperty(ref _bandeira, value);
        }
        public string CVV
        {
            get => _cvv;
            set => SetProperty(ref _cvv, value);
        }


        public async void LoadItemId(int itemId)
        {
            try
            {
                produto = await ProdutosService.DetalharProduto(itemId);
                NomeProduto = produto.nome;
                ValorUnitario = $"R${produto.valor_unitario}";
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
                await App.Current.MainPage.DisplayAlert($"Status Carregamento", "Falha ao acessar o Produto", "OK");
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
                {
                    titular = Titular,
                    numero_cartao = Numero_Cartao,
                    data_expiracao = Data_Expiracao,
                    bandeira = Bandeira,
                    cvv = CVV
                }
            };
            var resposta = await ComprasService.ComprarProduto(compra);
            await App.Current.MainPage.DisplayAlert($"Status da Compra", ServiceMsg.FormatarResposta(resposta), "OK");
            
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
                return false;
            }

            return Forma_Pagamento_Selecionada
                && Convert.ToInt32(Quantidade) > 0;
        }

        private void MockCartao()
        {
            Titular = "John Doe";
            Numero_Cartao = "4111111111111111";
            Data_Expiracao = "12/2018";
            Bandeira = "VISA";
            CVV = "123";
        }
    }
}
