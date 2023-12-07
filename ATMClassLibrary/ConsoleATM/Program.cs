using ATMClassLibrary;
using System.Text;

Console.OutputEncoding = Encoding.UTF8;

void AccountAuthenticationHandler(object sender, AccountAuthenticationEventArgs e)
{
    Console.WriteLine(e.result);
}
void AccountWithdrawalHandler(object sender, AccountWithdrawalEventArgs e)
{
    Console.WriteLine(e.result);
}
void AccountGetBalanceHandler(object sender, AccountGetBalanceEventArgs e)
{
    Console.WriteLine(e.balance);
}
void ATMWithdrawalHandler(object sender, AutomatedTellerMachineWithdrawalEventArgs e)
{
    Console.WriteLine(e.result);
}
void BankFindAccountHandler(object sender, BankFindAccountEventArgs e)
{
    Console.WriteLine(e.result);
}
void BankFindATMHandler(object sender, BankFindATMEventArgs e)
{
    Console.WriteLine(e.result);
}

List<Account> accounts = new List<Account>();
accounts.Add(new Account("5168123412340000", "Andrew", "Andreev", 3000, "1289"));
accounts.Add(new Account("5168432143219999", "Anna", "Andeeva", 1000, "0038"));

accounts[0].AuthenticationEvent += AccountAuthenticationHandler;
accounts[0].WithdrawalEvent += AccountWithdrawalHandler;
accounts[0].GetBalanceEvent += AccountGetBalanceHandler;
accounts[1].AuthenticationEvent += AccountAuthenticationHandler;
accounts[1].WithdrawalEvent += AccountWithdrawalHandler;
accounts[1].GetBalanceEvent += AccountGetBalanceHandler;

List<AutomatedTellerMachine> automatedTellerMachines = new List<AutomatedTellerMachine>();
automatedTellerMachines.Add(new AutomatedTellerMachine(100000, "01", "Kyivska, 63"));
automatedTellerMachines.Add(new AutomatedTellerMachine(10, "02", "Berdychivska, 18"));

automatedTellerMachines[0].WithdrawalEvent += ATMWithdrawalHandler;
automatedTellerMachines[1].WithdrawalEvent += ATMWithdrawalHandler;

Bank bank = new Bank("ProstoBank", accounts, automatedTellerMachines);

bank.FindAccountEvent += BankFindAccountHandler;
bank.FindATMEvent += BankFindATMHandler;

// -----------------------------------------

while (true)
{
    Console.WriteLine("Оберіть банкомат");
    for (short i = 0; i < bank.automatedTellerMachines.Count; i++)
        Console.WriteLine((i + 1).ToString() + ". " + automatedTellerMachines[i].address + "(" + automatedTellerMachines[i].ID + ").");
    string ATM_ID = bank.automatedTellerMachines[short.Parse(Console.ReadLine()) - 1].ID;

    while (true)
    {
        Console.WriteLine("Оберіть операцію");
        Console.WriteLine("1. Перегляд балансу карти.");
        Console.WriteLine("2. Зняття коштів.");
        Console.WriteLine("3. Зарахування коштів на картку.");
        Console.WriteLine("4. Перерахування коштів з картки на картку.");
        Console.WriteLine("5. Назад.");
        short read = short.Parse(Console.ReadLine());

        string cardNumber;
        string cardNumber2;
        string PIN;
        short balance;
        bool end = false;
        switch (read)
        {
            
            case 1:
                Console.WriteLine("Введіть номер картки:");
                bank.accounts[bank.findAccount(Console.ReadLine())].getBalance();
                break;

            case 2:
                Console.WriteLine("Введіть номер картки:");
                cardNumber = Console.ReadLine();
                Console.WriteLine("Введіть пін-код картки:");
                PIN = Console.ReadLine();
                Console.WriteLine("Введіть суму, яку хочете зняти:");
                balance = short.Parse(Console.ReadLine());
                bank.withdrawal(ATM_ID, cardNumber, PIN, balance);
                break;

            case 3:
                Console.WriteLine("Введіть номер картки:");
                cardNumber = Console.ReadLine();
                Console.WriteLine("Введіть суму, яку хочете зарахувати:");
                balance = short.Parse(Console.ReadLine());
                bank.charging(ATM_ID, cardNumber, balance);
                break;

            case 4:
                Console.WriteLine("Введіть номер вашої картки:");
                cardNumber = Console.ReadLine();
                Console.WriteLine("Введіть пін-код вашої картки:");
                PIN = Console.ReadLine();
                Console.WriteLine("Введіть номер картки, на яку хочете переразувати кошти:");
                cardNumber2 = Console.ReadLine();
                Console.WriteLine("Введіть суму, яку хочете зарахувати:");
                balance = short.Parse(Console.ReadLine());
                bank.transfer(cardNumber, PIN, cardNumber2, balance);
                break;

            case 5:
                end = true;
                break;
        }
        if (end)
            break;
    }

}
