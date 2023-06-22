namespace MyBlazorApp.Services;

public class CallHistory
{
    public class Item
    {
        public required int Order { get; set; }
        public required DateTimeOffset Timestamp { get; init; }
        public required IReadOnlyDictionary<string, object> Parameters { get; init; }
    }

    private readonly List<Item> _history = new();

    public void Add(IReadOnlyDictionary<string, object> parameters) =>
        _history.Add(new Item
        {
            Order = _history.Count + 1,
            Timestamp = DateTimeOffset.Now,
            Parameters = parameters
        });

    public IReadOnlyList<Item> GetAll() => _history.OrderBy(x => x.Order).ToList();

    public void Clear() => _history.Clear();
}
