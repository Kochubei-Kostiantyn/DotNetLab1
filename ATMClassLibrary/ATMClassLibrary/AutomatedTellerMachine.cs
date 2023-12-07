namespace ATMClassLibrary
{
    public class AutomatedTellerMachine
    {
        public double balance { get; private set; }
        public string ID { get; private set; }
        public string address { get; private set; }
        public event EventHandler<AutomatedTellerMachineWithdrawalEventArgs> WithdrawalEvent;

        public AutomatedTellerMachine(double balance, string ID, string address)
        {
            this.balance = balance;
            this.ID = ID;
            this.address = address;
        }
        public bool withdrawal(double balance)
        {
            if (this.balance < balance)
            {
                if (WithdrawalEvent != null)
                    WithdrawalEvent(this, new AutomatedTellerMachineWithdrawalEventArgs("Закінчилась готівка! Спробуйте провести операцію в іншому банкоматі!"));
                return false;
            }
            if (WithdrawalEvent != null)
                WithdrawalEvent(this, new AutomatedTellerMachineWithdrawalEventArgs("Банкомат успішно видав " + balance.ToString() + "грн!"));
            this.balance -= balance;
            return true;
        }
        public void charging(double balance)
        {
            this.balance += balance;
            return;
        }
    }
}
