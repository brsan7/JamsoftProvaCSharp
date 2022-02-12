using ClienteAPI.Models;
using ClienteAPI.ViewModels;
using ClienteAPI.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ClienteAPI.Views
{
    public partial class ItemsCompraPage : ContentPage
    {
        ItemsCompraViewModel _viewModel;

        public ItemsCompraPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new ItemsCompraViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}