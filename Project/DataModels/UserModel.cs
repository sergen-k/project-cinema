

public class UserModel
{
    public Int64 Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
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

    public UserModel(Int64 id, string fullName, string email, string phone, string password, Int64 dateOfBirth, string role)
    {
        Id = id;
        FullName = fullName;
        Email = email;
        Phone = phone;
        Password = password;
        DateOfBirth = dateOfBirth;
        Role = role;
    }

    public UserModel()
    {
    }
}