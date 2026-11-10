using Dapper;
using Microsoft.Data.Sqlite;

public class ConsumableOrderAccess : IDataAccess<ConsumableOrderModel>
{
    private SqliteConnection _connection = new SqliteConnection("Data Source=DataSources/project.db");

    public void Write(ConsumableOrderModel model)
    {
        List<ConsumableOrderModel> items = new List<ConsumableOrderModel>();

        items.Add(model);

        SaveForReservation(model.ReservationId, items);
    }

    public ConsumableOrderModel Select(int id)
    {
        string sql = "SELECT * FROM ConsumableOrder WHERE id = @Id";

        return _connection.Query<ConsumableOrderModel>(
            sql, new { Id = id }).First();
    }

    public void Update(ConsumableOrderModel model)
    {
        string sql = @"UPDATE ConsumableOrder
                    SET reservationId = @ReservationId,
                        consumableId = @ConsumableId,
                        quantity = @Quantity,
                        unitPrice = @UnitPrice
                    WHERE id = @Id";

        _connection.Execute(sql, model);
    }

    public void Delete(ConsumableOrderModel model)
    {
        string sql = "DELETE FROM ConsumableOrder WHERE id = @Id";

        _connection.Execute(sql, model);
    }

 public ConsumableOrderModel SelectAllById(int id)
{
    return null;
}

    public void SaveForReservation(long reservationId, List<ConsumableOrderModel> selectedItems)
    {
        if (reservationId <= 0)
        {
            throw new ArgumentException("Invalid reservation ID.");
        }

        foreach (ConsumableOrderModel item in selectedItems)
        {
            if (item.ConsumableId <= 0 || item.Quantity <= 0 || item.UnitPrice < 0)
            {
                throw new ArgumentException("Invalid consumable order.");
            }
        }

        string sql = @"INSERT INTO ConsumableOrder
                    (reservationId, consumableId, quantity, unitPrice)
                    VALUES
                    (@ReservationId, @ConsumableId, @Quantity, @UnitPrice)";

        foreach (ConsumableOrderModel item in selectedItems)
        {
            _connection.Execute(sql, new
            {
                ReservationId = reservationId,
                ConsumableId = item.ConsumableId,
                Quantity = item.Quantity,
                UnitPrice = item.UnitPrice
            });
        }
    }
}