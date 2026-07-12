using System;
using Microsoft.UI.Dispatching;

namespace WinUI3MediaViewer.Helpers;

public static class DispatcherHelper
{
    private static DispatcherQueue? _dispatcher;

    public static void Initialize(DispatcherQueue dispatcher)
    {
        _dispatcher = dispatcher;
    }

    public static void RunOnUIThread(Action action)
    {
        if (_dispatcher is null)
            throw new InvalidOperationException("Dispatcher not initialized.");

        if (_dispatcher.HasThreadAccess)
            action();
        else
            _dispatcher.TryEnqueue(() => action());
    }
}
