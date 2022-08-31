using CryTogether.ViewModels;
using CryTogether.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace CryTogether
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        LoginViewModel loginViewModel;
        public AppShell()
        {
            this.loginViewModel =  LoginViewModel.Instance;
            InitializeComponent();
            Routing.RegisterRoute("NewItemPage", typeof(NewItemPage));
            Routing.RegisterRoute("DetailPage", typeof(ItemDetailPage));

        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }

        private async void OnLogOut(object sender, EventArgs e)
        {
            await this.loginViewModel.LogoutGoogle();
        }
    }
}
