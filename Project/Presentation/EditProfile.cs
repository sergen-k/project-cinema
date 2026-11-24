using System;

public static class EditProfile
{
    private static UserLogic _userLogic = new();

    public static void Start()
    {
        UserModel currentUser = UserLogic.CurrentAccount;

        while (true)
        {
            ConsoleMenu menu = new();
            menu.AddOption(new InfoOption("Select to change your profile information!"));
            menu.AddOption(new SelectOption<string>($"First Name: {currentUser.FirstName}", "First Name"));
            menu.AddOption(new SelectOption<string>($"Last Name: {currentUser.LastName}", "Last Name"));
            menu.AddOption(new SelectOption<string>($"Email: {currentUser.Email}", "Email"));
            menu.AddOption(new SelectOption<string>($"Phone Number: {currentUser.PhoneNumber}", "Phone Number"));
            string choice = menu.SelectOption();

            if (choice == null)
            {
                break;
            }
            switch (choice)
            {
                case "First Name":
                    string newFirstName = AskFirstName();
                    if (newFirstName != null)
                        currentUser.FirstName = newFirstName;
                    break;
                case "Last Name":
                    string newLastName = AskLastName();
                    if (newLastName != null)
                        currentUser.LastName = newLastName;
                    break;
                case "Email":
                    string newEmail = AskEmail(currentUser.Id);
                    if (newEmail != null)
                        currentUser.Email = newEmail;
                    break;
                case "Phone Number":
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

    private static string AskEmail(long currentUserId)
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
               && !_userLogic.EmailAlreadyExistsForDifferentUser(input, currentUserId))
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