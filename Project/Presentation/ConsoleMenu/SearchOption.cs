public class SearchOption : OptionBase
{
    
    public override string Label {get => $"Search: {Query}"; set;}
    public override bool IsSelectable => true;
    public override bool IsSearchable => true;
    public override bool IsScrollable => false;
    public string Query {get;private set;} = "";

    public void SetQuery(ConsoleKeyInfo keyInfo)
    {
        if (keyInfo.Key == ConsoleKey.Backspace)
        {   
            if (Query.Length > 0)
                Query = Query[..^1];
        }
        else
        {
            Query += keyInfo.KeyChar;
        }
    }
}

