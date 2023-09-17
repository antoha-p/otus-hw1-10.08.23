using HomeWork.Interface;

namespace HomeWork.Menu;

public class MenuItem : IMenuItem
{
    /// <inheritdoc/>
    public string Title { get; set; }

    /// <inheritdoc/>
    public IHandler Handler { get; }

    public MenuItem(string title, IHandler handler)
    {
        Title = title;
        Handler = handler;
    }
}
