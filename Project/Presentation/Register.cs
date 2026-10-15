using System;

public static class Register
{
    private static UserLogic _userLogic;

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
        string password = AskPassword();
        AskPasswordConfirmation(password);

        DateTime dob = _userLogic.ParseDateOfBirth(dateOfBirthInput);

        UserModel newUser = new UserModel(0, fullName, email, phone, password, 0, "Customer");
        newUser.HumanReadableDateOfBirth = dob;

        _userLogic.AddUser(newUser);

        Console.WriteLine("Registration successful!");
        Menu.Start();
    }

    private static string AskFullName()
    {
        string fullName;
        do
        {
            Console.WriteLine("Enter your full name:");
            fullName = Console.ReadLine();
        } while (string.IsNullOrEmpty(fullName));

        return fullName;
    }

    private static string AskEmail()
    {
        string email;
        do
        {
            Console.WriteLine("Enter your email:");
            email = Console.ReadLine();
        } while (string.IsNullOrEmpty(email) || !_userLogic.IsValidEmail(email));

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

    private static string AskPassword()
    {
        string password;
        do
        {
            Console.WriteLine("Enter a password (min 12 characters, 1 uppercase, 1 number, 1 special character):");
            password = Console.ReadLine();
        } while (string.IsNullOrEmpty(password) || !_userLogic.IsValidPassword(password));

        return password;
    }

    private static void AskPasswordConfirmation(string originalPassword)
    {
        string confirmation;
        do
        {
            Console.WriteLine("Confirm your password:");
            confirmation = Console.ReadLine();
        } while (string.IsNullOrEmpty(confirmation) || confirmation != originalPassword);
    }
}