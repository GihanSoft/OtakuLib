using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

using OtakuLib.Logic.Components;
using OtakuLib.Logic.ViewModels;

namespace OtakuLib.WPF.Components;
/// <summary>
/// Interaction logic for SinglePagePagesViewer.xaml
/// </summary>
public partial class SinglePagePagesViewer : UserControl, IPagesViewer
{
    public SinglePagePagesViewer(IPagesViewerVM pagesViewerVM)
    {
        Title = "Single Page";
        ViewModel = pagesViewerVM;
        InitializeComponent();
    }

    public IPagesViewerVM ViewModel { get; }

    public string Title { get; }

    private void ScrollViewer_PreviewKeyDown(object sender, KeyEventArgs e)
    {
        if (sender is not ScrollViewer scrollViewer) { return; }

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
                scrollViewer.ScrollToHorizontalOffset(scrollViewer.ScrollableWidth);
                scrollViewer.ScrollToVerticalOffset(scrollViewer.ScrollableHeight);
                break;
            case Key.PageDown:
            case Key.Left or Key.A when rtl && isHScrollEnd:
            case Key.Right or Key.D when !rtl && isHScrollEnd:
            case Key.Down or Key.S when isVScrollEnd:
                ViewModel.CmdMoveToNextPage.Execute(null);
                scrollViewer.ScrollToHorizontalOffset(0);
                scrollViewer.ScrollToVerticalOffset(0);
                break;
            default:
                e.Handled = false;
                break;
        }
    }

    private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
    {
        if (sender is not ScrollViewer scrollViewer) { return; }

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
}
