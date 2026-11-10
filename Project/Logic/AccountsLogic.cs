using System.Security.Cryptography;
using System.Text;
using Microsoft.Data.Sqlite;
using System;
using System.Globalization;
using System.Text.RegularExpressions;

//This class is not static so later on we can use inheritance and interfaces
public class AccountsLogic
{

    //Static properties are shared across all instances of the class
    //This can be used to get the current logged in account from anywhere in the program
    //private set, so this can only be set by the class itself
    public static UserModel? CurrentAccount { get; private set; }
    private UserAccess _userAccess = new UserAccess();

    public UserModel? CheckLogin(string email, string password)
    {
        UserModel? acc = _userAccess.SelectByEmail(email);

        if (acc != null && acc.Password == sha256_hash(password))
        {
            CurrentAccount = acc;
            return acc;
        }
        return null;
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




    public static void ChangeAccount(UserModel user)
    {
        CurrentAccount = user;
    }

    public static void LogOut()
    {
        CurrentAccount = null;
    }

        public bool IsValidEmail(string email)
    {
        string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        return Regex.IsMatch(email, pattern);
    }

    public bool IsValidPhoneNumber(string phoneNumber)
    {
        string pattern = @"^\+?[0-9]{10,15}$";
        return Regex.IsMatch(phoneNumber, pattern);
    }

    public bool IsValidDateOfBirth(string dateOfBirth)
    {
        string expectedFormat = "dd-MM-yyyy";
        var culture = CultureInfo.InvariantCulture;
        var style = DateTimeStyles.None;

        bool isSuccessful = DateTime.TryParseExact(dateOfBirth, expectedFormat, culture, style, out DateTime result);

        if (!isSuccessful)
        {
            return false;
        }

        DateTime today = DateTime.Today;
        DateTime earliestAllowed = today.AddYears(-150);

        if (result >= today)
        {
            return false;
        }

        if (result < earliestAllowed)
        {
            return false;
        }

        return true;
    }

    public DateTime ParseDateOfBirth(string dateOfBirth)
    {
        string expectedFormat = "dd-MM-yyyy";
        var culture = CultureInfo.InvariantCulture;

        return DateTime.ParseExact(dateOfBirth, expectedFormat, culture);
    }

    public bool IsValidPassword(string password)
    {
        if (password.Length < 12) return false;
        bool hasUpper = Regex.IsMatch(password, @"[A-Z]");
        bool hasNumber = Regex.IsMatch(password, @"[0-9]");
        bool hasSpecial = Regex.IsMatch(password, @"[^a-zA-Z0-9]");
        return hasUpper && hasNumber && hasSpecial;
    }

    public bool EmailAlreadyExists(string email)
    {
        UserModel existing = _userAccess.SelectByEmail(email);
        return existing != null;
    }

    public void AddUser(UserModel user)
    {
        _userAccess.Write(user);
    }
    public bool IsValidFirstName(string firstName)
    {
        double output_parse;
        return !string.IsNullOrWhiteSpace(firstName) && firstName.Trim().Length >= 2 && !double.TryParse(firstName, out output_parse);
    }

    public bool IsValidLastName(string lastName)
    {
        double output_parse;
        return !string.IsNullOrWhiteSpace(lastName) && lastName.Trim().Length >= 2 && !double.TryParse(lastName, out output_parse);
    }


}