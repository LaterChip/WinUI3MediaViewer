using Microsoft.UI.Xaml;

namespace WinUI3MediaViewer;

public partial class App : Application
{
    private Window? m_window;

    public App()
    {
        this.InitializeComponent();
    }

    protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
    {
        m_window = new Views.MainWindow();
        m_window.Activate();
    }
}
