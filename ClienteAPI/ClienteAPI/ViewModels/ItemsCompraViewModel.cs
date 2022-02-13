using ClienteAPI.Models;
using ClienteAPI.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using ClienteAPI.Services;

namespace ClienteAPI.ViewModels
{
    public class ItemsCompraViewModel : BaseViewModel
    {
        private Item _selectedItem;

        public ObservableCollection<Item> Items { get; }
        public Command LoadItemsCommand { get; }
        public Command<Item> ItemTapped { get; }

        public ItemsCompraViewModel()
        {
            Title = "Produtos Disponíveis";
            Items = new ObservableCollection<Item>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            ItemTapped = new Command<Item>(OnItemSelected);
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Items.Clear();
                List<Produto> produtos = await ProdutosService.ListarProdutos();


                foreach (var produto in produtos)
                {
                    Item item = new Item();
                    item.Id_Produto = produto.produto_id;
                    item.Nome_Produto = produto.nome;
                    item.Valor_em_Questao = $"R${produto.valor_unitario}";

                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await App.Current.MainPage.DisplayAlert($"Status Carregamento", "Falha ao acessar os Produtos", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
        }

        public Item SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        async void OnItemSelected(Item item)
        {
            if (item == null)
                return;

            try
            {
                await Shell.Current.GoToAsync($"{nameof(ComprarPage)}?{nameof(ComprarViewModel.ItemId)}={item.Id_Produto}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
    }
}
