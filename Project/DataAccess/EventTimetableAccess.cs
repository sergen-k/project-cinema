using Dapper;
using Microsoft.Data.Sqlite;

public class EventTimetableAccess : IDataAccess<EventTimetableModel>
{
    private SqliteConnection _connection =
        new("Data Source=DataSources/project.db");

    private const string Table = "EventTimetable";

    public void Write(EventTimetableModel model)
    {
        string sql = $"INSERT INTO {Table} (eventId, timetableId) VALUES (@EventId, @TimetableId)";
        _connection.Execute(sql, model);
    }

    public EventTimetableModel SelectAllById(int id)
    {
        string sql = $"SELECT * FROM {Table} WHERE id = @Id";
        return _connection.QueryFirstOrDefault<EventTimetableModel>(sql, new { Id = id });
    }

    public List<EventTimetableModel> SelectAll()
    {
        string sql = $"SELECT * FROM {Table}";
        return _connection.Query<EventTimetableModel>(sql).ToList();
    }

    public List<EventTimetableModel> SelectAllByMovieId(long movieId)
    {
        string sql = $@"
            SELECT eventTimetable.*
            FROM {Table} AS eventTimetable
            INNER JOIN Timetable AS timetable
                ON timetable.id = eventTimetable.timetableId
            WHERE timetable.movieId = @MovieId";

        return _connection.Query<EventTimetableModel>(
            sql, new { MovieId = movieId }).ToList();
    }

    public void Update(EventTimetableModel model)
    {
        string sql = $"UPDATE {Table} SET eventId = @EventId, timetableId = @TimetableId WHERE id = @Id";
        _connection.Execute(sql, model);
    }

    public void Delete(EventTimetableModel model)
    {
        string sql = $"DELETE FROM {Table} WHERE id = @Id";
        _connection.Execute(sql, new { Id = model.Id });
    }
}
