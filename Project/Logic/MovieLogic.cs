public class MovieLogic
{
    private readonly MovieAccess _movieAccess = new();

    public MovieLogic()
    {
    }

    public bool AddMovie(UserModel? user, string title, string genre, string director, string description, long ageLimit, string duration)
    {
        if (user == null || user.Role != "admin")
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


    public static bool IsOfAge(MovieModel movie)
    {
        if (UserLogic.IsLoggedIn())
        {
            DateTime today = DateTime.Today;
            DateTime birthdate = UserLogic.CurrentAccount.HumanReadableDateOfBirth;

            int age = DateTime.Today.Year-birthdate.Year;
            if (today < birthdate.AddYears(age)) {age--;}
            
            if (age >= movie.AgeLimit){return true;}
        }
        return false;
    }

    public MovieModel Select(int id)
    {
        return _movieAccess.SelectAllById(id);
    }

    public List<MovieModel> SelectAll()
    {
        return _movieAccess.SelectAll();
    }

    public List<MovieModel> SelectNextTwoWeeks()
    {
        return _movieAccess.SelectNextTwoWeeks();
    }

    public bool MovieHasEvent(long movieId, List<EventTimetableModel> eventTimetables, List<TimetableModel> movieTimetables)
    {
        bool hasEvent = movieTimetables
        .Where(t => t.MovieId == movieId)
        .Any(t => eventTimetables.Any(et => et.TimetableId == t.Id));

        return hasEvent;
    }

}