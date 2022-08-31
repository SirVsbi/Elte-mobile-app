using CryTogether.Models;
using Newtonsoft.Json;
using Plugin.GoogleClient;
using Plugin.GoogleClient.Shared;
using System;
using System.Diagnostics;
using CryTogether.Views;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace CryTogether.ViewModels
{

    public class LoginViewModel
    {


        IGoogleClientManager _googleService = CrossGoogleClient.Current;

        private static LoginViewModel instance = null;
        private static readonly object padlock = new object();
        private NetworkAuthData socialLoginData;
        public ICommand onLoginCommand { get; set; }



        private LoginViewModel()
        {

            onLoginCommand = new Command<AuthNetwork>(async (data) => await LoginAsync(data));
        }


        public static LoginViewModel Instance
        {
            get
            {
                lock (padlock)
                {

                    if (instance == null)
                    {
                        instance = new LoginViewModel();
                    }
                    return instance;
                }
            }
        }

        public ObservableCollection<AuthNetwork> AuthenticationNetworks { get; set; } = new ObservableCollection<AuthNetwork>()
        {
              new AuthNetwork()
            {
                Name = "Google",
                Icon = "ic_google",
                Foreground = "#000000",
                Background ="#F8F8F8"
            }
        };

        async Task LoginAsync(AuthNetwork authNetwork)
        {
            switch (authNetwork.Name)
            {
                case "Google":
                    await LoginGoogleAsync(authNetwork);
                    break;
            }
        }

        async Task LoginGoogleAsync(AuthNetwork authNetwork)
        {
            try
            {
                if (!string.IsNullOrEmpty(_googleService.IdToken))
                {
                    //Always require user authentication
                    _googleService.Logout();
                }

                EventHandler<GoogleClientResultEventArgs<GoogleUser>> userLoginDelegate = null;
                userLoginDelegate = async (object sender, GoogleClientResultEventArgs<GoogleUser> e) =>
                {
                    switch (e.Status)
                    {
                        case GoogleActionStatus.Completed:
#if DEBUG
                            var googleUserString = JsonConvert.SerializeObject(e.Data);
                            Debug.WriteLine($"Google Logged in succesfully: {googleUserString}");
#endif

                            socialLoginData = new NetworkAuthData
                            {
                                Id = e.Data.Id,
                                Logo = authNetwork.Icon,
                                Foreground = authNetwork.Foreground,
                                Background = authNetwork.Background,
                                Picture = e.Data.Picture.AbsoluteUri,
                                Name = e.Data.Name,
                            };

                            await Shell.Current.GoToAsync("//HomePage");
                            break;
                        case GoogleActionStatus.Canceled:
                            await App.Current.MainPage.DisplayAlert("Google Auth", "Canceled", "Ok");
                            break;
                        case GoogleActionStatus.Error:
                            await App.Current.MainPage.DisplayAlert("Google Auth", "Error", "Ok");
                            break;
                        case GoogleActionStatus.Unauthorized:
                            await App.Current.MainPage.DisplayAlert("Google Auth", "Unauthorized", "Ok");
                            break;
                    }

                    _googleService.OnLogin -= userLoginDelegate;
                };

                _googleService.OnLogin += userLoginDelegate;

                await _googleService.LoginAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        public bool IsLogedIn() => _googleService.IsLoggedIn;

        public async Task LogoutGoogle()
        {
            _googleService.Logout();
            await Shell.Current.GoToAsync("//LoginPage");
        }


        public string getUserName()
        {
            return _googleService.CurrentUser.Name;
        }
    }
}