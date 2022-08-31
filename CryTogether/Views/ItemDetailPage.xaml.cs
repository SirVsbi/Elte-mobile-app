using CryTogether.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;
using CryTogether.Models;
using System;

namespace CryTogether.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {

            BindingContext = new ItemDetailViewModel();
            InitializeComponent();

        }
    }
}