using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMClassLibrary
{
    public class BankFindATMEventArgs : EventArgs
    {
        public string result { get; private set; }
        public BankFindATMEventArgs(string result)
        {
            this.result = result;
        }
    }
}
