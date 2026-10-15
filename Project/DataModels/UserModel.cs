public class UserModel
{

    public Int64 Id { get; set; }
    public string EmailAddress { get; set; }
    public string Password { get; set; }
    public string FullName { get; set; }
    public string Rol {get; set; }

    public UserModel(Int64 id, string email, string password, string fullname, string rol)
    {
        Id = id;
        EmailAddress = email;
        Password = password;
        FullName = fullname;
        Rol = rol;
    }
    public UserModel(long id, string email, string password, string fullname)
        : this(id, email, password, fullname, "customer")
    {
    }

    public UserModel()
    {
        EmailAddress = "";
        Password = "";
        FullName = "";
        Rol = "";
    }
}