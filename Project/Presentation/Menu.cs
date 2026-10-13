static class Menu
{
    public static UserModel User = null;
    private static Dictionary<int,string> _options = new();
    public static void Start()
    {
        LoadOptions();
        PrintMenuOptions();
        string chosenMenu = _options[Options()];
        switch (chosenMenu) // Got to give each option a body
        {
            case "About Us":
                break;
            case "Browse Movies":
                break;
            case "Register":
                break;
            case "Log In":
                break;
            case "Log Out":
                break;
            case "View Reservations":
                break;
        }
    }

    private static void LoadOptions()
    {
        _options.Clear();
        int indexer = 0;
        _options[++indexer] = "About Us";
        _options[++indexer] = "Browse Movies";
        if (User == null)
        {
            _options[++indexer] = "Register";
            _options[++indexer] = "Log In";
        }
        else
        {
            _options[++indexer] = "Log Out";
            _options[++indexer] = "View Reservations";
        }
        // can expand to give options to admins
    }

    private static void PrintMenuOptions()
    {
        foreach(KeyValuePair<int,string> option in _options)
            Console.WriteLine($"{option.Key}. {option.Value}");
    }

    private static int Options()
    {
        int option;
        do
        {
            int.TryParse(Console.ReadLine(), out option);
        } while (!_options.ContainsKey(option));
        return option;
    }
}