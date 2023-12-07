namespace ATMClassLibrary
{
    public class AccountGetBalanceEventArgs : EventArgs
    {
        public string balance { get; private set; }
        public AccountGetBalanceEventArgs(string balance)
        {
            this.balance = balance;
        }
    }
}
