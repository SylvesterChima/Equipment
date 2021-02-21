using Equipment.Models;
using FreshMvvm;
using Newtonsoft.Json;
using Plugin.GoogleClient;
using Plugin.GoogleClient.Shared;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Equipment.ViewModels
{
    public class LoginViewModel: BaseViewModel
    {
        IGoogleClientManager _googleService = CrossGoogleClient.Current;
        public ICommand OnLoginCommand { get; set; }


        public LoginViewModel()
        {
            OnLoginCommand = new Command(async () => await LoginAsync());
        }

        async Task LoginAsync()
        {
            await LoginGoogleAsync();
        }

        public override Task Initialize(object initData)
        {
            return base.Initialize(initData);
        }

        async Task LoginGoogleAsync()
        {
            try
            {
                if (!string.IsNullOrEmpty(_googleService.AccessToken))
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

                            var socialLoginData = new EquipmentsViewModel.Nav
                            {
                                Data= new NetworkAuthData
                                {
                                    Id = e.Data.Id,
                                    Picture = e.Data.Picture.AbsoluteUri,
                                    Name = e.Data.Name,
                                }
                            };
                            //await this.CoreMethods.PushPageModel<EquipmentsViewModel>(socialLoginData, true);
                            var page = FreshPageModelResolver.ResolvePageModel<EquipmentsViewModel>(socialLoginData);
                            var basicNavContainer = new FreshNavigationContainer(page);
                            Application.Current.MainPage = basicNavContainer;
                            break;
                        case GoogleActionStatus.Canceled:
                            await this.CoreMethods.DisplayAlert("Google Auth", "Canceled", "Ok");
                            break;
                        case GoogleActionStatus.Error:
                            await this.CoreMethods.DisplayAlert("Google Auth", "Error", "Ok");
                            break;
                        case GoogleActionStatus.Unauthorized:
                            await this.CoreMethods.DisplayAlert("Google Auth", "Unauthorized", "Ok");
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

    }
}
