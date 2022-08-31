using CryTogether.Services;
using CryTogether.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CryTogether
{
    public partial class App : Application
    {

        public App()
        {
            Xamarin.Forms.Device.SetFlags(new string[] { "AppTheme_Experimental" });
            InitializeComponent();

            DependencyService.Register<RestService>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
