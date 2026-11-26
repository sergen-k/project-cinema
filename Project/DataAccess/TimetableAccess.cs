using Dapper;
using Microsoft.Data.Sqlite;

public class TimetableAccess : IDataAccess<TimetableModel>
{

    private SqliteConnection _connection = new SqliteConnection($"Data Source=DataSources/project.db");

    private string Table = "Timetable";

    public void Write(TimetableModel reservation)
    {
        if (reservation.Id == 0)
        {
            reservation.Id = GetNextTimetableId();
        }

        string sql = $"INSERT INTO {Table} (id, startTime, endTime, movieId, roomNumber) VALUES (@Id, @StartTime, @EndTime, @MovieId, @RoomNumber)";
        _connection.Execute(sql, reservation);
    }

    public TimetableModel SelectAllById(int id)
    {
        string sql = $"SELECT * FROM {Table} WHERE  id = @Id";
        return _connection.QueryFirstOrDefault<TimetableModel>(sql, new { Id = id });
    }

    public List<TimetableModel> SelectByMovieId(MovieModel movie)
    {
        string sql = $"SELECT * FROM {Table} WHERE  movieId = @Id";
        return _connection.Query<TimetableModel>(sql, new { Id = movie.Id }).ToList();

    }
    public List<TimetableModel> SelectByRoom(int roomNumber)
    {
        string sql = $"SELECT * FROM {Table} WHERE roomNumber = @RoomNumber";
        return _connection.Query<TimetableModel>(sql, new { RoomNumber = roomNumber }).ToList();
    }

    public int ReservationCount(TimetableModel model)
    {
        string sql = $"SELECT sum(People) as val FROM Reservation where movieShowingId = @Id";
        return _connection.ExecuteScalar<int>(sql, model);
    }

    public List<TimetableModel> SelectAll()
    {
        string sql = $"SELECT * FROM {Table}";
        return _connection.Query<TimetableModel>(sql).ToList();
    }
    

    public void Update(TimetableModel timetable)
    {
        string sql = $"UPDATE {Table} SET startTime = @StartTime, endTime = @EndTime, movieId = @MovieId, roomNumber = @RoomNumber WHERE id = @Id";
        _connection.Execute(sql, timetable);
    }
    public void Delete(TimetableModel timetable)
    {
        string sql = $"DELETE FROM {Table} WHERE id = @Id";
        _connection.Execute(sql, new { Id = timetable.Id });
    }
    public long GetNextTimetableId()
    {
        string sql = $"SELECT COALESCE(MAX(id), 0) + 1 FROM {Table}";
        return _connection.ExecuteScalar<long>(sql);
    }





}