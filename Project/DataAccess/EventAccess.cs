using Dapper;
using Microsoft.Data.Sqlite;

public class EventAccess : IDataAccess<EventModel>
{
    private readonly SqliteConnection _connection =
        new("Data Source=DataSources/project.db");

    private const string Table = "EventTable";

    public void Write(EventModel model)
    {
        string sql = $"INSERT INTO {Table} (name, description, cost) VALUES (@Name, @Description, @Cost)";
        _connection.Execute(sql, model);
    }

    public EventModel SelectAllById(int id)
    {
        string sql = $"SELECT * FROM {Table} WHERE id = @Id";
        return _connection.QueryFirstOrDefault<EventModel>(sql, new { Id = id });
    }

    public void Update(EventModel model)
    {
        string sql = $"UPDATE {Table} SET name = @Name, description = @Description, cost = @Cost WHERE id = @Id";
        _connection.Execute(sql, model);
    }

    public void Delete(EventModel model)
    {
        string sql = $"DELETE FROM {Table} WHERE id = @Id";
        _connection.Execute(sql, new { Id = model.Id });
    }

    public List<EventModel> SelectAll()
    {
        string sql = $"SELECT * FROM {Table}";
        return _connection.Query<EventModel>(sql).ToList();
    }
}
