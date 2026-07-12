using System.Collections.Generic;
using System.IO;
using WinUI3MediaViewer.Core.Interfaces;
using WinUI3MediaViewer.Helpers;

namespace WinUI3MediaViewer.Services;

public class FileDropService : IFileDropService
{
    public IEnumerable<string> GetSupportedFiles(IEnumerable<string> filePaths, bool includeSubfolders = true)
    {
        var result = new List<string>();

        foreach (var path in filePaths)
        {
            if (File.Exists(path))
            {
                if (FileTypeHelper.IsSupported(path))
                    result.Add(path);
            }
            else if (Directory.Exists(path))
            {
                var searchOption = includeSubfolders ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
                foreach (var file in Directory.EnumerateFiles(path, "*.*", searchOption))
                {
                    if (FileTypeHelper.IsSupported(file))
                        result.Add(file);
                }
            }
        }

        return result;
    }
}
