using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using WinUI3MediaViewer.Core.Enums;

namespace WinUI3MediaViewer.Models;

public class MediaFileItem : INotifyPropertyChanged
{
    private string _filePath = string.Empty;
    private string _name = string.Empty;
    private MediaType _type = MediaType.Unsupported;
    private long _fileSize;
    private TimeSpan? _duration;
    private object? _thumbnail;

    public string FilePath
    {
        get => _filePath;
        set { _filePath = value; OnPropertyChanged(); }
    }

    public string Name
    {
        get => _name;
        set { _name = value; OnPropertyChanged(); }
    }

    public MediaType Type
    {
        get => _type;
        set { _type = value; OnPropertyChanged(); }
    }

    public long FileSize
    {
        get => _fileSize;
        set { _fileSize = value; OnPropertyChanged(); }
    }

    public TimeSpan? Duration
    {
        get => _duration;
        set { _duration = value; OnPropertyChanged(); }
    }

    public object? Thumbnail
    {
        get => _thumbnail;
        set { _thumbnail = value; OnPropertyChanged(); }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string? name = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
