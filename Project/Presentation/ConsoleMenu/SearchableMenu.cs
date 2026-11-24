public class SearchableMenu : ScrollableMenu
{
    protected override int _amountOfPages {get => (int)Math.Ceiling((double)_queriedList.Count/MaxItemsPerPage);}
    protected override int _scrollableOptionsOnScreen {get => _amountOfPages == _currentPage ? _queriedList.Count - (_amountOfPages-1)*10 : 10;}

    protected SearchOption _searchOption = new();
    protected List<OptionBase> _queriedList = new();

    protected override void QueryList()
    {
        _scrollOptions = _options.OfType<ScrollOption>().ToList();

        _queriedList.Clear();
        string query = _searchOption.Query;

        foreach (var option in _scrollOptions)
        {
            if (option.Label.Contains(query, StringComparison.OrdinalIgnoreCase))
                _queriedList.Add(option);
        }

        _currentPage = 1;

        FillOptionsList();
        _selectedIndex = _displayedOptions.IndexOf(_searchOption);
    }

    public override int SelectOption()
    {
        QueryList();
        return base.SelectOption();
    }

    protected override void FillOptionsList()
    {
        _displayedOptions.Clear();
        _displayedOptions.Add(new InfoOption($"Page ({_currentPage}/{_amountOfPages})"));
        _scrollOptions = _options.OfType<ScrollOption>().ToList();
        // search field
        _displayedOptions.Add(new InfoOption(""));
        _displayedOptions.Add(_searchOption);
        _displayedOptions.Add(new InfoOption(""));

        if (_queriedList.Count == 0) 
        {
            _displayedOptions.Add(new InfoOption("Nothing found!"));
            _displayedOptions.Add(new InfoOption(""));
        }
        
        else
        {
            for (int i = (_currentPage-1)*MaxItemsPerPage; i < (_currentPage-1)*10+_scrollableOptionsOnScreen; i++)
                _displayedOptions.Add(_queriedList[i]);
            _displayedOptions.Add(new InfoOption(""));
            if (_currentPage > 1)
            {
                _displayedOptions.Add(new SelectOption("↑ Scroll Up ↑"));
                _displayedOptions.Add(new InfoOption(""));
            }
            if (_currentPage < _amountOfPages)
            {
                _displayedOptions.Add(new SelectOption("↓ Scroll Down ↓"));
                _displayedOptions.Add(new InfoOption(""));
            }
        }
        _displayedOptions.AddRange(_options.Where(o => o is not ScrollOption));   
        _displayedOptions.Add(new SelectOption("Return"));
        Console.WriteLine(_displayedOptions.IndexOf(_searchOption));
    }
}