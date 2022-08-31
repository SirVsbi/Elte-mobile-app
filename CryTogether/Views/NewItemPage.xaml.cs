using CryTogether.Models;
using CryTogether.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CryTogether.Views
{
    public partial class NewItemPage : ContentPage
    {
        public Breakdown Item { get; set; }
        public string Name = "Name";
        public NewItemPage()
        {
            
            InitializeComponent();
            BindingContext = new NewItemViewModel();
            
        }
    }
}