using Dapper;
using Microsoft.Data.Sqlite;

public class EventAccess : IDataAccess<EventModel>
{
    private readonly string _connectionString =
        "Data Source=DataSources/project.db";

    private readonly string Table = "Event";

    public void Write(EventModel Event)
    {
        using var connection = new SqliteConnection(_connectionString);

        string sql = $"INSERT INTO {Table} (Name, Description, Cost) VALUES (@Name, @Description, @Cost)";

        connection.Execute(sql, Event);
    }

    public EventModel SelectAllById(int id)
    {
        using var connection = new SqliteConnection(_connectionString);

        string sql = $"SELECT * FROM {Table} WHERE Id = @Id";

        return connection.QueryFirstOrDefault<EventModel>(
            sql, new { Id = id });
    }

    public List<EventModel> SelectAll()
    {
        using var connection = new SqliteConnection(_connectionString);

        string sql = $"SELECT * FROM {Table}";

        return connection.Query<EventModel>(sql).ToList();
    }

    public void Update(EventModel Event)
    {
        using var connection = new SqliteConnection(_connectionString);

        string sql = $@"UPDATE {Table}
                        SET Name = @Name,
                            Description = @Description,
                            Cost = @Cost
                        WHERE Id = @Id";

        connection.Execute(sql, Event);
    }

    public void Delete(EventModel Event)
    {
        using var connection = new SqliteConnection(_connectionString);

        string sql = $"DELETE FROM {Table} WHERE Id = @Id";

        connection.Execute(sql, new { Id = Event.Id });
    }
}
