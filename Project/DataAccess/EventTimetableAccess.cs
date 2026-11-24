using Dapper;
using Microsoft.Data.Sqlite;

public class EventTimetableAccess : IDataAccess<EventTimetableModel>
{
    private SqliteConnection _connection = new SqliteConnection($"Data Source=DataSources/project.db");

    private string Table = "EventTimetable";

    public void Write(EventTimetableModel EventTimetable)
    {
        string sql = $"INSERT INTO {Table} (EventId, TimetableId) VALUES (@EventId, @TimetableId)";
        _connection.Execute(sql, EventTimetable);
    }

    public EventTimetableModel SelectAllById(int id)
    {
        string sql = $"SELECT * FROM {Table} WHERE id = @Id";
        return _connection.QueryFirstOrDefault<EventTimetableModel>(sql, new { Id = id });
    }

    public List<EventTimetableModel> SelectByEventId(int eventId)
    {
        string sql = $"SELECT * FROM {Table} WHERE EventId = @EventId";
        return _connection.Query<EventTimetableModel>(sql, new { EventId = eventId }).ToList();
    }

    public List<EventTimetableModel> SelectByTimetableId(string timetableId)
    {
        string sql = $"SELECT * FROM {Table} WHERE TimetableId = @TimetableId";
        return _connection.Query<EventTimetableModel>(sql, new { TimetableId = timetableId }).ToList();
    }

    public void Update(EventTimetableModel EventTimetable)
    {
        string sql = $"UPDATE {Table} SET EventId = @EventId, TimetableId = @TimetableId WHERE id = @Id";
        _connection.Execute(sql, EventTimetable);
    }

    public void Delete(EventTimetableModel EventTimetable)
    {
        string sql = $"DELETE FROM {Table} WHERE id = @Id";
        _connection.Execute(sql, new { Id = EventTimetable.Id });
    }
}