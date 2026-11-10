public class SearchableMenu<T> : ScrollableMenu<T>
{
    protected override int _amountOfPages {get => (int)Math.Ceiling((double)_queriedList.Count/MaxItemsPerPage);}
    protected override int _scrollableOptionsOnScreen {get => _amountOfPages == _currentPage ? _queriedList.Count - (_amountOfPages-1)*10 : 10;}

    protected SearchOption<T> _searchOption = new();
    protected List<OptionBase<T>> _queriedList = new();

    protected override void QueryList()
    {
        _queriedList.Clear();
        string query = _searchOption.Query;
        foreach(var scrollOption in _scrollOptions)
        {
            if (scrollOption.Label.ToLower().Contains(query.ToLower()))
            {
                _queriedList.Add(scrollOption);
            }
        }
        _currentPage = _queriedList.Count == 0 ? 0 : 1;
        
        FillOptionsList();
        _selectedIndex = _displayOptions.FindIndex(o => o is SearchOption<T>);
        if (_selectedIndex < 0) _selectedIndex = 1;
    }

    public override T SelectOption()
    {
        QueryList();
        return base.SelectOption();
    }

    protected override void FillOptionsList()
    {
        _displayOptions.Clear();
        _displayOptions.Add(new InfoOption<T>($"Page ({_currentPage}/{_amountOfPages})"));
        if (_queriedList.Count == 0) 
            _displayOptions.Add(new InfoOption<T>("Nothing found!"));
        else
        {
            for (int i = (_currentPage-1)*MaxItemsPerPage; i < (_currentPage-1)*10+_scrollableOptionsOnScreen; i++)
                _displayOptions.Add(_queriedList[i]);
            if (_currentPage > 1)
            {
                _displayOptions.Add(new InfoOption<T>(""));
                _displayOptions.Add(new SelectOption<T>("↑ Scroll Up ↑", default));
            }
            if (_currentPage < _amountOfPages)
            {
                _displayOptions.Add(new InfoOption<T>(""));
                _displayOptions.Add(new SelectOption<T>("↓ Scroll Down ↓", default));
            }
        }
        _displayOptions.Add(new InfoOption<T>(""));
        _displayOptions.Add(_searchOption);
        _displayOptions.Add(new InfoOption<T>(""));
        foreach(var option in _options)
            _displayOptions.Add(option);
    }
}