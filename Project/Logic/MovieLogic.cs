class MovieLogic
{
    private readonly MovieAccess _MovieAccess = new();

    public MovieLogic()
    {
    }

    public MovieModel Select(int id)
    {
        return _MovieAccess.Select(id);
    }
}