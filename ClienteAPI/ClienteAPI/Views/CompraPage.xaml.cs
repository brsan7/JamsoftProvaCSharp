using ClienteAPI.Models;
using ClienteAPI.ViewModels;
using Xamarin.Forms;

namespace ClienteAPI.Views
{
    public partial class CompraPage : ContentPage
    {
        public CompraPage()
        {
            InitializeComponent();
            BindingContext = new CompraViewModel();
        }
    }
}