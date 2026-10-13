using Microsoft.Data.Sqlite;

using Dapper;


public class AccountsAccess : IDataAccess<UserModel>
{
    private SqliteConnection _connection = new SqliteConnection($"Data Source=DataSources/project.db");

    private string Table = "Accounts";

    public void Write(UserModel account)
    {
        string sql = $"INSERT INTO {Table} (email, password, fullname) VALUES (@EmailAddress, @Password, @FullName)";
        _connection.Execute(sql, account);
    }

    public UserModel Select(int id )
    {
        string sql = $"SELECT * FROM {Table} WHERE  id = @Id";
        return _connection.QueryFirstOrDefault<UserModel>(sql, new {  Id = id});
    }

    public UserModel GetByEmail(string email)
    {
        string sql = $"SELECT * FROM {Table} WHERE email = @Email";
        return _connection.QueryFirstOrDefault<UserModel>(sql, new { Email = email });
    }

    public void Update(UserModel account)
    {
        string sql = $"UPDATE {Table} SET email = @EmailAddress, password = @Password, fullname = @FullName WHERE id = @Id";
        _connection.Execute(sql, account);
    }

    public void Delete(UserModel account)
    {
        string sql = $"DELETE FROM {Table} WHERE id = @Id";
        _connection.Execute(sql, new { Id = account.Id });
    }
}