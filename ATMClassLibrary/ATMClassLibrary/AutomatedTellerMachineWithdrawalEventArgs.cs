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
