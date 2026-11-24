public class ConsoleMenu

{
    protected int _selectedIndex;
    protected List<OptionBase> _options = new();
    protected List<OptionBase> _displayedOptions = new();
    public bool IsMainMenu;
    public virtual int SelectOption()
    {
        FillOptionsList();
        _selectedIndex = _displayedOptions.FindIndex(o => o.IsSelectable);

        while (true)
        {
            PrintAll();
            ConsoleKeyInfo key = Console.ReadKey(true);
            if (InputKey(key))
            {
                if (CheckOption())
                    if (key.Key == ConsoleKey.Escape || _selectedIndex == _displayedOptions.Count)
                    {
                        return -1;
                    }
                    else if (_displayedOptions[_selectedIndex] is SelectOption selectedOpt)
                    {
                        return _options.IndexOf(selectedOpt);
                    }
            }
        }

    }

    protected virtual bool CheckOption() => true;
    protected virtual void QueryList() { }

    protected bool InputKey(ConsoleKeyInfo keyInfo)
    {

        if (_displayedOptions[_selectedIndex] is SearchOption searchOption
            && keyInfo.Key != ConsoleKey.UpArrow
            && keyInfo.Key != ConsoleKey.DownArrow
            && keyInfo.Key != ConsoleKey.Escape)
        {
            if (keyInfo.Key != ConsoleKey.Enter)
            {
                searchOption.SetQuery(keyInfo);
                QueryList();
            }
            return false;
        }
        else
        {
            switch (keyInfo.Key)
            {
                case ConsoleKey.UpArrow:
                    MoveSelection(-1);
                    return false;

                case ConsoleKey.DownArrow:
                    MoveSelection(+1);
                    return false;

                case ConsoleKey.Enter:
                case ConsoleKey.Spacebar:
                case ConsoleKey.Escape:
                    return true;
                default:
                    return false;
            }
        }
    }
    private void MoveSelection(int step)
    {
        int n = _displayedOptions.Count;
        for (int i = 0; i < n; i++)
        {
            _selectedIndex = (_selectedIndex + step + n) % n;
            if (_displayedOptions[_selectedIndex] is { IsSelectable: true })
                return;
        }
    }

    protected void PrintAll()
    {
        Console.Clear();
        for (int i = 0; i < _displayedOptions.Count; i++)
        {
            _displayedOptions[i].PrintOut(_selectedIndex == i);
        }
    }

    public void SetOptions(List<OptionBase> options)
    {
        _options.Clear();
        _options.AddRange(options);
    }

    protected virtual void FillOptionsList()
    {
        _displayedOptions.Clear();
        _displayedOptions.AddRange(_options);
        if (IsMainMenu)
        {
            _displayedOptions.Add(new SelectOption("Exit"));
        }
        else
        {
            _displayedOptions.Add(new SelectOption("Return"));
        }
    }
}