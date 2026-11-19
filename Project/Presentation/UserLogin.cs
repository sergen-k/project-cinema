using System.Security.Cryptography;
using System.Text;


static class UserLogin
{
    private static AccountsLogic accountsLogic = new AccountsLogic();

    public static void Start()
    {
        Console.CursorVisible = true;
        Console.WriteLine("Welcome to the login page");

        string email = AskForEmail();

        if (email == "0")
        {
            Console.CursorVisible = false;
            return;
        }

        int attempts = 0;

        while (attempts < 3)
        {
            string? password = AskForPassword();

            if (password == null)
            {
                Console.CursorVisible = false;
                return;
            }

            UserModel? acc = accountsLogic.CheckLogin(email, password);

            if (acc != null)
            {
                AccountsLogic.ChangeAccount(acc);

                Console.WriteLine("Welcome back " + acc.FirstName + " " + acc.LastName);
                Console.WriteLine("Login successful!");

                Console.CursorVisible = false;
                return;
            }

            attempts++;

            if (attempts < 3)
            {
                Console.WriteLine("Incorrect password.");
                Console.WriteLine("Attempts remaining: " + (3 - attempts));
            }
        }

        Console.WriteLine("Too many failed login attempts.");
        Console.WriteLine("Returning to the main menu...");

        Console.CursorVisible = false;
    }

    private static string AskForEmail()
    {
        while (true)
        {
            Console.WriteLine("Please enter your email address (0 to go back):");

            string? email = Console.ReadLine();

            if (email == "0")
            {
                return "0";
            }

            if (string.IsNullOrEmpty(email) || !accountsLogic.IsValidEmail(email))
            {
                Console.WriteLine("Please enter a valid email address (e.g. name@example.com).");
                continue;
            }

            return email;
        }
    }

    private static string? AskForPassword()
    {
        Console.WriteLine("Please enter your password (Esc to go back):");

        string password = "";

        while (true)
        {
            ConsoleKeyInfo key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.Escape)
            {
                Console.WriteLine();
                return null;
            }

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