using Dapper;
using Microsoft.Data.Sqlite;

public class ReservationAccess : IDataAccess<ReservationModel>
{

    private SqliteConnection _connection = new SqliteConnection($"Data Source=DataSources/project.db");

    private string Table = "Reservation";

    public void Write(ReservationModel reservation)
    {
        string sql = $"INSERT INTO {Table} (id, movieShowingId, userId) VALUES (@Id, @MovieShowingId, @UserId)";
        _connection.Execute(sql, reservation);
    }


    public long getNextReservationId()
    {
        string sql = $"SELECT COALESCE(MAX(id), 0) + 1 FROM {Table}";

        return _connection.ExecuteScalar<long>(sql);
    }


    public ReservationModel Select(int id )
    {
        string sql = $"SELECT * FROM {Table} WHERE  id = @Id";
        return _connection.QueryFirstOrDefault<ReservationModel>(sql, new {Id = id});
    }

    public List<ReservationModel> GetByUser(UserModel user)
    {
        string sql = $"SELECT * FROM {Table} WHERE userId = @UserId";
        return _connection.Query<ReservationModel>(sql, new { UserId = user.Id}).ToList();
    }

    public void Update(ReservationModel reservation)
    {
        string sql = $"UPDATE {Table} SET movieShowingId = @MovieShowingId, userId = @UserId WHERE id = @Id";
        _connection.Execute(sql, reservation);
    }

    public void Delete(ReservationModel reservation)
    {
        string sql = $"DELETE FROM {Table} WHERE id = @Id";
        _connection.Execute(sql, new { Id = reservation.Id });
    }
}