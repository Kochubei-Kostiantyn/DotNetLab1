using ATMClassLibrary;

namespace WinFormsATM
{
    public partial class Form1 : Form
    {
        public string ATM_ID;
        public string cardNumber;
        public string PIN;
        public string cardNumber2;
        public short balance;
        public Bank bank;
        public Form1()
        {
            InitializeComponent();
        }
        public void load()
        {
            ATM_ID = bank.automatedTellerMachines[comboBox1.SelectedIndex].ID;
            cardNumber = textBox1.Text;
            PIN = textBox2.Text;
            cardNumber2 = textBox3.Text;
            balance = short.Parse(textBox4.Text);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            void AccountAuthenticationHandler(object sender, AccountAuthenticationEventArgs e)
            {
                MessageBox.Show(e.result);
            }
            void AccountWithdrawalHandler(object sender, AccountWithdrawalEventArgs e)
            {
                MessageBox.Show(e.result);
            }
            void AccountGetBalanceHandler(object sender, AccountGetBalanceEventArgs e)
            {
                MessageBox.Show(e.balance);
            }
            void ATMWithdrawalHandler(object sender, AutomatedTellerMachineWithdrawalEventArgs e)
            {
                MessageBox.Show(e.result);
            }
            void BankFindAccountHandler(object sender, BankFindAccountEventArgs e)
            {
                MessageBox.Show(e.result);
            }
            void BankFindATMHandler(object sender, BankFindATMEventArgs e)
            {
                MessageBox.Show(e.result);
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

            bank = new Bank("ProstoBank", accounts, automatedTellerMachines);

            bank.FindAccountEvent += BankFindAccountHandler;
            bank.FindATMEvent += BankFindATMHandler;

            for (int i = 0; i < bank.automatedTellerMachines.Count; i++)
            {
                comboBox1.Items.Add(automatedTellerMachines[i].address + "(" + automatedTellerMachines[i].ID + ")");
            }

            comboBox1.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            load();
            bank.accounts[bank.findAccount(cardNumber)].getBalance();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            load();
            bank.withdrawal(ATM_ID, cardNumber, PIN, balance);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            load();
            bank.charging(ATM_ID, cardNumber, balance);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            load();
            bank.transfer(cardNumber, PIN, cardNumber2, balance);
        }
    }
}