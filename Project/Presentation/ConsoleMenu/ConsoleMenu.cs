public class ConsoleMenu<T>

{
    protected int _selectedIndex;
    protected List<OptionBase<T>> _options = new();
    protected List<OptionBase<T>> _displayOptions = new();
    public virtual T SelectOption()
    {
        FillOptionsList();
        _selectedIndex = 0;
        foreach(var option in _displayOptions)
            if (!option.IsSelectable)
                _selectedIndex++;
            else
                break;
        
        while (true)
        {
            PrintAll();
            ConsoleKeyInfo key = Console.ReadKey(true);
            if (InputKey(key)) 
            {
                if (CheckOption())
                    return _displayOptions[_selectedIndex].ReturnInfo;
            }
        }

    }

    protected virtual bool CheckOption() => true;
    protected virtual void QueryList(){}

    protected bool InputKey(ConsoleKeyInfo keyInfo)
    {
        
        if (_displayOptions[_selectedIndex] is SearchOption<T> searchOption && keyInfo.Key != ConsoleKey.UpArrow && keyInfo.Key != ConsoleKey.DownArrow)
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
                    do
                    {
                    _selectedIndex = (_selectedIndex - 1 + _displayOptions.Count) % _displayOptions.Count;
                    if (_displayOptions[_selectedIndex] == null) {_selectedIndex--;}
                    } while (!_displayOptions[_selectedIndex].IsSelectable);
                    return false;

                case ConsoleKey.DownArrow:
                    do
                    {
                    _selectedIndex = (_selectedIndex + 1) % _displayOptions.Count;
                    if (_displayOptions[_selectedIndex] == null) {_selectedIndex++;}
                    } while (!_displayOptions[_selectedIndex].IsSelectable);
                    return false;

                case ConsoleKey.Enter:
                    return true;
                default:
                    return false;
            }
        }
    }

    protected void PrintAll()
    {
        Console.Clear();
        for (int i = 0; i < _displayOptions.Count; i++)
        {
            _displayOptions[i].PrintOut(_selectedIndex == i);
        }
    }

    public void AddOption(OptionBase<T> option)
    {
        _options.Add(option);
    }

    public void ResetOptions()
    {
        _options.Clear();
    }

    protected virtual void FillOptionsList()
    {
        _displayOptions.Clear();
        _displayOptions.AddRange(_options);
    }
}

public class ConsoleMenu : ConsoleMenu<string>
{
    public void PopulateOptions(List<string> options)
    {
        foreach (var option in options)
        {
            _options.Add(new SelectOption(option));
        }
    }
}