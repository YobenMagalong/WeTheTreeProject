using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;
using WeTheTree.Models;

namespace WeTheTree.Services
{
    public class DBFirebase
    {
        FirebaseClient client;

        public DBFirebase()
        {
            client = new FirebaseClient("https://wethetree-default-rtdb.firebaseio.com/");
        }
        public ObservableCollection<Tree> getTree()
        {
            var treeData = client
                .Child("Tree")
                .AsObservable<Tree>()
                .AsObservableCollection();

            return treeData;
        }

        public async Task AddTree(int TreeID, string CommonName, string ScientificName, string Notes, int Height, string DMB, string TrunkWoods, string Mushroom, string Canopy,
            string CanopyPerimeter, string Leaves, string Trunk, string Mulch)
        {
            Tree em = new Tree()
            {
                treeid = TreeID,
                commonname = CommonName,
                scientificname = ScientificName,
                notes = Notes,
                height = Height,
                dmb = DMB,
                trunkwoods = TrunkWoods,
                mushroom = Mushroom,
                canopy = Canopy,
                canopyperimeter = CanopyPerimeter,
                leaves = Leaves,
                trunk = Trunk,
                mulch = Mulch
            };

            await client
                .Child("Tree")
                .PostAsync(em);
        }
        public async Task DeleteTree(int TreeID, string CommonName, string ScientificName, string Notes, int Height, string DMB, string TrunkWoods, string Mushroom, string Canopy,
            string CanopyPerimeter, string Leaves, string Trunk, string Mulch)
        {
            var toDeleteTree = (await client
                .Child("Tree")
                .OnceAsync<Tree>()).FirstOrDefault
                (a => a.Object.treeid == TreeID || a.Object.commonname == CommonName);
            await client.Child("Tree").Child(toDeleteTree.Key).DeleteAsync();
        }

        public async Task UpdateTree(int TreeID, string CommonName, string ScientificName, string Notes, int Height, string DMB, string TrunkWoods, string Mushroom, string Canopy,
            string CanopyPerimeter, string Leaves, string Trunk, string Mulch)
        {
            var toUpdateTree = (await client
                .Child("Tree")
                .OnceAsync<Tree>()).FirstOrDefault
                (a => a.Object.commonname == CommonName);

            Tree em = new Tree()
            {
                treeid = TreeID,
                commonname = CommonName,
                scientificname = ScientificName,
                notes = Notes,
                height = Height,
                dmb = DMB,
                trunkwoods = TrunkWoods,
                mushroom = Mushroom,
                canopy = Canopy,
                canopyperimeter = CanopyPerimeter,
                leaves = Leaves,
                trunk = Trunk,
                mulch = Mulch
            };

            await client
                .Child("Tree")
                .Child(toUpdateTree.Key)
                .PostAsync(em);
        }
    }
}
