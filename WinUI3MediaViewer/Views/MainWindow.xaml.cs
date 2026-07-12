using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using WinUI3MediaViewer.Core.Interfaces;
using WinUI3MediaViewer.Services;
using WinUI3MediaViewer.ViewModels;
using Windows.Storage.Pickers;
using WinRT.Interop;

namespace WinUI3MediaViewer.Views;

public sealed partial class MainWindow : Window
{
    public MainViewModel ViewModel { get; }

    public MainWindow()
    {
        this.InitializeComponent();

        // 初始化服务
        var fileDropService = new FileDropService();
        var thumbnailService = new ThumbnailService();

        ViewModel = new MainViewModel(fileDropService, thumbnailService);
        ViewModel.RequestOpenFile += (s, e) => OpenFilePicker();

        // 绑定数据上下文
        this.Content = new Grid(); // 实际上 XAML 已经设置了根元素，这里只是为了说明
        // 但由于 XAML 定义，我们不需要再次设置 Content。
        // 只需确保 DataContext 有效：
        this.Closed += (s, e) => { /* 释放资源 */ };
    }

    private void OnDragOver(object sender, DragEventArgs e)
    {
        e.Handled = true;
        e.AcceptedOperation = Windows.ApplicationModel.DataTransfer.DataPackageOperation.Copy;
    }

    private async void OnDrop(object sender, DragEventArgs e)
    {
        if (e.DataView.Contains(Windows.ApplicationModel.DataTransfer.StandardDataFormats.StorageItems))
        {
            var items = await e.DataView.GetStorageItemsAsync();
            var paths = items.Select(item => item.Path).ToList();
            ViewModel.AddFiles(paths);
        }
    }

    private void OnClearClick(object sender, RoutedEventArgs e) => ViewModel.ClearFiles();

    private async void OnOpenClick(object sender, RoutedEventArgs e) => OpenFilePicker();

    private async void OpenFilePicker()
    {
        var picker = new FileOpenPicker();
        picker.FileTypeFilter.Add(".jpg");
        picker.FileTypeFilter.Add(".jpeg");
        picker.FileTypeFilter.Add(".png");
        picker.FileTypeFilter.Add(".ico");
        picker.FileTypeFilter.Add(".mp4");
        picker.FileTypeFilter.Add(".mp3");

        var hwnd = Win32Interop.GetWindowHandle(this);
        InitializeWithWindow.Initialize(picker, hwnd);

        var files = await picker.PickMultipleFilesAsync();
        if (files != null && files.Count > 0)
        {
            var paths = files.Select(f => f.Path).ToList();
            ViewModel.AddFiles(paths);
        }
    }
}
