public class TimetableLogic
{
    private readonly TimetableAccess _timeTableAccess = new();
    public TimetableModel Select(int id)
    {
        return _timeTableAccess.SelectAllById(id);
    }

    public List<TimetableModel> SelectByMovieId(MovieModel movie)
    {
        List<TimetableModel> models = _timeTableAccess.SelectByMovieId(movie);;

        List<TimetableModel> modelsToReturn = [];

        foreach (TimetableModel model in models)
        {
            int reservationCount = _timeTableAccess.ReservationCount(model);
            switch (model.RoomNumber)
            {
                case 1: 
                    if (reservationCount < 150)
                    {
                        modelsToReturn.Add(model);
                    }
                break;
            }

        }

        return modelsToReturn;
    }
}