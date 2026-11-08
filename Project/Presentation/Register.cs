using System;
using System.Security.Cryptography;
using System.Text;

public static class Register
{
    private static AccountsLogic _userLogic = new();

    public static void Initialize(AccountsLogic userLogic)
    {
        _userLogic = userLogic;
    }

    public static void Start()
    {
        string firstName = AskFirstName();
        string lastName = AskLastName();
        string email = AskEmail();
        string phone = AskPhoneNumber();
        string dateOfBirthInput = AskDateOfBirth();

        Console.WriteLine("Enter a password (min 12 characters, 1 uppercase, 1 number, 1 special character):");
        string password = AskPassword();
        AskPasswordConfirmation(password);

        DateTime dob = _userLogic.ParseDateOfBirth(dateOfBirthInput);
        string hashedPassword = HashPassword(password);

        if (!_userLogic.EmailAlreadyExists(email))
        {
            UserModel newUser = new UserModel(0, firstName, lastName, email, phone, hashedPassword, 0, "Customer");
            newUser.HumanReadableDateOfBirth = dob;

            _userLogic.AddUser(newUser);
        }

        Console.WriteLine("If this email address is not already registered, your account has been created.");
    }
    private static string AskFirstName()
    {
        string firstName;
        bool firstAttempt = true;
        do
        {
            if (!firstAttempt)
            {
                Console.WriteLine("First name must be at least 2 characters long.");
            }
            Console.WriteLine("Enter your first name:");
            firstName = Console.ReadLine();
            firstAttempt = false;
        } while (!_userLogic.IsValidFirstName(firstName));

        return firstName;
    }

    private static string AskLastName()
    {
        string lastName;
        bool firstAttempt = true;
        do
        {
            if (!firstAttempt)
            {
                Console.WriteLine("Last name must be at least 2 characters long.");
            }
            Console.WriteLine("Enter your last name:");
            lastName = Console.ReadLine();
            firstAttempt = false;
        } while (!_userLogic.IsValidLastName(lastName));

        return lastName;
    }
    private static string AskEmail()
    {
        string email;
        bool firstAttempt = true;
        do
        {
            if (!firstAttempt)
            {
                Console.WriteLine("Please enter a valid email address (e.g. name@example.com).");
            }
            Console.WriteLine("Enter your email:");
            email = Console.ReadLine();
            firstAttempt = false;
        } while (string.IsNullOrEmpty(email) || !_userLogic.IsValidEmail(email));

        return email;
    }

    private static string AskPhoneNumber()
    {
        string phone;
        bool firstAttempt = true;
        do
        {
            if (!firstAttempt)
            {
                Console.WriteLine("Please enter a valid phone number (10-15 digits, optionally starting with +).");
            }
            Console.WriteLine("Enter your phone number:");
            phone = Console.ReadLine();
            firstAttempt = false;
        } while (string.IsNullOrEmpty(phone) || !_userLogic.IsValidPhoneNumber(phone));

        return phone;
    }

    private static string AskDateOfBirth()
    {
        string dateOfBirthInput;
        bool firstAttempt = true;
        do
        {
            if (!firstAttempt)
            {
                Console.WriteLine("Please enter a valid date of birth in the format dd-MM-yyyy. It must be in the past and no more than 150 years ago.");
            }
            Console.WriteLine("Enter your date of birth (dd-MM-yyyy):");
            dateOfBirthInput = Console.ReadLine();
            firstAttempt = false;
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
    private static string ReadMaskedInput()
    {
        string input = "";

        while (true)
        {
            ConsoleKeyInfo key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.Enter)
            {
                Console.WriteLine();
                return input;
            }
            else if (key.Key == ConsoleKey.Backspace)
            {
                if (input.Length > 0)
                {
                    input = input.Substring(0, input.Length - 1);
                    Console.Write("\b \b");
                }
            }
            else if (!char.IsControl(key.KeyChar))
            {
                input += key.KeyChar;
                Console.Write("*");
            }
        }
    }
    private static string AskPassword()
    {
        string password = "";
        bool showError = false;

        while (true)
        {
            if (showError)
            {
                Console.WriteLine("Password must be at least 12 characters and include an uppercase letter, a number, and a special character.");
                Console.WriteLine("Enter a password:");
                showError = false;
            }

            ConsoleKeyInfo key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.Enter)
            {
                Console.WriteLine();

                if (string.IsNullOrEmpty(password) || !_userLogic.IsValidPassword(password))
                {
                    password = "";
                    showError = true;
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
            confirmation = ReadMaskedInput();

            if (confirmation != originalPassword)
            {
                Console.WriteLine("Passwords do not match. Please try again.");
            }
        } while (confirmation != originalPassword);
    }
}