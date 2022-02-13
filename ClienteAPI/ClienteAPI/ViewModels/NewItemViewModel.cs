using ClienteAPI.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using ClienteAPI.Services;

namespace ClienteAPI.ViewModels
{
    public class NewItemViewModel : BaseViewModel
    {
        private string nome_produto;
        private string valor_unitario;
        private string qtde_estoque;

        public NewItemViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
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

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            Produto produto = new Produto()
            {
                nome = Nome_Produto,
                valor_unitario = Convert.ToDouble(Valor_Unitario),
                qtde_estoque = Convert.ToInt32(Qtde_Estoque)
            };
            await ProdutosService.RegistrarProduto(produto);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private bool ValidateSave()
        {
            try
            {
                if (Convert.ToDouble(Valor_Unitario) <= 0) return false;
                if (Convert.ToInt32(Qtde_Estoque) <= 0) return false;
            }
            catch (Exception)
            {
                return false;
            }
            return !String.IsNullOrWhiteSpace(Nome_Produto);
        }
    }
}
