using System.Diagnostics;
using System.IO;

namespace WinUI3MediaViewer.Helpers;

public static class ShellHelper
{
    public static void OpenInFileExplorer(string filePath)
    {
        var folder = Path.GetDirectoryName(filePath);
        if (!string.IsNullOrEmpty(folder) && Directory.Exists(folder))
        {
            Process.Start("explorer.exe", $"/select,\"{filePath}\"");
        }
    }
}
