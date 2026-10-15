using Dapper;
using Microsoft.Data.Sqlite;

public class UserAccess : IDataAccess<UserModel>
{
    private SqliteConnection _connection;
    private string Table = "User";

    public UserAccess(SqliteConnection connection)
    {
        _connection = connection;
    }

    public void Write(UserModel model)
    {
        string sql = $@"INSERT INTO {Table} 
                        (FullName, Email, Phone, Password, DateOfBirth, Role) 
                        VALUES (@FullName, @Email, @Phone, @Password, @DateOfBirth, @Role)";
        _connection.Execute(sql, model);
    }

    public UserModel Select(int id)
    {
        string sql = $"SELECT * FROM {Table} WHERE Id = @Id";
        return _connection.QueryFirstOrDefault<UserModel>(sql, new { Id = id });
    }

    public void Update(UserModel model)
    {
        string sql = $@"UPDATE {Table} 
                        SET FullName = @FullName, Email = @Email, Phone = @Phone, 
                            Password = @Password, DateOfBirth = @DateOfBirth, Role = @Role 
                        WHERE Id = @Id";
        _connection.Execute(sql, model);
    }

    public void Delete(UserModel model)
    {
        string sql = $"DELETE FROM {Table} WHERE Id = @Id";
        _connection.Execute(sql, model);
    }

    public UserModel SelectByEmail(string email)
    {
        string sql = $"SELECT * FROM {Table} WHERE Email = @Email";
        return _connection.QueryFirstOrDefault<UserModel>(sql, new { Email = email });
    }
}