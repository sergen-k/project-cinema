using System;

public static class EditProfile
{
    private static AccountsLogic _userLogic = new();

    public static void Start()
    {
        UserModel currentUser = AccountsLogic.CurrentAccount;

        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("Current profile information:");
            Console.WriteLine($"1. First name: {currentUser.FirstName}");
            Console.WriteLine($"2. Last name: {currentUser.LastName}");
            Console.WriteLine($"3. Email: {currentUser.Email}");
            Console.WriteLine($"4. Phone number: {currentUser.PhoneNumber}");
            Console.WriteLine("Type 1-4 to edit a field, or type \"Q\" to save and exit.");

            string choice = Console.ReadLine()?.Trim().ToUpper();

            if (choice == "Q")
            {
                break;
            }

            switch (choice)
            {
                case "1":
                    currentUser.FirstName = AskFirstName();
                    break;
                case "2":
                    currentUser.LastName = AskLastName();
                    break;
                case "3":
                    currentUser.Email = AskEmail(currentUser.Id);
                    break;
                case "4":
                    currentUser.PhoneNumber = AskPhoneNumber();
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please type 1, 2, 3, 4, or \"Q\".");
                    break;
            }
        }
        
        _userLogic.UpdateUser(currentUser);
        Console.WriteLine("Profile updated successfully!");
    }


    private static string AskFirstName()
    {
        string input;
        bool firstAttempt = true;
        do
        {
            if (!firstAttempt)
            {
                Console.WriteLine("First name must be at least 2 characters long.");
            }
            Console.WriteLine("Enter your new first name:");
            input = Console.ReadLine();
            firstAttempt = false;
        } while (!_userLogic.IsValidFirstName(input));

        return input;
    }

    private static string AskLastName()
    {
        string input;
        bool firstAttempt = true;
        do
        {
            if (!firstAttempt)
            {
                Console.WriteLine("Last name must be at least 2 characters long.");
            }
            Console.WriteLine("Enter your new last name:");
            input = Console.ReadLine();
            firstAttempt = false;
        } while (!_userLogic.IsValidLastName(input));

        return input;
    }

    private static string AskEmail(long currentUserId)
    {
        string input;
        bool firstAttempt = true;
        do
        {
            if (!firstAttempt)
            {
                Console.WriteLine("Please enter a valid, unused email address.");
            }
            Console.WriteLine("Enter your new email:");
            input = Console.ReadLine();
            firstAttempt = false;
        } while (string.IsNullOrWhiteSpace(input)
               || !_userLogic.IsValidEmail(input)
               || _userLogic.EmailAlreadyExistsForDifferentUser(input, currentUserId));

        return input;
    }

    private static string AskPhoneNumber()
    {
        string input;
        bool firstAttempt = true;
        do
        {
            if (!firstAttempt)
            {
                Console.WriteLine("Please enter a valid phone number (10-15 digits, optionally starting with +).");
            }
            Console.WriteLine("Enter your new phone number:");
            input = Console.ReadLine();
            firstAttempt = false;
        } while (string.IsNullOrWhiteSpace(input) || !_userLogic.IsValidPhoneNumber(input));

        return input;
    }
}