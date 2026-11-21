using System.Security.Cryptography;
using System.Text;


static class UserLogin
{
    private static UserLogic _userLogic = new();

    public static void Start()
    {

        int attempts = 0;

        

        string email = AskForEmail();
        if (email == null) {return;}
        Console.Clear();
        while (attempts < 3)
        {
            string password = AskForPassword();
            if (password == null) {return;}

            UserModel? acc = _userLogic.CheckLogin(email, password);

            if (acc != null)
            {
                UserLogic.ChangeAccount(acc);

                Console.WriteLine("Welcome back " + acc.FirstName + " " + acc.LastName);
                Console.WriteLine("Login successful!");

                return;
            }
            else
            {
                attempts++;


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
        Console.CursorVisible = false;
    }

    private static string AskForEmail()
    {
        Console.Clear();
        Console.WriteLine("Please enter your email address, enter 'Q' to return to menu:");
        while (true)
        {
            string? email = Console.ReadLine().ToLower();
            if (email == "q")
            {
                return null;
            }
            else if (string.IsNullOrEmpty(email))
            {
                Console.Clear();
                Console.WriteLine("Email cannot be empty. Please try again:");
            }
            else if (!_userLogic.EmailAlreadyExists(email))
            {
                Console.Clear();
                Console.WriteLine("No account with this email exists. Please try again:");
            }
            else
            {
                return email;
            }
        }
    }

    private static string AskForPassword()
    {
        Console.WriteLine("Please enter your password, press Escape to return to menu:");

        string password = "";

        while (true)
        {
            ConsoleKeyInfo key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.Enter)
            {
                Console.WriteLine();

                if (string.IsNullOrEmpty(password))
                {
                    Console.Clear();
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
            else if (key.Key == ConsoleKey.Escape)
            {
                return null;
            }
            else if (!char.IsControl(key.KeyChar))
            {
                password += key.KeyChar;
                Console.Write("*");
            }
        }
    }
}