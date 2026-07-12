using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using WinUI3MediaViewer.Core.Enums;
using WinUI3MediaViewer.Helpers;

namespace WinUI3MediaViewer.ViewModels;

/// <summary>
/// 媒体文件项视图模型，用于列表项绑定。
/// 可替代 MediaFileItem 作为数据模型，并添加视图特定行为。
/// </summary>
public class MediaItemViewModel : INotifyPropertyChanged
{
    private string _filePath = string.Empty;
    private string _name = string.Empty;
    private MediaType _type = MediaType.Unsupported;
    private long _fileSize;
    private TimeSpan? _duration;
    private object? _thumbnail;
    private bool _isSelected;
    private bool _isLoadingThumbnail;

    public MediaItemViewModel(string filePath)
    {
        FilePath = filePath;
        Name = System.IO.Path.GetFileName(filePath);
        Type = FileTypeHelper.GetMediaType(filePath);
        FileSize = new System.IO.FileInfo(filePath).Length;

        // 可以在这里触发异步缩略图加载
        // LoadThumbnailAsync();

        OpenInExplorerCommand = new RelayCommand(OpenInExplorer);
    }

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

    /// <summary>
    /// 列表项是否被选中（可用于高亮、播放控制等）
    /// </summary>
    public bool IsSelected
    {
        get => _isSelected;
        set { _isSelected = value; OnPropertyChanged(); }
    }

    public bool IsLoadingThumbnail
    {
        get => _isLoadingThumbnail;
        set { _isLoadingThumbnail = value; OnPropertyChanged(); }
    }

    /// <summary>
    /// 在文件资源管理器中打开该文件所在文件夹并选中文件
    /// </summary>
    public ICommand OpenInExplorerCommand { get; }

    private void OpenInExplorer()
    {
        ShellHelper.OpenInFileExplorer(FilePath);
    }

    /// <summary>
    /// 异步加载缩略图（需结合 IThumbnailService）
    /// </summary>
    public async System.Threading.Tasks.Task LoadThumbnailAsync(Interfaces.IThumbnailService thumbnailService, int size = 64)
    {
        if (Thumbnail != null || IsLoadingThumbnail)
            return;

        IsLoadingThumbnail = true;
        try
        {
            var bitmap = await thumbnailService.GenerateThumbnailAsync(FilePath, size);
            if (bitmap != null)
            {
                // 注意：由于跨线程，可能需要使用 DispatcherHelper
                await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(
                    Windows.UI.Core.CoreDispatcherPriority.Normal,
                    () => { Thumbnail = bitmap; });
            }
        }
        finally
        {
            IsLoadingThumbnail = false;
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string? name = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}

// 简单 RelayCommand 实现（若全局已存在可省略）
public class RelayCommand : ICommand
{
    private readonly Action _execute;
    private readonly Func<bool>? _canExecute;

    public RelayCommand(Action execute, Func<bool>? canExecute = null)
    {
        _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        _canExecute = canExecute;
    }

    public bool CanExecute(object? parameter) => _canExecute?.Invoke() ?? true;
    public void Execute(object? parameter) => _execute();
    public event EventHandler? CanExecuteChanged;
}
