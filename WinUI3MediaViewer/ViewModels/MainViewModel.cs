using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using WinUI3MediaViewer.Core.Interfaces;
using WinUI3MediaViewer.Models;

namespace WinUI3MediaViewer.ViewModels;

public class MainViewModel : INotifyPropertyChanged
{
    private readonly IFileDropService _fileDropService;
    private readonly IThumbnailService _thumbnailService;

    private MediaFileItem? _selectedItem;
    private int _selectedIndex;
    private bool _isLoading;

    public ObservableCollection<MediaFileItem> MediaItems { get; } = new();

    public MediaFileItem? SelectedItem
    {
        get => _selectedItem;
        set
        {
            if (_selectedItem != value)
            {
                _selectedItem = value;
                OnPropertyChanged();
                SelectedIndex = MediaItems.IndexOf(value!);
                // 触发播放/预览更新
            }
        }
    }

    public int SelectedIndex
    {
        get => _selectedIndex;
        set
        {
            if (_selectedIndex != value)
            {
                _selectedIndex = value;
                OnPropertyChanged();
                if (value >= 0 && value < MediaItems.Count)
                    SelectedItem = MediaItems[value];
            }
        }
    }

    public bool IsLoading
    {
        get => _isLoading;
        set { _isLoading = value; OnPropertyChanged(); }
    }

    public ICommand ClearCommand { get; }
    public ICommand OpenFilesCommand { get; }

    public MainViewModel(IFileDropService fileDropService, IThumbnailService thumbnailService)
    {
        _fileDropService = fileDropService;
        _thumbnailService = thumbnailService;

        ClearCommand = new RelayCommand(ClearFiles);
        OpenFilesCommand = new RelayCommand(OpenFiles);
    }

    public void AddFiles(IEnumerable<string> filePaths)
    {
        IsLoading = true;
        try
        {
            var supported = _fileDropService.GetSupportedFiles(filePaths);
            foreach (var path in supported)
            {
                var item = new MediaFileItem
                {
                    FilePath = path,
                    Name = System.IO.Path.GetFileName(path),
                    Type = Helpers.FileTypeHelper.GetMediaType(path),
                    FileSize = new System.IO.FileInfo(path).Length
                };
                MediaItems.Add(item);
            }

            if (MediaItems.Count > 0 && SelectedItem == null)
                SelectedItem = MediaItems.First();
        }
        finally
        {
            IsLoading = false;
        }
    }

    public void ClearFiles()
    {
        MediaItems.Clear();
        SelectedItem = null;
    }

    private void OpenFiles()
    {
        // 调用文件选择器，通过 MainWindow 实现
        // 这里使用事件通知 View 打开文件对话框
        RequestOpenFile?.Invoke(this, EventArgs.Empty);
    }

    public event EventHandler? RequestOpenFile;

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string? name = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}

// 简单的 RelayCommand 实现
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
