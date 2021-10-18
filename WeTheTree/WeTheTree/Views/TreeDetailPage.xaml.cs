using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeTheTree.Models;
using WeTheTree.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WeTheTree.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TreeDetailPage : ContentPage
    {
        DBFirebase services;
        public TreeDetailPage(Tree tree)
        {
            InitializeComponent();
            BindingContext = tree;
            services = new DBFirebase();
        }

        public async void BtnDelete(object sender, EventArgs e)
        {
            await services.DeleteTree(int.Parse(TreeID.Text), CommonName.Text, ScientificName.Text, Notes.Text, int.Parse(Height.Text), DMB.Text, TrunkWoods.Text, Mushroom.Text, Canopy.Text, CanopyPerimeter.Text, Leaves.Text, Trunk.Text, Mulch.Text);
            await Navigation.PushAsync(new TreePage());
        }

        public async void BtnUpdate(object sender, EventArgs e)
        {
            await services.UpdateTree(int.Parse(TreeID.Text), CommonName.Text, ScientificName.Text, Notes.Text, int.Parse(Height.Text), DMB.Text, TrunkWoods.Text, Mushroom.Text, Canopy.Text, CanopyPerimeter.Text, Leaves.Text, Trunk.Text, Mulch.Text);
            await Navigation.PushAsync(new TreePage());
        }
    }
}