using ClienteAPI.Views;
using Xamarin.Forms;

namespace ClienteAPI
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
            Routing.RegisterRoute(nameof(ComprarPage), typeof(ComprarPage));
        }
    }
}
