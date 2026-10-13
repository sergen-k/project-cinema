static class UserLogin
{
    private static AccountsLogic accountsLogic = new AccountsLogic();

    public static void Start()
    {
        Console.WriteLine("Welcome to the login page");

        string email = AskForEmail();
        string password = AskForPassword();

        UserModel? acc = accountsLogic.CheckLogin(email, password);

        if (acc != null)
        {
            Console.WriteLine("Welcome back " + acc.FullName);
            Console.WriteLine("Login successful!");

            Menu.Start();
        }
        else
        {
            Console.WriteLine("Invalid email or password.");
        }
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

        string? password = Console.ReadLine();

        while (string.IsNullOrEmpty(password))
        {
            Console.WriteLine("Password cannot be empty. Please try again:");
            password = Console.ReadLine();
        }

        return password;
    }
}