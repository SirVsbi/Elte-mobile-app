using CryTogether.Models;
using CryTogether.ViewModels;
using CryTogether.Views;
using Google.Android.Material.Snackbar;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CryTogether.Views
{
    public partial class ItemsPage : ContentPage
    {
        ItemsViewModel _viewModel;

        public ItemsPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new ItemsViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }



        async void Swipe_Event(object sender, EventArgs e)
        {
            var item = ((SwipeItem)sender).BindingContext as Breakdown;
            if(item == null)
            {
                return;
            }
            await Shell.Current.GoToAsync($"DetailPage?itemId={item.Id}");
        }


        async void Swipe_Delete_Event(object sender, EventArgs e)
        {
            var item = ((SwipeItem)sender).BindingContext as Breakdown;
            if (item == null)
            {
                return;
            }
            if(item.suferer != LoginViewModel.Instance.getUserName())
            {
                return;
            }
            await _viewModel.DeleteItem(item.Id);
            //Snackbar.Make(this, "🦀🦀🦀 YOUR SUFFERING IS GONE 🦀🦀🦀", Snackbar.LengthIndefinite).Show();
        }
    }
}