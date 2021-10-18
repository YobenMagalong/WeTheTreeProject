using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using WeTheTree.ViewModels;

namespace WeTheTree.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListView : ContentPage
    {
        public ListView()
        {
            InitializeComponent();
        }
    }
}