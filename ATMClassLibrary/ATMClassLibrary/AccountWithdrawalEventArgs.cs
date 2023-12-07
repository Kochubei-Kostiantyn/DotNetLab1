namespace ATMClassLibrary
{
    public class AccountWithdrawalEventArgs : EventArgs
    {
        public string result { get; private set; }
        public AccountWithdrawalEventArgs (string result)
        {
            this.result = result;
        }
    }
}
