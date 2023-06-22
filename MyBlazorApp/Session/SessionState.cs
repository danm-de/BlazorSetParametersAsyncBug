namespace MyBlazorApp.Session;

/// <summary>
/// Demo class - used for testing only!
/// </summary>
public class SessionState
{
    public string? LastSearch { get; set; }

    public override string ToString() => $"{nameof(LastSearch)}: {LastSearch}";
}
