using System.Security.Cryptography;
using System.Text;


static class UserLogin
{
    private static AccountsLogic accountsLogic = new AccountsLogic();

    public static void Start()
    {
        Console.WriteLine("Welcome to the login page");

        int attempts = 0;

        while (attempts < 3)
        {
            string email = AskForEmail();
            string password = AskForPassword();

            UserModel? acc = accountsLogic.CheckLogin(email, password);

            if (acc != null)
            {
                AccountsLogic.ChangeAccount(acc);

                Console.WriteLine("Welcome back " + acc.FullName);
                Console.WriteLine("Login successful!");

                return;
            }
            else
            {
                attempts++;

                Console.WriteLine("Invalid email or password.");

                if (attempts < 3)
                {
                    Console.WriteLine("Attempts remaining: " + (3 - attempts));
                }
            }
        }

        Console.WriteLine("Too many failed login attempts.");
        Console.WriteLine("You cannot try again in this login session.");
        Console.WriteLine("Press any key to return to the main menu...");

        Console.ReadKey(true);
    }

    private static string AskForEmail()
    {
        Console.WriteLine("Please enter your email address:");

        string? email = Console.ReadLine();

        while (string.IsNullOrEmpty(email))
        {
            Console.WriteLine("Email cannot be empty. Please try again:");
            email = Console.ReadLine();
        }

        return email;
    }

    private static string AskForPassword()
    {
        Console.WriteLine("Please enter your password:");

        string password = "";

        while (true)
        {
            ConsoleKeyInfo key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.Enter)
            {
                Console.WriteLine();

                if (string.IsNullOrEmpty(password))
                {
                    Console.WriteLine("Password cannot be empty. Please try again:");
                }
                else
                {
                    return password;
                }
            }
            else if (key.Key == ConsoleKey.Backspace)
            {
                if (password.Length > 0)
                {
                    password = password.Substring(0, password.Length - 1);
                    Console.Write("\b \b");
                }
            }
            else if (!char.IsControl(key.KeyChar))
            {
                password += key.KeyChar;
                Console.Write("*");
            }
        }
    }
}