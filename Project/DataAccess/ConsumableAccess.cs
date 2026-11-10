using Microsoft.Data.Sqlite;
using Dapper;

public class ConsumableAccess : IDataAccess<ConsumableModel>
{
    private SqliteConnection _connection = new SqliteConnection("Data Source=DataSources/project.db");

    public List<ConsumableModel> GetAll()
    {
        string sql = "SELECT * FROM Consumable";
        return _connection.Query<ConsumableModel>(sql).ToList();
    }

    public void Write(ConsumableModel model)
    {
        string sql = @"INSERT INTO Consumable (name, category, price)
                       VALUES (@Name, @Category, @Price)";

        _connection.Execute(sql, model);
    }

    public ConsumableModel Select(int id)
    {
        string sql = "SELECT * FROM Consumable WHERE id = @Id";

        return _connection.Query<ConsumableModel>(sql, new { Id = id }).First();
    }

    public void Update(ConsumableModel model)
    {
        string sql = @"UPDATE Consumable
                       SET name = @Name,
                           category = @Category,
                           price = @Price
                       WHERE id = @Id";

        _connection.Execute(sql, model);
    }

    public void Delete(ConsumableModel model)
    {
        string sql = "DELETE FROM Consumable WHERE id = @Id";

        _connection.Execute(sql, model);
    }
}