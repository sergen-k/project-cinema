
using Microsoft.Data.Sqlite;
//This class is not static so later on we can use inheritance and interfaces
public class AccountsLogic
{

    //Static properties are shared across all instances of the class
    //This can be used to get the current logged in account from anywhere in the program
    //private set, so this can only be set by the class itself
    public static UserModel? CurrentAccount { get; private set; }
    private UserAccess _access = new UserAccess(new SqliteConnection("Data Source=DataSources/project.db"));

    public UserModel? CheckLogin(string email, string password)
    {
        UserModel? acc = _access.SelectByEmail(email);

        if (acc != null && acc.Password == password)
        {
            CurrentAccount = acc;
            return acc;
        }
        return null;
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