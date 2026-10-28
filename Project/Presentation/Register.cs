using System;
using System.Security.Cryptography;
using System.Text;

public static class Register
{
    private static UserLogic _userLogic = new();

    public static void Initialize(UserLogic userLogic)
    {
        _userLogic = userLogic;
    }

    public static void Start()
    {
        string fullName = AskFullName();
        string email = AskEmail();
        string phone = AskPhoneNumber();
        string dateOfBirthInput = AskDateOfBirth();
        
        Console.WriteLine("Enter a password (min 12 characters, 1 uppercase, 1 number, 1 special character):");
        string password = AskPassword();
        AskPasswordConfirmation(password);

        DateTime dob = _userLogic.ParseDateOfBirth(dateOfBirthInput);

        string hashedPassword = HashPassword(password);

        long nextUserId = _userLogic.getNextId(); 

        UserModel newUser = new UserModel(nextUserId, fullName, email, phone, hashedPassword, 0, "Customer");
        newUser.HumanReadableDateOfBirth = dob;

        _userLogic.AddUser(newUser);

        Console.WriteLine("Registration successful!");
    }

    private static string AskFullName()
    {
        string fullName;
        do
        {
            Console.WriteLine("Enter your full name:");
            fullName = Console.ReadLine();
        } while (!_userLogic.IsValidFullName(fullName));

        return fullName;
    }

    private static string AskEmail()
    {
        string email;
        do
        {
            Console.WriteLine("Enter your email:");
            email = Console.ReadLine();
        } while (string.IsNullOrEmpty(email)
               || !_userLogic.IsValidEmail(email)
               || _userLogic.EmailAlreadyExists(email));

        return email;
    }

    private static string AskPhoneNumber()
    {
        string phone;
        do
        {
            Console.WriteLine("Enter your phone number:");
            phone = Console.ReadLine();
        } while (string.IsNullOrEmpty(phone) || !_userLogic.IsValidPhoneNumber(phone));

        return phone;
    }

    private static string AskDateOfBirth()
    {
        string dateOfBirthInput;
        do
        {
            Console.WriteLine("Enter your date of birth (dd-MM-yyyy):");
            dateOfBirthInput = Console.ReadLine();
        } while (string.IsNullOrEmpty(dateOfBirthInput) || !_userLogic.IsValidDateOfBirth(dateOfBirthInput));

        return dateOfBirthInput;
    }


    // Source - https://stackoverflow.com/a/17001289
    // Posted by Dmitrii Bychenko, modified by community. See post 'Timeline' for change history
    // Retrieved 2026-10-27, License - CC BY-SA 4.0

    public static string sha256_hash(String value)
    {
        StringBuilder Sb = new StringBuilder();

        using (SHA256 hash = SHA256Managed.Create())
        {
            Encoding enc = Encoding.UTF8;
            Byte[] result = hash.ComputeHash(enc.GetBytes(value));

            foreach (Byte b in result)
                Sb.Append(b.ToString("x2"));
        }

        return Sb.ToString();
    }

    private static string HashPassword(string password)
    {
        return sha256_hash(password);
    }

    private static string AskPassword()
    {
        string password = "";

        while (true)
        {
            ConsoleKeyInfo key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.Enter)
            {
                Console.WriteLine();

                if (string.IsNullOrEmpty(password) || !_userLogic.IsValidPassword(password))
                {
                    Console.WriteLine("Enter a password (min 12 characters, 1 uppercase, 1 number, 1 special character):");
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

    private static void AskPasswordConfirmation(string originalPassword)
    {
        string confirmation;
        do
        {
            Console.WriteLine("Confirm your password:");

            confirmation = AskPassword();
        } while (string.IsNullOrEmpty(confirmation) || confirmation != originalPassword);
    }
}