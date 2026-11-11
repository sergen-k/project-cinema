public class SearchOption<T> : OptionBase<T>
{
    
    public override string Label {get => $"Search: {Query}"; set;}
    public override T ReturnInfo {get => default! ; set;}
    public override bool IsSelectable {get;} = true;
    public override bool IsSearchable {get;} = true;
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