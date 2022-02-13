using ClienteAPI.Models;
using ClienteAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ClienteAPI.Views
{
    public partial class ComprarPage : ContentPage
    {
        public Item Item { get; set; }

        public ComprarPage()
        {
            InitializeComponent();
            BindingContext = new ComprarViewModel();
        }

        private void RadioButton_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            this.FindByName<StackLayout>("credito").IsVisible = true;
        }
    }
}