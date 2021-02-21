using Equipment.ViewModels;
using FreshMvvm;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Equipment
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            var page = FreshPageModelResolver.ResolvePageModel<LoginViewModel>();
            var basicNavContainer = new FreshNavigationContainer(page);
            MainPage = basicNavContainer;
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
