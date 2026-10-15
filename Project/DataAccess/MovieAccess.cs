using Microsoft.Data.Sqlite;
using Dapper;

public class MovieAccess
{
    private SqliteConnection _connection =
        new SqliteConnection("Data Source=DataSources/project.db");

    public void AddMovie(MovieModel movie)
    {
        string sql = @"
            INSERT INTO Movie
            (id, title, description, director, genre, ageLimit, duration)
            VALUES
            (@Id, @Title, @Description, @Director, @Genre, @AgeLimit, @Duration)";

        _connection.Execute(sql, movie);
    }

    public long GetNextMovieId()
    {
        string sql = "SELECT COALESCE(MAX(id), 0) + 1 FROM Movie";

        return _connection.ExecuteScalar<long>(sql);
    }
}