class ConsoleMenu<T> : IConsoleMenu<T>
{

    public int SelectOption(List<T> options)
    {
        if (options == null || options.Count() == 0)
        {
            return -1;
        }

        int selectedIndex = 0;


        while (true)
        {
            Console.Clear();
            for (int i = 0; i < options.Count; i++)
            {
                if (i == selectedIndex)
                {
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine($"> {options[i]}");
                    Console.ResetColor();
                }



                else
                {
                    Console.WriteLine($"  {options[i]}");

                }
            }

            ConsoleKey key = Console.ReadKey(true).Key;

            switch (key)
            {
                case ConsoleKey.UpArrow:
                    selectedIndex =
                    (selectedIndex - 1 + options.Count) % options.Count;
                    break;

                case ConsoleKey.DownArrow:
                    selectedIndex =
                        (selectedIndex + 1) % options.Count;
                    break;

                case ConsoleKey.Enter:
                    return selectedIndex;

                case ConsoleKey.Q :
                    // TODO: this is not an error
                    return -1;
            }


        }

    }


}