using CryTogether.Models;
using Plugin.GoogleClient;
using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace CryTogether.Views
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            NetworkAuthData emptydata = new NetworkAuthData();
            BindingContext = emptydata;
            InitializeComponent();
        }

        public HomePage(NetworkAuthData networkAuthData)
        {
            BindingContext = networkAuthData;
            InitializeComponent();
        }

        async void OnLogout(object sender, System.EventArgs e)
        {
            if (BindingContext is NetworkAuthData data)
            {
               CrossGoogleClient.Current.Logout();
               await Shell.Current.GoToAsync("//HomePage");
            }

        }
    }
}
