
using Dapper;
using Microsoft.Data.Sqlite;

public class PaymentAccess : IDataAccess<PaymentModel>
{
    private SqliteConnection _connection = new SqliteConnection("Data Source=DataSources/project.db");

    public void Write(PaymentModel model)
    {
        string sql = @"INSERT INTO Payment
                    (reservationId, paymentNumber, method, status, amount)
                    VALUES
                    (@ReservationId, @PaymentNumber, @Method, @Status, @Amount)";

        _connection.Execute(sql, model);
    }

    public PaymentModel Select(int id)
    {
        string sql = "SELECT * FROM Payment WHERE id = @Id";

        return _connection.Query<PaymentModel>(sql, new { Id = id }).First();
    }

    public void Update(PaymentModel model)
    {
        string sql = @"UPDATE Payment
                    SET reservationId = @ReservationId,
                        paymentNumber = @PaymentNumber,
                        method = @Method,
                        status = @Status,
                        amount = @Amount
                    WHERE id = @Id";

        _connection.Execute(sql, model);
    }

    public void Delete(PaymentModel model)
    {
        string sql = "DELETE FROM Payment WHERE id = @Id";

        _connection.Execute(sql, model);
    }

    public PaymentModel SelectAllById(int id)
    {
        return Select(id);
    }

    public bool PaymentNumberExists(string paymentNumber)
    {
        string sql = @"SELECT COUNT(*) FROM Payment
                       WHERE paymentNumber = @PaymentNumber";

        int count = _connection.Query<int>(sql, new { PaymentNumber = paymentNumber }).First();

        return count > 0;
    }
}
