﻿namespace ATMClassLibrary
{
    public class BankFindAccountEventArgs : EventArgs
    {
        public string result { get; private set; }
        public BankFindAccountEventArgs(string result)
        {
            this.result = result;
        }
    }
}
