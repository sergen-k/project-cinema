using System.ComponentModel;

public class ScrollableMenu<T> : ConsoleMenu<T>
{
    public const int MaxItemsPerPage = 10;
    protected virtual int _amountOfPages {get => (int)Math.Ceiling((double)_scrollOptions.Count/MaxItemsPerPage);}
    protected int _currentPage {get; set => field = Math.Clamp(value, 1, Math.Max(1, _amountOfPages));} = 1; 
    protected virtual int _scrollableOptionsOnScreen {get => _amountOfPages == _currentPage ? _scrollOptions.Count - (_amountOfPages-1)*10 : 10;}

    protected List<SelectOption> _scrollableButtons {get;} = [new("↑ Scroll Up ↑"), new("↓ Scroll Down ↓")];
    protected List<OptionBase<T>> _scrollOptions = [];
    

    public void AddScrollOption(OptionBase<T> option)
    {
        _scrollOptions.Add(option);
    }
    
    protected override bool CheckOption()
    {
        if (_displayOptions[_selectedIndex].Label == "↑ Scroll Up ↑")
        {
            _currentPage--;
            FillOptionsList();
            _selectedIndex = _displayOptions.FindIndex(o => o.Label == "↑ Scroll Up ↑");
            if (_selectedIndex < 0) _selectedIndex = 1;
            
            return false;
        }
        else if (_displayOptions[_selectedIndex].Label == "↓ Scroll Down ↓")
        {
            _currentPage++;
            FillOptionsList();
            _selectedIndex = _displayOptions.FindIndex(o => o.Label == "↓ Scroll Down ↓");
            if (_selectedIndex < 0) _selectedIndex = 1;
            return false;
        }
        return true;
        
    }

    protected override void FillOptionsList()
    {
        _displayOptions.Clear();
        _displayOptions.Add(new InfoOption<T>($"Page ({_currentPage}/{_amountOfPages})"));
        if (_scrollOptions.Count == 0) 
            _displayOptions.Add(new InfoOption<T>("Nothing found!"));
        else
        {
            for (int i = (_currentPage-1)*MaxItemsPerPage; i < (_currentPage-1)*10+_scrollableOptionsOnScreen; i++)
                _displayOptions.Add(_scrollOptions[i]);
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
        foreach(var option in _options)
            _displayOptions.Add(option);
    }
}