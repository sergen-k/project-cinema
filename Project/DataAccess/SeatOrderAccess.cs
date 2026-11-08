using Dapper;
using Microsoft.Data.Sqlite;

public class SeatOrderAccess: IDataAccess<SeatOrderModel>
{

    private SqliteConnection _connection = new SqliteConnection($"Data Source=DataSources/project.db");

    private string Table = "SeatOrder";

    public void Write(SeatOrderModel reservation)
    {
        string sql = $"INSERT INTO {Table} (reservationId, seatId) VALUES (@ReservationId, @SeatId)";
        _connection.Execute(sql, reservation);
    }

    public  SeatOrderModel Select(int id )
    {
        string sql = $"SELECT * FROM {Table} WHERE  id = @Id";
        return _connection.QueryFirstOrDefault<SeatOrderModel>(sql, new {Id = id});
    }

    public  List<SeatOrderModel> SelectForReservation(ReservationModel reservation )
    {
        string sql = $"SELECT * FROM {Table} WHERE reservationId = @Id";
        return _connection.Query<SeatOrderModel>(sql, reservation).ToList();
    }


    public void Update(SeatOrderModel SeatOrder)
    {
        string sql = $"UPDATE {Table} SET reservationId = @ReservationId, seatId = @SeatId WHERE id = @Id";
        _connection.Execute(sql, SeatOrder);
    }

    public void Delete(SeatOrderModel SeatOrder)
    {
        string sql = $"DELETE FROM {Table} WHERE id = @Id";
        _connection.Execute(sql, new { Id = SeatOrder.Id });
    }
}