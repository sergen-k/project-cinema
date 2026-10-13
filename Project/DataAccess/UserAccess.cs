using Microsoft.Data.Sqlite;
using Dapper;

public class UserAccess
{
    private SqliteConnection _connection = new SqliteConnection("Data Source=DataSources/project.db");

    private string Table = "User";

    public UserModel? SelectByEmail(string email)
    {
        string sql = $"SELECT id, email AS EmailAddress, password, fullName FROM {Table} WHERE email = @Email";

        return _connection.QueryFirstOrDefault<UserModel>(sql, new { Email = email });
    }
}