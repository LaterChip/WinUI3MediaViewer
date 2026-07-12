using System.Threading.Tasks;
using Microsoft.UI.Xaml.Media.Imaging;

namespace WinUI3MediaViewer.Core.Interfaces;

public interface IThumbnailService
{
    /// <summary>
    /// 异步生成缩略图（图片直接缩放，视频提取首帧）。
    /// </summary>
    Task<BitmapImage?> GenerateThumbnailAsync(string filePath, int size = 64);
}
