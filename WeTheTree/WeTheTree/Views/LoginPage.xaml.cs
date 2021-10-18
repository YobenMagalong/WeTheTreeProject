using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using WeTheTree.Services;

namespace WeTheTree.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        iAuth auth;
        public LoginPage()
        {
            InitializeComponent();
            auth = DependencyService.Get<iAuth>();
        }
        async void Login_Clicked(object sender, EventArgs e)
        {
            string token = await auth.LoginWithEmailAndPassword(EmailLogin.Text, PasswordLogin.Text);
            if (token != string.Empty)
            {
                Application.Current.MainPage = new NavigationPage(new Views.TreePage());
            }
            else
            {
                await DisplayAlert("Authentication Failed", "Email or Password is incorrect", "OK");
            }
        }
         void register_Tapped(object sender, EventArgs args )
        {
            var signOut = auth.SignOut();
            if (signOut)
            {
                Application.Current.MainPage = new Views.RegistartionPage();

            }

        }
    }
}