using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CryTogether.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {

        public string Name;


        public AboutViewModel()
        {
            Title = "About";
            Name = LoginViewModel.Instance.getUserName();
        }

    }
}