using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Threading;

using MahApps.Metro.IconPacks;

using OtakuLib.Logic.Pages;
using OtakuLib.Logic.ViewModels;

namespace OtakuLib.WPF.Pages;

/// <summary>
/// Interaction logic for PgMangaViewer.xaml
/// </summary>
public partial class PgMangaViewer : IPgMangaViewer
{
    private readonly DispatcherTimer timer;

    public PgMangaViewer(IPgMangaViewerVM viewModel)
    {
        ViewModel = viewModel;
        InitializeComponent();
        IsVisibleChanged += (_, _) => _ = IsVisibleChangedAsync();
        timer = new(DispatcherPriority.Background)
        {
            Interval = TimeSpan.FromSeconds(1)
        };
    }

    private void DispatcherTimer_Tick(object? sender, EventArgs e)
    {
        tbTime.SetCurrentValue(TextBlock.TextProperty, DateTime.Now.ToShortTimeString());
    }

    public IPgMangaViewerVM ViewModel { get; }

    private async Task IsVisibleChangedAsync()
    {
        await Dispatcher.Yield();
        ViewModel.FullScreenProvider.IsFullScreen = false;
        grdToolbar.SetCurrentValue(HeightProperty, 32.0);
        if (IsVisible)
        {
            ViewModel.FullScreenProvider.PropertyChanged += FullScreenProvider_PropertyChanged;
            timer.Tick += DispatcherTimer_Tick;
            timer.Start();
        }
        else
        {
            ViewModel.FullScreenProvider.PropertyChanged -= FullScreenProvider_PropertyChanged;
            timer.Stop();
            timer.Tick -= DispatcherTimer_Tick;
        }
    }

    private void FullScreenProvider_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(ViewModel.FullScreenProvider.IsFullScreen) when !ViewModel.FullScreenProvider.IsFullScreen:
                grdToolbar.SetCurrentValue(HeightProperty, 32.0);
                break;

            default:
                break;
        }
    }

    protected override void OnPreviewMouseMove(MouseEventArgs e)
    {
        base.OnPreviewMouseMove(e);
        if (!ViewModel.FullScreenProvider.IsFullScreen)
        {
            return;
        }

        var position = Mouse.GetPosition(this);
        if (position.Y <= 10)
        {
            grdToolbar.SetCurrentValue(HeightProperty, 32.0);
        }
        else if (!grdToolbar.IsMouseOver)
        {
            grdToolbar.SetCurrentValue(HeightProperty, 0.0);
        }
    }

    protected override void OnPreviewKeyDown(KeyEventArgs e)
    {
        ArgumentNullException.ThrowIfNull(e);

        base.OnPreviewKeyDown(e);

        e.Handled = true;
        switch (e.Key)
        {
            case Key.Left when e.KeyboardDevice.Modifiers == ModifierKeys.Control:
                ViewModel.Chapter--;
                break;

            case Key.Right when e.KeyboardDevice.Modifiers == ModifierKeys.Control:
                ViewModel.Chapter++;
                break;

            case Key.F11:
                ViewModel.FullScreenProvider.IsFullScreen = !ViewModel.FullScreenProvider.IsFullScreen;
                break;

            case Key.Add or Key.OemPlus:
                ViewModel.PagesViewer.ViewModel.Zoom += 0.1;
                break;

            case Key.Subtract or Key.OemMinus:
                ViewModel.PagesViewer.ViewModel.Zoom -= 0.1;
                break;

            default:
                e.Handled = false;
                break;
        }
    }

    private void Ltr_Click(object sender, RoutedEventArgs e)
    {
        contentPresenter.SetCurrentValue(FlowDirectionProperty, FlowDirection.LeftToRight);
        if (btnRtl.Content is PackIconMaterial iconRtl && btnLtr.Content is PackIconMaterial iconLtr)
        {
            iconRtl.Kind = PackIconMaterialKind.PagePreviousOutline;
            iconLtr.Kind = PackIconMaterialKind.PageNext;
        }
    }

    private void Rtl_Click(object sender, RoutedEventArgs e)
    {
        contentPresenter.SetCurrentValue(FlowDirectionProperty, FlowDirection.RightToLeft);
        if (btnRtl.Content is PackIconMaterial iconRtl && btnLtr.Content is PackIconMaterial iconLtr)
        {
            iconRtl.Kind = PackIconMaterialKind.PagePrevious;
            iconLtr.Kind = PackIconMaterialKind.PageNextOutline;
        }
    }
}