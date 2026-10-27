using System.Security.Cryptography;
using System.Text;
using Microsoft.Data.Sqlite;
//This class is not static so later on we can use inheritance and interfaces
public class AccountsLogic
{

    //Static properties are shared across all instances of the class
    //This can be used to get the current logged in account from anywhere in the program
    //private set, so this can only be set by the class itself
    public static UserModel? CurrentAccount { get; private set; }
    private UserAccess _access = new UserAccess();

    public UserModel? CheckLogin(string email, string password)
    {
        UserModel? acc = _access.SelectByEmail(email);

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
}