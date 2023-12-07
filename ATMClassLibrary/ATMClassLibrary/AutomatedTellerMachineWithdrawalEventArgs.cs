using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMClassLibrary
{
    public class AutomatedTellerMachineWithdrawalEventArgs : EventArgs
    {
        public string result { get; private set; }
        public AutomatedTellerMachineWithdrawalEventArgs(string result)
        {
            this.result = result;
        }
    }
}
