using Microsoft.AspNetCore.Components;
using MyBlazorApp.Services;
using MyBlazorApp.Session;

namespace MyBlazorApp.Pages;

public partial class Index
{
    private IReadOnlyList<CallHistory.Item> _history = new List<CallHistory.Item>();

    [CascadingParameter]
    public SessionState? SessionState { get; set; }
    
    [Parameter, SupplyParameterFromQuery(Name = "search")]
    public string? Search { get; set; }

    [Parameter, SupplyParameterFromQuery(Name = "page")]
    public int? Page { get; set; }

    [Parameter, SupplyParameterFromQuery(Name = "pageSize")]
    public int? PageSize { get; set; }

    [Inject]
    private CallHistory CallHistory { get; set; } = null!;

    [Inject]
    private NavigationManager NavigationManager { get; set; } = null!;

    public override Task SetParametersAsync(ParameterView parameters)
    {
        CallHistory.Add(parameters.ToDictionary());

        LoadHistory();

        return base.SetParametersAsync(parameters);
    }

    protected override void OnInitialized() => LoadHistory();

    private void OnClearHistory()
    {
        CallHistory.Clear();
        _history = CallHistory.GetAll();
    }

    private void OnSubmit()
    {
        if (SessionState != null) SessionState.LastSearch = Search;
        NavigateTo(Search, Page, PageSize);
    }

    private void OnPageChanged(ChangeEventArgs args) =>
        NavigateTo(Search, int.TryParse(args.Value as string, out var page) ? page : null, PageSize);

    private void OnPageSizeChanged(ChangeEventArgs args) =>
        NavigateTo(Search, Page, int.TryParse(args.Value as string, out var pageSize) ? pageSize : null);

    private void NavigateTo(string? search, int? page, int? pageSize)
    {
        var uri = NavigationManager.GetUriWithQueryParameters(new Dictionary<string, object?>
        {
            { "page", page },
            { "pageSize", pageSize },
            { "search", string.IsNullOrWhiteSpace(search) ? default : search },
        });

        NavigationManager.NavigateTo(uri);
    }

    private void LoadHistory() => _history = CallHistory.GetAll();
}
