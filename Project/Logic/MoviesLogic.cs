public class MoviesLogic
{
    private MovieAccess _movieAccess = new MovieAccess();

    public bool AddMovie(UserModel? user, string title, string genre, string director,
                         string description, long ageLimit, string duration)
    {
        if (user == null || user.Rol != "admin")
        {
            return false;
        }

        if (string.IsNullOrWhiteSpace(title))
        {
            return false;
        }

        if (string.IsNullOrWhiteSpace(genre))
        {
            return false;
        }

        if (string.IsNullOrWhiteSpace(director))
        {
            return false;
        }

        if (string.IsNullOrWhiteSpace(description))
        {
            return false;
        }

        if (ageLimit < 0)
        {
            return false;
        }

        double minutes;

        if (!double.TryParse(duration, out minutes) || minutes <= 0)
        {
            return false;
        }

        long id = _movieAccess.GetNextMovieId();

        MovieModel movie = new MovieModel(
            id,
            title,
            description,
            director,
            genre,
            ageLimit,
            minutes
        );

        _movieAccess.AddMovie(movie);

        return true;
    }
}