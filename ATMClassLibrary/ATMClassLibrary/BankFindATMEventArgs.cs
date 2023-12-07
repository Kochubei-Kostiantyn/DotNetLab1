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
