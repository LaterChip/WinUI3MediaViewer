using System.IO;
using WinUI3MediaViewer.Core.Enums;

namespace WinUI3MediaViewer.Helpers;

public static class FileTypeHelper
{
    public static MediaType GetMediaType(string filePath)
    {
        var ext = Path.GetExtension(filePath).ToLowerInvariant();
        return ext switch
        {
            ".jpg" or ".jpeg" or ".png" or ".bmp" or ".gif" or ".ico" or ".tiff" or ".wdp" => MediaType.Image,
            ".mp4" or ".avi" or ".mkv" or ".mov" or ".wmv" or ".flv" or ".webm" => MediaType.Video,
            ".mp3" or ".wav" or ".flac" or ".aac" or ".ogg" or ".wma" => MediaType.Audio,
            _ => MediaType.Unsupported
        };
    }

    public static bool IsSupported(string filePath) => GetMediaType(filePath) != MediaType.Unsupported;
}
