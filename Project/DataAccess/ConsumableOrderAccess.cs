using Dapper;
using Microsoft.Data.Sqlite;

public class ConsumableOrderAccess
{
    public void SaveForReservation(long reservationId, List<ConsumableOrderModel> selectedItems)
    {
        if (reservationId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(reservationId));
        }

        foreach (ConsumableOrderModel item in selectedItems)
        {
            if (item.ConsumableId <= 0 || item.Quantity <= 0 || item.UnitPrice < 0)
            {
                throw new ArgumentException("Invalid consumable order.");
            }
        }

        using (SqliteConnection connection = new SqliteConnection("Data Source=DataSources/project.db"))
        {
            connection.Open();
            connection.Execute("PRAGMA foreign_keys = ON");

            using (SqliteTransaction transaction = connection.BeginTransaction())
            {
                string sql = @"
                    INSERT INTO ConsumableOrder
                    (reservationId, consumableId, quantity, unitPrice)
                    VALUES
                    (@ReservationId, @ConsumableId, @Quantity, @UnitPrice)";

                foreach (ConsumableOrderModel item in selectedItems)
                {
                    connection.Execute(sql, new
                    {
                        ReservationId = reservationId,
                        ConsumableId = item.ConsumableId,
                        Quantity = item.Quantity,
                        UnitPrice = item.UnitPrice
                    }, transaction);
                }

                transaction.Commit();
            }
        }
    }
}