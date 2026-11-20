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
        Console.CursorVisible = true;
        string firstName = AskFirstName();
        if (firstName == null) {return;}
        string lastName = AskLastName();
        if (lastName == null) {return;}
        string email = AskEmail();
        if (email == null) {return;}
        string phone = AskPhoneNumber();
        if (phone == null) {return;}
        string dateOfBirthInput = AskDateOfBirth();
        if (dateOfBirthInput == null) {return;}

        string passwordMessage = """
                Password must be at least 12 characters and include an uppercase letter, a number, and a special character.
                If you want to escape account creation, press the Escape Key
                Enter a password:
                """;
        string password = AskPassword(passwordMessage, $"Invalid password, please try again \n{passwordMessage}");

        if (password == null) {return;}
        if (!AskPasswordConfirmation(password)) {return;}

        DateTime dob = _userLogic.ParseDateOfBirth(dateOfBirthInput);
        string hashedPassword = HashPassword(password);

        UserModel newUser = new UserModel(0, firstName, lastName, email, phone, hashedPassword, 0, "Customer");
        newUser.HumanReadableDateOfBirth = dob;
        _userLogic.AddUser(newUser);
        Console.Clear();
        Console.WriteLine("Your account has successfully been created.");
        Console.WriteLine("Press any key to continue.");
        Console.ReadKey();
        Console.CursorVisible = false;
    }

    private static string AskFirstName()
    {
        string errorMessage = "";
        string input;
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Enter your first name");
            Console.WriteLine("- Must be at least 2 characters long");
            Console.WriteLine("- Enter \"Q\" to quit account creation");
            Console.Write(errorMessage);
            input = Console.ReadLine();
            if (_userLogic.IsValidFirstName(input))
            {
                return input;
            }
            else if (input.ToLower() == "q")
            {
                return null;
            }
            else
            {
                errorMessage = "ERROR: Input was invalid!\n";
            }
        }
    }

    private static string AskLastName()
    {
        string errorMessage = "";
        string input;
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Enter your last name");
            Console.WriteLine("- Must be at least 2 characters long");
            Console.WriteLine("- Enter \"Q\" to quit account creation");
            Console.Write(errorMessage);
            input = Console.ReadLine();
            if (_userLogic.IsValidLastName(input))
            {
                return input;
            }
            else if (input.ToLower() == "q")
            {
                return null;
            }
            else
            {
                errorMessage = "ERROR: Input was invalid!\n";
            }
        }
    }

    private static string AskEmail()
    {
        string errorMessage = "";
        string input;
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Enter your email");
            Console.WriteLine("- Needs to be a valid email");
            Console.WriteLine("- Has to be unused");
            Console.WriteLine("- Enter \"Q\" to quit account creation");
            Console.Write(errorMessage);
            input = Console.ReadLine().ToLower();
            if (_userLogic.EmailAlreadyExists(input))
            {
                errorMessage = "ERROR: Email already exists!\n";
            }
            else if (!string.IsNullOrEmpty(input)
               && _userLogic.IsValidEmail(input))
            {
                return input;
            }
            else if (input == "q")
            {
                return null;
            }
            else
            {
                errorMessage = "ERROR: Input was invalid!\n";
            }
        }
    }

    private static string AskPhoneNumber()
    {
        string errorMessage = "";
        string input;
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Enter your phone number");
            Console.WriteLine("- Has to be a valid phone number");
            Console.WriteLine("- Optionally starting with +");
            Console.WriteLine("- Enter \"Q\" to quit account creation");
            Console.Write(errorMessage);
            input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input) && _userLogic.IsValidPhoneNumber(input))
            {
                return input;
            }
            else if (input.ToLower() == "q")
            {
                return null;
            }
            else
            {
                errorMessage = "ERROR: Input was invalid!\n";
            }
        }
    }    
    
    private static string AskDateOfBirth()
    {
        string errorMessage = "";
        string input;
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Enter your date of birth");
            Console.WriteLine("- In the format dd-MM-yyyy");
            Console.WriteLine("- Has to be in the range of 0-150 years");
            Console.WriteLine("- Enter \"Q\" to quit account creation");
            Console.Write(errorMessage);
            input = Console.ReadLine();
            if (!string.IsNullOrEmpty(input) && _userLogic.IsValidDateOfBirth(input))
            {
                return input;
            }
            else if (input.ToLower() == "q")
            {
                return null;
            }
            else
            {
                errorMessage = "ERROR: Input was invalid!\n";
            }
        }
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

    private static string AskPassword(string criteria, string error)
    {
        string password = "";
        bool resetPass = true;
        bool hasErrored = false;
        while (true)
        {
            if (resetPass)
            {
                Console.Clear();
                if (hasErrored)
                {
                    Console.WriteLine(error);
                }
                else
                {
                    Console.WriteLine(criteria);
                    hasErrored = true;
                }
                resetPass = false;
            }

            ConsoleKeyInfo key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.Enter)
            {
                Console.WriteLine();

                if (string.IsNullOrEmpty(password) || !_userLogic.IsValidPassword(password))
                {
                    password = "";
                    resetPass = true;
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

    private static bool AskPasswordConfirmation(string originalPassword)
    {
        while (true)
        {
            string confirmation = AskPassword("Confirm your password. Press escape to quit account creation.", 
                                              "Passwords do not match, try again. Press escape to quit account creation.");
            if (confirmation == null)
            {
                return false;
            } 
            else if (confirmation == originalPassword)
            {
                return true;
            }
        }
    }
}