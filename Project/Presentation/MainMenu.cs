static class MainMenu
{
    const string Banner = """
        ══════════════════════════════════════════════════════════════════════════════

        ██████╗  ██████╗ ████████╗████████╗███████╗██████╗ ██████╗  █████╗ ███╗   ███╗
        ██╔══██╗██╔═══██╗╚══██╔══╝╚══██╔══╝██╔════╝██╔══██╗██╔══██╗██╔══██╗████╗ ████║
        ██████╔╝██║   ██║   ██║      ██║   █████╗  ██████╔╝██║  ██║███████║██╔████╔██║
        ██╔══██╗██║   ██║   ██║      ██║   ██╔══╝  ██╔══██╗██║  ██║██╔══██║██║╚██╔╝██║
        ██║  ██║╚██████╔╝   ██║      ██║   ███████╗██║  ██║██████╔╝██║  ██║██║ ╚═╝ ██║
        ╚═╝  ╚═╝ ╚═════╝    ╚═╝      ╚═╝   ╚══════╝╚═╝  ╚═╝╚═════╝ ╚═╝  ╚═╝╚═╝     ╚═╝

                     ██████╗██╗███╗   ██╗███████╗███╗   ███╗ █████╗
                    ██╔════╝██║████╗  ██║██╔════╝████╗ ████║██╔══██╗
                    ██║     ██║██╔██╗ ██║█████╗  ██╔████╔██║███████║
                    ██║     ██║██║╚██╗██║██╔══╝  ██║╚██╔╝██║██╔══██║
                    ╚██████╗██║██║ ╚████║███████╗██║ ╚═╝ ██║██║  ██║
                    ╚═════╝╚═╝╚═╝  ╚═══╝╚══════╝╚═╝     ╚═╝╚═╝  ╚═╝

        ══════════════════════════════════════════════════════════════════════════════
        
        """;

    private static List<OptionBase> _options = new();

    public static void Start()
    {
        
        while (true)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.Clear();
            int chosenOption = Options();
            if (chosenOption == -1) {break;}
            switch (_options[chosenOption].Label)
            {
                case "Browse Movies":
                    BrowseMovies.Start();
                    break;

                case "Register":
                    Register.Start();
                    break;

                case "Log In":
                    Console.CursorVisible = true;
                    UserLogin.Start();
                    Console.CursorVisible = false;
                    break;

                case "Log Out":
                    UserLogic.LogOut();
                    break;

                case "View Reservations":
                    Console.CursorVisible = true;
                    ViewReservation viewReservation = new();
                    viewReservation.Start();
                    Console.CursorVisible = false;
                    break;


                case "Finances":
                    Finances.Start();
                    Console.WriteLine("hello");
                   break;

                case "Edit Profile":
                    Console.CursorVisible = true;
                    EditProfile.Start();
                    Console.CursorVisible = false;
                    break;

                case "Add Movie":
                    AdminAddMovie.Start();
                    break;

                case ">>> ENTER ADMIN PANEL <<<":
                    AdminPanel.Start();
                    break;

                case "Auto Log (Temporary)":
                    UserAccess userAccess = new();
                    UserLogic.ChangeAccount(userAccess.SelectAllById(1));
                    break;

                case null:
                    break;
            }
            if (chosenOption == null)
            {
                break;
            }
        }
    }

    private static int Options()
    {

        ConsoleMenu menu = new();
        _options.Clear();
        menu.IsMainMenu = true;
        _options.Add(new InfoOption(Banner));

        if (UserLogic.CurrentAccount?.Role == "Admin")
        {
            _options.Add(new SelectOption(">>> ENTER ADMIN PANEL <<<"));
        }



        // maak een finance panel aan die alleen iets aanroept als de role FinanceWorker is of een admin. 

        if(UserLogic.CurrentAccount?.Role == "Admin" || UserLogic.CurrentAccount?.Role == "FinanceWorker")
        {
            _options.Add(new SelectOption("Finances"));
        }

        _options.Add(new SelectOption("Browse Movies"));
        if (UserLogic.CurrentAccount == null)
        {
            _options.Add(new SelectOption("Log In"));
            _options.Add(new SelectOption("Register"));
        }
        else
        {
            _options.Add(new SelectOption("Log Out"));
            _options.Add(new SelectOption("View Reservations"));
            _options.Add(new SelectOption("Edit Profile"));
        }

        _options.Add(new SelectOption("About Us"));

        if (!UserLogic.IsLoggedIn())
        {
            _options.Add(new SelectOption("Auto Log (Temporary)"));
        }

        menu.SetOptions(_options);
        return menu.SelectOption();
    }
}