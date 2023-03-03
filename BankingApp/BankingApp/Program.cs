// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");

public class Bank
{
    List<BankAccount> AccountsList = new List<BankAccount>();

    public static void Main()
    {
        Bank newBank = new Bank();
        Console.WriteLine("Welcome to Basic Banking Bank!");
        Console.WriteLine("______________________________");
        Console.WriteLine("");
        newBank.Options();

    }
    public void Options()
    {
        Console.WriteLine("You have the following options:");
        Console.WriteLine("1. Add an account");
        Console.WriteLine("2. View an account");
        Console.WriteLine("3. Deposit funds");
        Console.WriteLine("4. Withdraw funds");
        Console.WriteLine("5. View all accounts and balances");
        Console.WriteLine("6. Exit");
        Console.WriteLine("");
        Console.WriteLine("Please type the number of the option you wish to select:");

        int option = Convert.ToInt32(Console.ReadLine());
        if (option == 1)
        {
            AddAccount();
        }
        else if (option == 2)
        {
            ViewAccount();
        }
        else if (option == 3)
        {
            Deposit();
        }
        else if (option == 4)
        {
            Withdraw();
        }
        else if (option == 5)
        {
            ViewAll();
        }
        else if (option == 6)
        {
            Console.WriteLine("Thank you and good bye!");

        }
        else
        {
            Console.WriteLine("You did not enter a number between 1 and 6. Press enter to continue.");
            Console.ReadLine();
            Console.Clear();
            Options();
        }
    }

    public void AddAccount()
    {
        Console.Clear();
        Console.WriteLine("Please enter your new account number:");
        string accountNumber = Console.ReadLine();
        foreach (var item in AccountsList)
        {
            if (accountNumber == item.accountNumber)
            {
                Console.WriteLine("This account number already exists. Please choose another one. Press enter to retry.");
                Console.ReadLine();
                AddAccount();
            }
        }
        Console.WriteLine("Please enter your opening balance:");
        double accountBalance = Convert.ToDouble(Console.ReadLine());

        BankAccount newAccount = new BankAccount(accountNumber, accountBalance);
        AccountsList.Add(newAccount);

        Console.WriteLine($"\nThank you, your input is shown below:\nAccount Number: {newAccount.accountNumber}\nAccount Balance: {newAccount.accountBalance}\n\n");
        Console.WriteLine("Press enter to return to the menu");
        Console.ReadLine();
        Console.Clear();
        Options();
    }

    public void ViewAccount()
    {
        Console.Clear();
        Console.WriteLine("Please enter an account number:");
        string accountNumberSearch = Console.ReadLine();

        var bankAcount = AccountsList.FirstOrDefault(_ => _.accountNumber == accountNumberSearch);

        foreach (var item in AccountsList)
        {
            if (accountNumberSearch == item.accountNumber)
            {
                Console.WriteLine("Account Number: " + item.accountNumber);
                Console.WriteLine("Account Balance: " + item.accountBalance);
                foreach (var transaction in item.TransactionList)
                {
                    Console.WriteLine(transaction);
                }
                Console.WriteLine("");
                Console.WriteLine("Press enter to return to the menu");
                Console.ReadLine();
                Console.Clear();
                Options();
            }
        }
    }

    public void Deposit()
    {
        Console.Clear();
        Console.WriteLine("Please enter your account number:");
        string accountNumberSearch = Console.ReadLine();
        Console.Clear();
        foreach (var item in AccountsList)
        {
            if (accountNumberSearch == item.accountNumber)
            {
                Console.WriteLine("Account " + item.accountNumber);
                Console.WriteLine("Please enter the amount you want to deposit:");
                double deposit = Convert.ToDouble(Console.ReadLine());
                Console.Clear();
                item.accountBalance += deposit;
                item.TransactionList.Add(new Transaction(deposit, DateTime.Now, TransactionType.Deposit));
                Console.WriteLine("Account " + item.accountNumber);
                Console.WriteLine("New balance is: " + item.accountBalance);
                Console.WriteLine("Press enter to return to the menu");
                Console.ReadLine();
                Console.Clear();
                Options();
            }
            //Error when the program has looped through all accounts without a match. When a count reaches number of objects in list.
        }

    }

    public void Withdraw()
    {
        Console.Clear();
        Console.WriteLine("Please enter your account number:");
        string accountNumberSearch = Console.ReadLine();
        Console.Clear();
        foreach (var item in AccountsList)
        {
            if (accountNumberSearch == item.accountNumber)
            {
                Console.WriteLine("Account " + item.accountNumber + ", your current balance is " + item.accountBalance + ". How much would you like to withdraw?");
                double withdraw = Convert.ToDouble(Console.ReadLine());
                if (withdraw > item.accountBalance)
                {
                    Console.WriteLine("You cannot withdraw more than your balance. Press enter to retry");
                    Console.ReadLine();
                    Console.Clear();
                    Withdraw();
                }
                item.accountBalance -= withdraw;
                item.TransactionList.Add(new Transaction(withdraw, DateTime.Now, TransactionType.Withdrawal));
                Console.WriteLine("Account " + item.accountNumber);
                Console.WriteLine("New balance is: " + item.accountBalance);
                Console.WriteLine("Press enter to return to the menu");
                Console.ReadLine();
                Console.Clear();
                Options();
            }
            //Error when the program has looped through all accounts without a match. When a count reaches number of objects in list.
        }
    }

    public void ViewAll()
    {
        foreach (var item in AccountsList)
        {
            Console.WriteLine("Account Number: " + item.accountNumber);
            Console.WriteLine("Account Balance: " + item.accountBalance);
            Console.WriteLine("");
        }
        Console.WriteLine("Press enter to return to the menu");
        Console.ReadLine();
        Console.Clear();
        Options();
    }

}

class BankAccount
{
    public string accountNumber;
    public double accountBalance;
    public List<Transaction> TransactionList;

    public BankAccount(string accountNumber, double accountBalance)
    {
        this.accountNumber = accountNumber;
        this.accountBalance = accountBalance;
        this.TransactionList = new List<Transaction>();
    }

    public string getAccountNumber
    {
        get { return accountNumber; }
    }
    public double getBalance
    {
        get { return accountBalance; }
    }
}

public enum TransactionType
{
    Deposit, 
    Withdrawal
}
class Transaction
{
    public double amount;
    public DateTime date;
    public TransactionType TransactionType;

    public Transaction(double amount, DateTime date, TransactionType transactionType)
    {
        this.amount = amount;
        this.date = date;
        this.TransactionType = transactionType;
    }

    public override string ToString()
    {
        return $"{TransactionType} - {date} - {amount}";
    }
}

//Console.ReadKey();