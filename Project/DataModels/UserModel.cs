using System;

public class UserModel
{
    public Int64 Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
    public Int64 DateOfBirth { get; set; }
    public string Role { get; set; }

    public DateTime HumanReadableDateOfBirth
    {
        get
        {
            return DateTimeOffset.FromUnixTimeSeconds(DateOfBirth).LocalDateTime;
        }
        set
        {
            DateOfBirth = ((DateTimeOffset)value).ToUnixTimeSeconds();
        }
    }

    public UserModel(Int64 id,  string firstName, string lastName, string email, string phoneNumber, string password, Int64 dateOfBirth, string role)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PhoneNumber = phoneNumber;
        Password = password;
        DateOfBirth = dateOfBirth;
        Role = role;
    }

    public UserModel()
    {
    }
}