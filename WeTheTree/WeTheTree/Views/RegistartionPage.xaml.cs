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
    public partial class RegistartionPage : ContentPage
    {
        iAuth auth;
        public RegistartionPage()
        {
            InitializeComponent();
            auth = DependencyService.Get<iAuth>();
        }
        async void Register_Clicked(object sender, EventArgs e)
        {
            var user = auth.SignUpnWithEmailAndPassword(EmailReg.Text, PasswordReg.Text);
            if (user != null)
            {
                await DisplayAlert("Success", "New Member is Created", "OK");
                var signOut = auth.SignOut();
                if (signOut)
                {
                    Application.Current.MainPage = new LoginPage();
                }
            }
            else
            {
                await DisplayAlert("Error", "Something Went Wrong, Please Try Again.", "OK");
            }
        }
        private void login_Tapped(object sender, EventArgs args)
        {
            Navigation.PushAsync(new LoginPage());

        }

    }
}