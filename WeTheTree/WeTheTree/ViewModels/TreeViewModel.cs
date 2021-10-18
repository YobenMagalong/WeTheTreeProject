using System;
using System.Collections.Generic;
using System.Text;

using System.Threading.Tasks;
using WeTheTree.Models;
using WeTheTree.Services;
using MvvmHelpers;
using Xamarin.Forms;
using System.Collections.ObjectModel;

namespace WeTheTree.ViewModels
{
   public class TreeViewModel : BaseViewModel
    {
        public int treeid { get; set; }
        public string commonname { get; set; }
        public string scientificname { get; set; }
        public string notes { get; set; }
        public string location { get; set; }
        public string image { get; set; }
        public int height { get; set; }
        public string dmb { get; set; }
        public string trunkwoods { get; set; }
        public string mushroom { get; set; }
        public string canopy { get; set; }
        public string canopyperimeter { get; set; }
        public string leaves { get; set; }
        public string trunk { get; set; }
        public string mulch { get; set; }

        public Command AddTreeCommand { get; }
        public ObservableCollection<Tree> _trees = new ObservableCollection<Tree>();
        private DBFirebase services;

        public ObservableCollection<Tree> Trees
        {
            get { return _trees; }
            set
            {
                _trees = value;
                OnPropertyChanged();
            }
        }

        public TreeViewModel()
        {
            services = new DBFirebase();
            Trees = services.getTree();
            AddTreeCommand = new Command(async () => await addTreeAsync(treeid, commonname, scientificname, notes, height, dmb, trunkwoods, mushroom, canopy, canopyperimeter, leaves, trunk, mulch));          
        }

        private async Task addTreeAsync(int treeid, string commonname, string scientificname, string notes, int height, string dmb, string trunkwoods, string mushroom, string canopy, string canopyperimeter, string leaves, string trunk, string mulch)
        {
            await services.AddTree(treeid, commonname, scientificname, notes, height, dmb, trunkwoods, mushroom, canopy, canopyperimeter, leaves, trunk, mulch);
        }
    }
}
