using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

using OtakuLib.Logic.Components;
using OtakuLib.Logic.ViewModels;
using OtakuLib.MangaSourceBase;

namespace OtakuLib.WPF.Components;

/// <summary>
/// Interaction logic for SinglePagePagesViewer.xaml
/// </summary>
public partial class SinglePagePagesViewer : UserControl, IPagesViewer, INotifyPropertyChanged
{
    private ImageSource? imageSource;

    public SinglePagePagesViewer(IPagesViewerVM pagesViewerVM)
    {
        Id = "GihanSoft.SinglePage";
        Title = "Single Page";
        ViewModel = pagesViewerVM;
        InitializeComponent();
        ViewModel.PropertyChanged += ViewModel_PropertyChanged;
    }

    private void ViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(IPagesViewerVM.Page) or nameof(IPagesViewerVM.PagesProvider):

#pragma warning disable VSTHRD110 // Observe result of async calls
                _ = SetPageAsync()
                    .ContinueWith(_ => ViewModel.PagesProvider?.LoadPageAsync(ViewModel.Page + 1) ?? Task.CompletedTask, TaskScheduler.Current)
                    .ContinueWith(_ => ViewModel.PagesProvider?.LoadPageAsync(ViewModel.Page + 2) ?? Task.CompletedTask, TaskScheduler.Current);
#pragma warning restore VSTHRD110 // Observe result of async calls

                break;
        }

        async Task SetPageAsync()
        {
            if (ViewModel.PagesProvider is null)
            {
                ImageSource = null;
            }
            else
            {
                await ViewModel.PagesProvider.LoadPageAsync(ViewModel.Page).ConfigureAwait(true);
                var mem = ViewModel.PagesProvider.GetPage(ViewModel.Page)!;
                mem.Position = 0;
                ImageSource = BitmapFrame.Create(mem);
            }
        }
    }

    public IPagesViewerVM ViewModel { get; }

    public string Id { get; }

    public string Title { get; }

    public ImageSource? ImageSource
    {
        get => imageSource;
        set
        {
            imageSource = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ImageSource)));
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void ScrollViewer_PreviewKeyDown(object sender, KeyEventArgs e)
    {
        if (sender is not ScrollViewer scrollViewer)
        {
            return;
        }

        var rtl = scrollViewer.FlowDirection == FlowDirection.RightToLeft;
        var isHScrollStart = scrollViewer.HorizontalOffset == 0;
        var isHScrollEnd = scrollViewer.HorizontalOffset == scrollViewer.ScrollableWidth;
        var isVScrollStart = scrollViewer.VerticalOffset == 0;
        var isVScrollEnd = scrollViewer.VerticalOffset == scrollViewer.ScrollableHeight;

        e.Handled = true;
        switch (e.Key)
        {
            case Key.PageUp:
            case Key.Left or Key.A when !rtl && isHScrollStart:
            case Key.Right or Key.D when rtl && isHScrollStart:
            case Key.Up or Key.W when isVScrollStart:
                ViewModel.CmdMoveToPreviousPage.Execute(null);
                scrollViewer.ScrollToRightEnd();
                scrollViewer.ScrollToBottom();
                break;

            case Key.PageDown:
            case Key.Left or Key.A when rtl && isHScrollEnd:
            case Key.Right or Key.D when !rtl && isHScrollEnd:
            case Key.Down or Key.S when isVScrollEnd:
                ViewModel.CmdMoveToNextPage.Execute(null);
                scrollViewer.ScrollToHorizontalOffset(0);
                scrollViewer.ScrollToVerticalOffset(0);
                break;

            case Key.W:
                scrollViewer.RaiseEvent(new KeyEventArgs(e.KeyboardDevice, e.InputSource, e.Timestamp, Key.Up)
                {
                    RoutedEvent = KeyDownEvent
                });
                break;

            case Key.S:
                scrollViewer.RaiseEvent(new KeyEventArgs(e.KeyboardDevice, e.InputSource, e.Timestamp, Key.Down)
                {
                    RoutedEvent = KeyDownEvent
                });
                break;

            case Key.A:
                scrollViewer.RaiseEvent(new KeyEventArgs(e.KeyboardDevice, e.InputSource, e.Timestamp, Key.Left)
                {
                    RoutedEvent = KeyDownEvent
                });
                break;

            case Key.D:
                scrollViewer.RaiseEvent(new KeyEventArgs(e.KeyboardDevice, e.InputSource, e.Timestamp, Key.Right)
                {
                    RoutedEvent = KeyDownEvent
                });
                break;

            case Key.Tab:
                break;

            default:
                e.Handled = false;
                break;
        }
    }

    private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
    {
        if (sender is not ScrollViewer scrollViewer)
        {
            return;
        }

        static async Task AsyncMothod(ScrollViewer scrollViewer, MouseWheelEventArgs e, IPagesViewerVM vm)
        {
            if (Keyboard.Modifiers != ModifierKeys.Control)
            {
                return;
            }

            vm.Zoom += 0.05 * (e.Delta > 0 ? 1 : -1);

            await Dispatcher.Yield();

            scrollViewer.ScrollToHorizontalOffset(scrollViewer.ScrollableWidth / 2);
            scrollViewer.ScrollToVerticalOffset(scrollViewer.ScrollableHeight / 2);
        }

        _ = AsyncMothod(scrollViewer, e, ViewModel);
    }

    private void ScrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
    {
        if (e.VerticalChange != 0)
        {
            ViewModel.Offset = e.VerticalOffset;
        }
        else if (sender is ScrollViewer scrollViewer)
        {
            scrollViewer.ScrollToVerticalOffset(ViewModel.Offset);
        }
    }
}