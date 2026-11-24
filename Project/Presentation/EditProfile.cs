using System;

public static class EditProfile
{
    private static UserLogic _userLogic = new();
    private static List<OptionBase> _options = new();

    public static void Start()
    {
        UserModel currentUser = UserLogic.CurrentAccount;

        while (true)
        {
            ConsoleMenu menu = new();
            _options.Clear();
            _options.Add(new InfoOption("Select to change your profile information!"));
            _options.Add(new SelectOption($"First Name: {currentUser.FirstName}"));
            _options.Add(new SelectOption($"Last Name: {currentUser.LastName}"));
            _options.Add(new SelectOption($"Email: {currentUser.Email}"));
            _options.Add(new SelectOption($"Phone Number: {currentUser.PhoneNumber}"));
            menu.SetOptions(_options);
            int choice = menu.SelectOption();

            if (choice == -1)
            {
                break;
            }
            switch (choice)
            {
                case 1:
                    string newFirstName = AskFirstName();
                    if (newFirstName != null)
                        currentUser.FirstName = newFirstName;
                    break;
                case 2:
                    string newLastName = AskLastName();
                    if (newLastName != null)
                        currentUser.LastName = newLastName;
                    break;
                case 3:
                    string newEmail = AskEmail();
                    if (newEmail != null)
                        currentUser.Email = newEmail;
                    break;
                case 4:
                    string newPhoneNumber = AskPhoneNumber();
                    if (newPhoneNumber != null)
                        currentUser.PhoneNumber = newPhoneNumber;
                    break;
            }
            _userLogic.UpdateUser(currentUser);
        }
        Console.WriteLine("Profile updated successfully!");
    }

    private static string AskFirstName()
    {
        string errorMessage = "";
        string input;
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Enter your new first name");
            Console.WriteLine("- Must be at least 2 characters long");
            Console.WriteLine("- Enter \"Q\" to quit editing");
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
            Console.WriteLine("Enter your new last name");
            Console.WriteLine("- Must be at least 2 characters long");
            Console.WriteLine("- Enter \"Q\" to quit editing");
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
            Console.WriteLine("Enter your new email");
            Console.WriteLine("- Needs to be a valid email");
            Console.WriteLine("- Has to be unused");
            Console.WriteLine("- Enter \"Q\" to quit editing");
            Console.Write(errorMessage);
            input = Console.ReadLine().ToLower();
            if (!string.IsNullOrWhiteSpace(input)
               && _userLogic.IsValidEmail(input)
               && !_userLogic.EmailAlreadyExists(input))
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

    private static string AskPhoneNumber()
    {
        string errorMessage = "";
        string input;
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Enter your new phone number");
            Console.WriteLine("- Has to be a valid phone number");
            Console.WriteLine("- Optionally starting with +");
            Console.WriteLine("- Enter \"Q\" to quit editing");
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
}