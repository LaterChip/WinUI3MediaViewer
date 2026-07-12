using System;
using Microsoft.UI.Xaml.Data;

namespace WinUI3MediaViewer.Converters;

public class FileSizeToFriendlyStringConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (value is long size)
        {
            string[] suffixes = { "B", "KB", "MB", "GB", "TB" };
            int index = 0;
            double dSize = size;
            while (dSize >= 1024 && index < suffixes.Length - 1)
            {
                dSize /= 1024;
                index++;
            }
            return $"{dSize:0.##} {suffixes[index]}";
        }
        return "0 B";
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}
