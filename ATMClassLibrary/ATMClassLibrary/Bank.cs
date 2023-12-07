using System.Net.NetworkInformation;
using System.Security.Principal;

namespace ATMClassLibrary
{
    public class Bank
    {
        public string name { get; private set; }
        public List<Account> accounts { get; private set; }
        public List<AutomatedTellerMachine> automatedTellerMachines { get; private set; }
        public event EventHandler<BankFindATMEventArgs> FindATMEvent;
        public event EventHandler<BankFindAccountEventArgs> FindAccountEvent;

        public Bank (string name, List<Account> accounts, List<AutomatedTellerMachine> automatedTellerMachines)
        {
            this.name = name;
            this.accounts = accounts;
            this.automatedTellerMachines = automatedTellerMachines;
        }
        public short findATM (string ID)
        {
            for (short i = 0; i < automatedTellerMachines.Count; i++)
            {
                if (automatedTellerMachines[i].ID.Equals(ID))
                {
                    if (FindATMEvent != null)
                        FindATMEvent(this, new BankFindATMEventArgs("Підключення до банкомату з номером " + ID + " успішне!"));
                    return i;
                }
            }
            if (FindATMEvent != null)
                FindATMEvent(this, new BankFindATMEventArgs("Банкомату з номером " + ID + " не знайдено!"));
            return -1;
        }
        public short findAccount (string cardNumber)
        {
            for (short i = 0; i < accounts.Count; i++)
            {
                if (accounts[i].cardNumber.Equals(cardNumber))
                {
                    if (FindATMEvent != null)
                        FindATMEvent(this, new BankFindATMEventArgs("Картка з номером " + cardNumber + " в базі знайдена!"));
                    return i;
                }
            }
            if (FindATMEvent != null)
                FindATMEvent(this, new BankFindATMEventArgs("Картки з номером " + cardNumber + " в базі не знайдено!"));
            return -1;
        }
        public bool withdrawal (string ID, string cardNumber, string PIN, double balance)
        {
            short iATM = findATM (ID);
            if (iATM == -1)
            {
                return false;
            }
            short iAccount = findAccount (cardNumber);
            if (iAccount == -1)
            {
                return false;
            }
            if (!accounts[iAccount].authentication(PIN))
            {
                return false;
            }

            if (!accounts[iAccount].withdrawal(balance))
            {
                return false;
            }
            if (!automatedTellerMachines[iATM].withdrawal(balance))
            {
                accounts[iAccount].charging(balance);
                return false;
            }
            return true;
        }

        public bool charging(string ID, string cardNumber, double balance)
        {
            short iATM = findATM(ID);
            if (iATM == -1)
            {
                return false;
            }
            short iAccount = findAccount(cardNumber);
            if (iAccount == -1)
            {
                return false;
            }

            accounts[iAccount].charging(balance);
            automatedTellerMachines[iATM].charging(balance);
            return true;
        }
        public bool transfer(string cardNumber1, string PIN, string cardNumber2, double balance)
        {
            short iAccount1 = findAccount(cardNumber1);
            if (iAccount1 == -1)
            {
                return false;
            }
            short iAccount2 = findAccount(cardNumber2);
            if (iAccount2 == -1)
            {
                return false;
            }
            if (!accounts[iAccount1].authentication(PIN))
            {
                return false;
            }

            if (!accounts[iAccount1].withdrawal(balance))
            {
                return false;
            }
            accounts[iAccount2].charging(balance);
            return true;
        }
    }
}
