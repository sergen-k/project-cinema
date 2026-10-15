using System;
using System.Globalization;
using System.Text.RegularExpressions;

public class UserLogic
{
    private UserAccess _userAccess;

    public UserLogic(UserAccess userAccess)
    {
        _userAccess = userAccess;
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

        bool success = DateTime.TryParseExact(dateOfBirth, expectedFormat, culture, style, out DateTime result);

        return success;
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
}