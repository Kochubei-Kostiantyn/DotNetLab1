namespace ATMClassLibrary
{
    public class AccountAuthenticationEventArgs : EventArgs
    {
        public string result { get; private set; }
        public AccountAuthenticationEventArgs (string result)
        {
            this.result = result;
        }
    }
}
