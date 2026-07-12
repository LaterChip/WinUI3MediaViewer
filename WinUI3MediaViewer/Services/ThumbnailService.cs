using System;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Media.Imaging;
using WinUI3MediaViewer.Core.Enums;
using WinUI3MediaViewer.Core.Interfaces;
using WinUI3MediaViewer.Helpers;

namespace WinUI3MediaViewer.Services;

public class ThumbnailService : IThumbnailService
{
    public async Task<BitmapImage?> GenerateThumbnailAsync(string filePath, int size = 64)
    {
        var type = FileTypeHelper.GetMediaType(filePath);

        if (type == MediaType.Image)
        {
            // 直接加载图片并缩小
            try
            {
                var bitmap = new BitmapImage();
                await bitmap.SetSourceAsync(Windows.Storage.Streams.RandomAccessStreamReference.CreateFromUri(new Uri(filePath)).OpenReadAsync());
                // 注意：若要生成缩略图尺寸，可以设置 DecodePixelWidth/Height
                bitmap.DecodePixelWidth = size;
                bitmap.DecodePixelHeight = size;
                return bitmap;
            }
            catch
            {
                return null;
            }
        }
        else if (type == MediaType.Video)
        {
            // 使用 Windows 外壳提取缩略图（需要引用 Microsoft.WindowsAPICodePack 或使用 Shell API）
            // 这里简单返回一个占位图标，实际项目可集成 ShellThumbnail
            // 此处演示：直接返回 null，显示默认图标。
            return null;
        }
        else
        {
            return null;
        }
    }
}
