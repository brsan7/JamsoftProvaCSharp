using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ClienteAPI.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "Apresentação";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://github.com/brsan7/JamsoftProvaCSharp/blob/master/README.md"));
        }

        public ICommand OpenWebCommand { get; }
    }
}