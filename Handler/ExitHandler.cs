using HomeWork.Interface;

namespace HomeWork.Handler;

public class ExitHandler : IHandler
{
    /// <inheritdoc/>
    public void Run()
    {
        Environment.Exit(1);
    }
}
