using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Data;
using WinUI3MediaViewer.Core.Enums;

namespace WinUI3MediaViewer.Converters;

public class MediaTypeToVisibilityConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (value is MediaType type && parameter is string targetTypeStr)
        {
            var expected = targetTypeStr.ToLowerInvariant() switch
            {
                "image" => MediaType.Image,
                "video" => MediaType.Video,
                "audio" => MediaType.Audio,
                _ => MediaType.Unsupported
            };
            return type == expected ? Visibility.Visible : Visibility.Collapsed;
        }
        return Visibility.Collapsed;
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}
