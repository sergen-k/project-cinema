using Dapper;
using Microsoft.Data.Sqlite;

public class UserAccess : IDataAccess<UserModel>
{
    private SqliteConnection _connection =
        new SqliteConnection("Data Source=DataSources/project.db");
    private string Table = "User";

    public UserAccess()
    {
    }

    public void Write(UserModel model)
    {
        string sql = $@"INSERT INTO {Table} 
                        (id, FullName, Email, phoneNumber, Password, DateOfBirth, Role) 
                        VALUES (@Id, @FullName, @Email, @PhoneNumber, @Password, @DateOfBirth, @Role)";
        _connection.Execute(sql, model);
    }


    public long GetNextUserId()
    {
        string sql = $"SELECT COALESCE(MAX(id), 0) + 1 FROM {Table}";

        return _connection.ExecuteScalar<long>(sql);
    }


    public UserModel Select(int id)
    {
        string sql = $"SELECT * FROM {Table} WHERE Id = @Id";
        return _connection.QueryFirstOrDefault<UserModel>(sql, new { Id = id });
    }

    public void Update(UserModel model)
    {
        string sql = $@"UPDATE {Table} 
                        SET FullName = @FullName, Email = @Email, phoneNumber  = @PhoneNumber, 
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