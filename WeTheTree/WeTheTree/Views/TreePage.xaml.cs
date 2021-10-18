using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WeTheTree.Models;
using WeTheTree.Services;
using WeTheTree.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WeTheTree.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TreePage : ContentPage
    {
        iAuth auth;
        public TreePage()
        {
            InitializeComponent();
            BindingContext = new TreeViewModel();
            auth = DependencyService.Get<iAuth>();

            GetAddressCommand = new Command(async () => await OnGetAddress());
            GetPositionCommand = new Command(async () => await OnGetPosition());
        }

        string lat = "47.683988";
        string lon = "122.121513";
        string address = "Santa Rita, Pampanga, Philippines";
        string geocodeAddress;
        string geocodePosition;
        public ICommand GetAddressCommand { get; }
        public ICommand GetPositionCommand { get; }

        public string Latitude
        {
            get => lat;
            set => SetProperty(ref lat, value);

        }
        public string Longtitude
        {
            get => lon;
            set => SetProperty(ref lon, value);

        }
        public string GeocodeAddress
        {
            get => geocodeAddress;
            set => SetProperty(ref geocodeAddress, value);
        }
        public string Address
        {
            get => address;
            set => SetProperty(ref address, value);
        }
        public string GeocodePosition
        {
            get => geocodePosition;
            set => SetProperty(ref geocodePosition, value);
        }

        async Task OnGetPosition()
        {
            if (IsBusy)
                return;
            IsBusy = true;
            try
            {
                var locations = await Geocoding.GetLocationsAsync(Address);
                Location location = locations.FirstOrDefault();
                if (location == null)
                {
                    GeocodePosition = "Unable to detect locations";
                }
                else
                {
                    GeocodePosition =
                        $"{nameof(location.Latitude)}: {location.Latitude}\n" +
                        $"{nameof(location.Longitude)}: {location.Longitude}\n";
                }
            }
            catch (Exception ex)
            {
                GeocodePosition = $"Unable to detect locations: {ex.Message}";
            }
            finally
            {
                IsBusy = false;
            }
        }
        //function get address

        async Task OnGetAddress()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            try
            {
                double.TryParse(lat, out var lt);
                double.TryParse(lon, out var ln);
                var placemarks = await Geocoding.GetPlacemarksAsync(lt, ln);
                Placemark placemark = placemarks.FirstOrDefault();
                if (placemark == null)
                {
                    GeocodeAddress = "Unable to detect palcemark";
                }
                else
                {
                    GeocodeAddress =
                        $"{nameof(placemark.AdminArea)}: {placemark.AdminArea}\n" +
                        $"{nameof(placemark.CountryName)}: {placemark.CountryName}\n" +
                        $"{nameof(placemark.Locality)}: {placemark.Locality}\n";
                }
            }
            catch (Exception ex)
            {
                GeocodeAddress = $"Unable to detect placemarks: {ex.Message}";

            }
            finally
            {
                IsBusy = false;
            }
        }

        //SetProperty
        protected virtual bool SetProperty<T>(ref T backingStore, T value, [CallerMemberName] string propertyName = "", Action onCanged = null, Func<T, T, bool> validateValue = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;
            if (validateValue != null && !validateValue(backingStore, value))
                return false;
            backingStore = value;
            onCanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        void Logout_Clicked(object sender, EventArgs e)
        {
            var signOut = auth.SignOut();
            if (signOut)
            {
                Application.Current.MainPage = new LoginPage();
            }
        }
        public async void OnItemSelected(object sender, ItemTappedEventArgs args)
        {
            var tree = args.Item as Tree;
            if (tree == null) return;

            await Navigation.PushAsync(new TreeDetailPage(tree));
            lstTree.SelectedItem = null;
        }

        private void Clicked_List(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Views.ListView());

        }
    }
}