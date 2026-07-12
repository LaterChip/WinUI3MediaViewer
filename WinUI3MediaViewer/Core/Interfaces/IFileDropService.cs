using System.Collections.Generic;

namespace WinUI3MediaViewer.Core.Interfaces;

public interface IFileDropService
{
    /// <summary>
    /// 从拖放数据中提取所有支持的媒体文件路径。
    /// </summary>
    IEnumerable<string> GetSupportedFiles(IEnumerable<string> filePaths, bool includeSubfolders = true);
}
