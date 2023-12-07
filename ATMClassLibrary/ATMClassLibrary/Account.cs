namespace ATMClassLibrary
{
    public class Account
    {
        public string cardNumber { get; private set; }
        public string firstName { get; private set; }
        public string lastName { get; private set; }
        public double balance { get; private set; }
        public string PIN { get; private set; }
        public event EventHandler<AccountAuthenticationEventArgs> AuthenticationEvent;
        public event EventHandler<AccountWithdrawalEventArgs> WithdrawalEvent;
        public event EventHandler<AccountGetBalanceEventArgs> GetBalanceEvent;

        public Account (string cardNumber, string firstName, string lastName, double balance, string PIN)
        {
            this.cardNumber = cardNumber;
            this.firstName = firstName;
            this.lastName = lastName;
            this.balance = balance;
            this.PIN = PIN;
        }
        public bool authentication (string PIN)
        {
            string result;
            if (this.PIN.Equals(PIN))
                result = "Аутентифікація картки успішна! Користувач: " + firstName + " " + lastName + ".";
            else
                result = "Аутентифікація картки неуспішна! Введено неправильний пін-код";
            if (AuthenticationEvent != null)
                AuthenticationEvent(this, new AccountAuthenticationEventArgs(result));
            return this.PIN.Equals (PIN);
        }
        public double getBalance ()
        {
            if (GetBalanceEvent != null)
                GetBalanceEvent(this, new AccountGetBalanceEventArgs("Баланс картки: " + this.balance.ToString()));
            return this.balance;
        }
        public bool withdrawal (double balance)
        {
            if (this.balance < balance)
            {
                if (WithdrawalEvent != null)
                    WithdrawalEvent(this, new AccountWithdrawalEventArgs("Недостатньо коштів!"));
                return false;
            }
            this.balance -= balance;
            if (WithdrawalEvent != null)
                WithdrawalEvent(this, new AccountWithdrawalEventArgs("Операція зняття пройша успішно!"));
            return true;
        }
        public void charging(double balance)
        {
            this.balance += balance;
            return;
        }
    }
}