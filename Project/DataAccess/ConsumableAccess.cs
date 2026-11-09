using Microsoft.Data.Sqlite;
using Dapper;

public class ConsumableAccess
{
    private SqliteConnection _connection =
        new SqliteConnection("Data Source=DataSources/project.db");

    public List<ConsumableModel> GetAll()
    {
        string sql = "SELECT * FROM Consumable";

        return _connection.Query<ConsumableModel>(sql).ToList();
    }
}