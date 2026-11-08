using Microsoft.Data.Sqlite;
using Dapper;

public class MovieAccess : IDataAccess<MovieModel>
{
    private SqliteConnection _connection =
        new SqliteConnection("Data Source=DataSources/project.db");

    private string Table = "Movie";

    public void AddMovie(MovieModel movie)
    {
        string sql = $@"
            INSERT INTO {Table}
            (id, title, description, director, genre, ageLimit, duration)
            VALUES
            (@Id, @Title, @Description, @Director, @Genre, @AgeLimit, @Duration)";

        _connection.Execute(sql, movie);
    }

    public long GetNextMovieId()
    {
        string sql = $"SELECT COALESCE(MAX(id), 0) + 1 FROM {Table}";

        return _connection.ExecuteScalar<long>(sql);
    }

    public void Write(MovieModel movie)
    {
        if (movie.Id == 0)
        {
            movie.Id = GetNextMovieId();
        }

        AddMovie(movie);
    }

    public MovieModel Select(int id)
    {
        string sql = $"SELECT * FROM {Table} WHERE id = @Id";

        return _connection.QueryFirstOrDefault<MovieModel>(
            sql, new { Id = id })!;
    }

    public MovieModel GetByTitle(string title)
    {
        string sql = $"SELECT * FROM {Table} WHERE title = @Title";

        return _connection.QueryFirstOrDefault<MovieModel>(
            sql, new { Title = title })!;
    }

    public void Update(MovieModel movie)
    {
        string sql = $@"
            UPDATE {Table}
            SET title = @Title,
                description = @Description,
                director = @Director,
                genre = @Genre,
                ageLimit = @AgeLimit,
                duration = @Duration
            WHERE id = @Id";

        _connection.Execute(sql, movie);
    }

    public void Delete(MovieModel movie)
    {
        string sql = $"DELETE FROM {Table} WHERE id = @Id";

        _connection.Execute(sql, new { Id = movie.Id });
    }

    public List<MovieModel> SelectAll()
    {
        string sql = $"SELECT * FROM {Table}";

        return _connection.Query<MovieModel>(sql).ToList();
    }
}