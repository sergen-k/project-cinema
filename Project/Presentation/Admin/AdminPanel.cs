static class AdminPanel
{
    const string Banner = """
        ══════════════════════════════════════════════════════════════════════════════

         █████╗ ██████╗ ███╗   ███╗██╗███╗   ██╗    ██████╗  █████╗ ███╗   ██╗███████╗██╗
        ██╔══██╗██╔══██╗████╗ ████║██║████╗  ██║    ██╔══██╗██╔══██╗████╗  ██║██╔════╝██║
        ███████║██║  ██║██╔████╔██║██║██╔██╗ ██║    ██████╔╝███████║██╔██╗ ██║█████╗  ██║
        ██╔══██║██║  ██║██║╚██╔╝██║██║██║╚██╗██║    ██╔═══╝ ██╔══██║██║╚██╗██║██╔══╝  ██║
        ██║  ██║██████╔╝██║ ╚═╝ ██║██║██║ ╚████║    ██║     ██║  ██║██║ ╚████║███████╗███████╗
        ╚═╝  ╚═╝╚═════╝ ╚═╝     ╚═╝╚═╝╚═╝  ╚═══╝    ╚═╝     ╚═╝  ╚═╝╚═╝  ╚═══╝╚══════╝╚══════╝

        ══════════════════════════════════════════════════════════════════════════════
        
        """;
    private static List<OptionBase> _options = new();
    public static void Start()
    {
        while (true)
        {

            int chosenOption = Options();
            if (chosenOption == -1) {break;}

            Console.Clear();
            switch (_options[chosenOption].Label)
            {
                case "Change User Settings":
                    AdminChangeUserSettings.Start();
                    break;

                case "Add Movie":
                    AdminAddMovie.Start();
                    break;
            }
        }
    }

    private static int Options()
    {
        ConsoleMenu menu = new();
        _options.Clear();
        _options.Add(new InfoOption(Banner));
        _options.Add(new SelectOption("Change User Settings"));
        _options.Add(new SelectOption("Add Movie"));
        menu.SetOptions(_options);
        return menu.SelectOption();
    }
}