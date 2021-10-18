using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WeTheTree.Services
{
    public interface iAuth
    {
        Task<string> LoginWithEmailAndPassword(string email, string password);
        Task<string> SignUpnWithEmailAndPassword(string email, string password);
        bool SignOut();
        bool IsSignIn();
    }
}
