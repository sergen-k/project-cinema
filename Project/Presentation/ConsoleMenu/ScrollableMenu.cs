
public class ScrollableMenu : ConsoleMenu
{
    public const int MaxItemsPerPage = 10;
    protected virtual int _amountOfPages{get => Math.Max(1, (int)Math.Ceiling((double)_scrollOptions.Count / MaxItemsPerPage));}
    protected int _currentPage { get; set => field = Math.Clamp(value, 1, Math.Max(1, _amountOfPages)); } = 1;
    protected virtual int _scrollableOptionsOnScreen { get => _amountOfPages == _currentPage ? _scrollOptions.Count - (_currentPage - 1) * MaxItemsPerPage : MaxItemsPerPage; }
    protected List<ScrollOption> _scrollOptions = [];

    protected override bool CheckOption()
    {
        if (_displayedOptions[_selectedIndex].Label == "↑ Scroll Up ↑")
        {
            _currentPage--;
            FillOptionsList();
            _selectedIndex = _displayedOptions.FindIndex(o => o.Label == "↑ Scroll Up ↑");
            if (_selectedIndex < 0) _selectedIndex = 1;

            return false;
        }
        else if (_displayedOptions[_selectedIndex].Label == "↓ Scroll Down ↓")
        {
            _currentPage++;
            FillOptionsList();
            _selectedIndex = _displayedOptions.FindIndex(o => o.Label == "↓ Scroll Down ↓");
            if (_selectedIndex < 0) _selectedIndex = 1;
            return false;
        }

        return true;
    }

    protected override void FillOptionsList()
    {
        _displayedOptions.Clear();
        _displayedOptions.Add(new InfoOption($"Page ({_currentPage}/{_amountOfPages})"));
        _scrollOptions = _options.OfType<ScrollOption>().ToList();

        if (_scrollOptions.Count == 0)
            _displayedOptions.Add(new InfoOption("Nothing found!"));
        else
        {
            for (int i = (_currentPage - 1) * MaxItemsPerPage; i < (_currentPage - 1) * MaxItemsPerPage + _scrollableOptionsOnScreen; i++)
                _displayedOptions.Add(_scrollOptions[i]);

            if (_currentPage > 1)
            {
                _displayedOptions.Add(new InfoOption(""));
                _displayedOptions.Add(new SelectOption("↑ Scroll Up ↑"));
            }

            if (_currentPage < _amountOfPages)
            {
                _displayedOptions.Add(new InfoOption(""));
                _displayedOptions.Add(new SelectOption("↓ Scroll Down ↓"));
            }
        }

        _displayedOptions.Add(new InfoOption(""));
        _displayedOptions.AddRange(_options.Where(o => o is not ScrollOption));
        _displayedOptions.Add(new SelectOption("Return"));
    }
}
