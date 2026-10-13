using Dapper;
using Microsoft.Data.Sqlite;

public class TimetableAccess: IDataAccess<TimetableModel>
{

    private SqliteConnection _connection = new SqliteConnection($"Data Source=DataSources/project.db");

    private string Table = "Timetable";

    public void Write(TimetableModel reservation)
    {
        string sql = $"INSERT INTO {Table} (showtime, movieId, roomNumber) VALUES (@ShowTime, @MovieId, @RoomNumber)";
        _connection.Execute(sql, reservation);
    }

    public  TimetableModel Select(int id )
    {
        string sql = $"SELECT * FROM {Table} WHERE  id = @Id";
        return _connection.QueryFirstOrDefault<TimetableModel>(sql, new {Id = id});
    }

    public void Update(TimetableModel timetable)
    {
        string sql = $"UPDATE {Table} SET showtime = @ShowTime, movieId = @MovieId, roomNumber = @RoomNumber WHERE id = @Id";
        _connection.Execute(sql, timetable);
    }

    public void Delete(TimetableModel timetable)
    {
        string sql = $"DELETE FROM {Table} WHERE id = @Id";
        _connection.Execute(sql, new { Id = timetable.Id });
    }
}