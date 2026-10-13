using Dapper;
using Microsoft.Data.Sqlite;

public class MovieAccess: IDataAccess<MovieModel>
{

    private SqliteConnection _connection = new SqliteConnection($"Data Source=DataSources/project.db");

    private string Table = "movie";

    public void Write(MovieModel reservation)
    {
        string sql = $@"INSERT INTO {Table} (name, description, director, ageLimit, duration)
                        VALUES (@Name, @Description, @Director, @AgeLimit, @Duration)
        ";
        _connection.Execute(sql, reservation);
    }

    public  MovieModel Select(int id )
    {
        string sql = $"SELECT * FROM {Table} WHERE  id = @Id";
        return _connection.QueryFirstOrDefault<MovieModel>(sql, new {Id = id});
    }

    public void Update(MovieModel timetable)
    {
        string sql = $@"UPDATE {Table} SET name = @Name, description = @Description, 
                        director = @Director, ageLimit = @AgeLimit, duration = @Duration 
                        WHERE id = @Id";
        _connection.Execute(sql, timetable);
    }

    public void Delete(MovieModel timetable)
    {
        string sql = $"DELETE FROM {Table} WHERE id = @Id";
        _connection.Execute(sql, new { Id = timetable.Id });
    }
}