using Dapper;
using Microsoft.Data.Sqlite;

public class SeatAccess: IDataAccess<SeatModel>
{

    private SqliteConnection _connection = new SqliteConnection($"Data Source=DataSources/project.db");

    private string Table = "Seat";

    public void Write(SeatModel reservation)
    {
        string sql = $"INSERT INTO {Table} (roomnumber, seattype, seatCost) VALUES (@RoomNumber, @SeatType, @SeatCost)";
        _connection.Execute(sql, reservation);
    }

    public  SeatModel Select(int id )
    {
        string sql = $"SELECT * FROM {Table} WHERE  id = @Id";
        return _connection.QueryFirstOrDefault<SeatModel>(sql, new {Id = id});
    }

    public  SeatModel SelectFromSeatOrder(SeatOrderModel model )
    {
        string sql = $"SELECT * FROM {Table} WHERE id = @SeatId";
        return _connection.QueryFirstOrDefault<SeatModel>(sql, model);
    }


    public void Update(SeatModel Seat)
    {
        string sql = $"UPDATE {Table} SET roomnumber = @RoomNumber, seattype = @SeatType, seatCost = @SeatCost WHERE id = @Id";
        _connection.Execute(sql, Seat);
    }

    public void Delete(SeatModel Seat)
    {
        string sql = $"DELETE FROM {Table} WHERE id = @Id";
        _connection.Execute(sql, new { Id = Seat.Id });
    }
}