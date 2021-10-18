using System;
using WeTheTree.Services;
using WeTheTree.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WeTheTree
{
    public partial class App : Application
    {
        iAuth auth;
        public App()
        {
            InitializeComponent();

            auth = DependencyService.Get<iAuth>();
            if (auth.IsSignIn())
            {
                MainPage = new NavigationPage(new Views.LoginPage());
            }
            else
            {
                MainPage = new LoginPage(); 
            }
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
