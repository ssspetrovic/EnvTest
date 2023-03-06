namespace OOPIntro
{
    internal class BankAccount
    {
        public string Number { get; }
        public string Owner { get; }

        public decimal Balance
        {
            get { return _allTransactions.Sum(transaction => transaction.Amount); }
        }

        private static int _accountNumberSeed = 12345678;
        private readonly List<Transaction> _allTransactions = new();

        public BankAccount(string name, decimal initialBalance)
        {
            Number = _accountNumberSeed++.ToString();
            Owner = name;
            MakeDeposit(initialBalance, DateTime.Now, "Initial Balance");
        }

        public void MakeDeposit(decimal amount, DateTime date, string note)
        {
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Amount of deposit must be positive");
            }

            var deposit = new Transaction(amount, date, note);
            _allTransactions.Add(deposit);
        }

        public void MakeWithdrawal(decimal amount, DateTime date, string note)
        {
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Amount of deposit must be positive");
            }

            if (Balance - amount < 0)
            {
                throw new InvalidOperationException("Not sufficient funds for this withdrawal");
            }

            var withdrawal = new Transaction(-amount, date, note);
            _allTransactions.Add(withdrawal);
        }

        public string GetAccountHistory()
        {
            var reportBuilder = new System.Text.StringBuilder();
            decimal balance = 0;
            reportBuilder.AppendLine("Date\t\tAmount\tBalance\tNote");

            foreach (var transaction in _allTransactions)
            {
                balance += transaction.Amount;
                reportBuilder.AppendLine(
                    $"{transaction.Date.ToShortDateString()}\t{transaction.Amount}\t{balance}\t{transaction.Note}");
            }

            return reportBuilder.ToString();
        }
    }
}
